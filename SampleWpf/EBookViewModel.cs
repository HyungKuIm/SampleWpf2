using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWpf
{
    class EBookViewModel : BindableBase
    {
        private BookModel _activeBook;
        public BookModel ActiveBook
        {
            get => _activeBook;
            set
            {
                //SetProperty(ref _activeBook, value);
                if (SetProperty(ref _activeBook, value))
                {
                    // 자동으로 OpenPdfCommand 실행
                    if (ShowPdfViewerAction != null && value != null)
                    {
                        OpenPdfCommand.Execute(value.FileName);
                    }
                }
            }
        }

        public ObservableCollection<BookModel> Books { get; } = new();


        public DelegateCommand<string> OpenPdfCommand { get; }

        public EBookViewModel()
        {
            OpenPdfCommand = new DelegateCommand<string>(OnOpenPdf);
            InitializeData();
        }

        private void InitializeData()
        {
            Books.Add(new BookModel { Title = "아낌없이 주는 나무", FileName = "1.아낌없이주는나무" });
            Books.Add(new BookModel { Title = "너구리의 후회", FileName = "15.너구리의후회" });
            Books.Add(new BookModel { Title = "충성스런 개", FileName = "7.충성스런+개" });

            if (Books.Count > 0)
            {
                ActiveBook = Books[0];
            }
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
