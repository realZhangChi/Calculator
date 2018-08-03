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
using Calculator;

namespace Calculator_Gui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculate calculate = new Calculate();
        Expressions expressions = new Expressions();

        public MainWindow()
        {
            DataContext = expressions;
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // 对输入进行验证处理
            var b = calculate.SetInput(((Button)sender).Content.ToString());
            // 字符可直接输入
            if (b == 1)
            {
                // 判断当前字符是否为"=",若是则显示运算结果，并更新界面，显示结果的处理逻辑在Expressions类中；若不是则更新界面
                if (((Button)sender).Content.ToString() == "=")
                    expressions.result = calculate.Result;
                expressions.UiDataUpdate(((Button)sender).Content.ToString());
            }
            // 需删除一个字符再追加当前字符
            else if (b == 2)
            {
                // 若当前字符为Del，则不追加(追加null)
                if (((Button)sender).Content.ToString() == "Del")
                    expressions.UiDataUpdate(null, true);
                else
                    expressions.UiDataUpdate(((Button)sender).Content.ToString(), true);
            }
            else if (b == 3)
                expressions.AllClear();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
