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

        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("0");
            expressions.UiDataUpdate("0");
        }

        private void Button_Dot_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add(".");
            expressions.UiDataUpdate(".");
        }

        private void Button_Equals_Click(object sender, RoutedEventArgs e)
        {
            OperClick();
            calculate.StartCalculator();
            expressions.Expression0 = expressions.Expression1;
            expressions.Ex1Clear();
            expressions.Expression1 = calculate.result.ToString();
            expressions.EqualsClick();
        }

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("1");
            expressions.UiDataUpdate("1");
        }

        private void Button_LBracket_Click(object sender, RoutedEventArgs e)
        {
            OperClick();
            calculate.interthemExp.Add("(");
            expressions.UiDataUpdate("0");
        }

        private void OperClick()
        {
            //if (calculate.temp != null)
            //{
            //    calculate.interthemExp.Add(calculate.temp);
            //    calculate.temp = null;
            //}
        }

        private void Button_RBracket_Click(object sender, RoutedEventArgs e)
        {
            OperClick();
            calculate.interthemExp.Add(")");
            expressions.UiDataUpdate("0");
        }

        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Divide_Click(object sender, RoutedEventArgs e)
        {
            OperClick();
            calculate.interthemExp.Add("/");
            expressions.UiDataUpdate("÷");
        }

        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("7");
            expressions.UiDataUpdate("7");
        }

        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("8");
            expressions.UiDataUpdate("8");
        }

        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("9");
            expressions.UiDataUpdate("9");
        }

        private void Button_Multiply_Click(object sender, RoutedEventArgs e)
        {
            OperClick();
            calculate.interthemExp.Add("*");
            expressions.UiDataUpdate("×");
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("4");
            expressions.UiDataUpdate("4");
        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("5");
            expressions.UiDataUpdate("5");
        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("6");
            expressions.UiDataUpdate("6");
        }

        private void Button_Minus_Click(object sender, RoutedEventArgs e)
        {
            OperClick();
            calculate.interthemExp.Add("-");
            expressions.UiDataUpdate("-");
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("2");
            expressions.UiDataUpdate("2");
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            calculate.interthemExp.Add("3");
            expressions.UiDataUpdate("3");
        }

        private void Button_Plus_Click(object sender, RoutedEventArgs e)
        {
            OperClick();
            calculate.interthemExp.Add("+");
            expressions.UiDataUpdate("+");
        }
    }
}
