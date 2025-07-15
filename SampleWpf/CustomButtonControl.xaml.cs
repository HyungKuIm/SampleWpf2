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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleWpf;
/// <summary>
/// CustomButtonControl.xaml에 대한 상호 작용 논리
/// </summary>
public partial class CustomButtonControl : UserControl
{
    public CustomButtonControl()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty
        ButtonTextProperty =
            DependencyProperty.Register("ButtonText",
                typeof(string), typeof(CustomButtonControl),
                new PropertyMetadata("Click me"));

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public static DependencyProperty ButtonCommandProperty =
        DependencyProperty.Register("ButtonCommand", typeof(ICommand),
            typeof(CustomButtonControl), new PropertyMetadata(null));
    
    
    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }
}
