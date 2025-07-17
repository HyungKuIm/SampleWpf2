using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SampleWpf
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            //LoadPdf("pdf/1.아낌없이주는나무.pdf");
            Loaded += (s, e) =>
            {
                if (DataContext is EBookViewModel vm)
                {
                    vm.ShowPdfViewerAction = pdfViewer =>
                    {
                        pdfHost.Child = pdfViewer;
                    };

                    if (vm.ActiveBook != null)
                    {
                        vm.OpenPdfCommand.Execute(vm.ActiveBook.FileName);
                    }

                }
            };

            Closed += (s, e) =>
            {
                // 창이 닫힐 때 Action 제거 → 이후 PDF 열기 시도 방지
                if (DataContext is EBookViewModel vm)
                {
                    vm.ShowPdfViewerAction = null;
                }

                // pdfViewer.Dispose() 도 고려
                if (pdfHost.Child is PdfViewer pdfViewer)
                {
                    pdfViewer.Document?.Dispose();
                    pdfViewer.Dispose();
                }
            };
            
        }

        private void LoadPdf(string filePath)
        {
            var pdfViewer = new PdfViewer
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            pdfViewer.Document = PdfDocument.Load(filePath);

            pdfHost.Child = pdfViewer;
        }
    }
}
