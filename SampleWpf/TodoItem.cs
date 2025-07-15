using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWpf
{
    public class TodoItem : BindableBase
    {
        private string _text;
        private bool _isComplete;

        public int Id { get; set; }  // 화면과 관련이 없으므로

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public bool IsComplete
        {
            get => _isComplete;
            set => SetProperty(ref _isComplete, value);
        }
    }
}
