using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWpf
{
    class EBookViewModel : BindableBase
    {
        public DelegateCommand<string> OpenPdfCommand { get; }

        public EBookViewModel()
        {
            OpenPdfCommand = new DelegateCommand<string>(OnOpenPdf);
        }

        // 이 Action은 View에서 바인딩할 예정
        public Action<PdfViewer> ShowPdfViewerAction { get; set; }

        private void OnOpenPdf(string fileName)
        {
            string filePath = $"pdf/{fileName}.pdf";
            try
            {
                var viewer = new PdfViewer
                {
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    Document = PdfiumViewer.PdfDocument.Load(filePath)
                };

                ShowPdfViewerAction?.Invoke(viewer);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"PDF 열기 실패: {ex.Message}");
            }
        }
    }
}
