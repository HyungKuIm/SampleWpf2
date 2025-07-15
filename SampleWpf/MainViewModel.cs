using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SampleWpf;
internal class MainViewModel : BindableBase
{
    public DelegateCommand TestButtonCmd { get; }

    public MainViewModel()
    {
        TestButtonCmd = new DelegateCommand(() =>
        {
            MessageBox.Show("버튼을 클릭하셨습니다");
        });
    }
}
