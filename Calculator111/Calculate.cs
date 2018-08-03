using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculate : ICalculator
    {
        //public List<int> num = new List<int>();

        private readonly int[,] vs = new int[6, 6]
        {
        //    +  -  *  /  (  ) 0弹出 1压栈
        /*+*/{0, 0, 1, 1, 1, 0 },
        /*-*/{0, 0, 1, 1, 1, 0 },
        /***/{0, 0, 0, 0, 1, 0 },
        /*/*/{0, 0, 0, 0, 1, 0 },
        /*(*/{1, 1, 1, 1, 1, 0 },
        /*)*/{2, 2, 2, 2, 2, 2 }
        };

        private readonly Dictionary<string, int> keyValues = new Dictionary<string, int> { { "+", 0 }, { "-", 1 }, { "×", 2 }, { "÷", 3 }, { "(", 4 }, { ")", 5 } };
        
        private string temp = null; //临时变量，存放操作数
        private double result; // 计算结果
        public List<string> interthemExp = new List<string>(); // 中序表达式
        private Stack<string> oper = new Stack<string>(); // 运算符栈
        private List<string> postorderExp = new List<string>(); // 后序表达式

        public double Result
        {
            get
            {
                return result;
            }
            private set { }
        }

        /// <summary>
        /// 转换为后序表达式时，操作符的处理方法
        /// </summary>
        /// <param name="c"></param>
        public void Oper(string c)
        {
            // 如果运算符栈为空，则直接将运算符压栈
            if (oper.Count() == 0)
            {
                oper.Push(c);
                //oper.Add(c);
            }

            // 如果运算符栈不为空，则进行优先级比较，决定是压栈还是出栈
            else if (oper.Count() != 0)
            {

                // 如果当前运算符优先级高于栈顶运算符，则压栈
                if (vs[keyValues[oper.Peek()], keyValues[c]] == 1)
                {
                    oper.Push(c);
                }

                // 如果当前运算符优先级低于或等于栈顶运算符，则将栈顶运算符弹出至后续表达式，
                // 直至当前运算符优先级高于栈顶运算符或栈为空，则将当前运算符压栈
                else if (vs[keyValues[oper.Peek()], keyValues[c]] == 0)
                {
                    while (oper.Count() != 0 && (vs[keyValues[oper.Peek()], keyValues[c]] == 0))
                    {
                        if (oper.Peek() != "(")
                        {
                            postorderExp.Add(oper.Peek());
                            oper.Pop();
                        }
                        else if (oper.Peek() == "(")
                        {
                            oper.Pop();
                            break;
                        }
                    }
                    if (c != ")")
                        oper.Push(c);
                }
            }
        }

        /// <summary>
        /// 转换为后序表达式
        /// </summary>
        public void ToPostorder()
        {
            // 遍历中序表达式
            foreach (var item in interthemExp)
            {
                // 如果是运算符，则进行栈操作
                if (item == "+" || item == "-" || item == "×" || item == "÷" || item == "(" || item == ")")
                {
                    Oper(item);
                }
                // 如果是操作数，则直接输出至后序表达式
                else
                {
                    postorderExp.Add(item);
                }
            }
            while (oper.Count() != 0)
            {
                postorderExp.Add(oper.Peek());
                oper.Pop();
            }
        }

        /// <summary>
        /// 计算结果主方法
        /// </summary>
        public void StartCalculator()
        {
            ToPostorder();
            var length = postorderExp.Count();
            for (var i = 0; i < length; i++)
            {
                if (postorderExp[i] == "+")
                {
                    postorderExp[i] = Convert.ToString(Convert.ToDouble(postorderExp[i - 2]) + Convert.ToDouble(postorderExp[i - 1]));
                    i = CalculatorHelper(i);
                }
                else if (postorderExp[i] == "-")
                {
                    postorderExp[i] = Convert.ToString(Convert.ToDouble(postorderExp[i - 2]) - Convert.ToDouble(postorderExp[i - 1]));
                    i = CalculatorHelper(i);
                }
                else if (postorderExp[i] == "×")
                {
                    postorderExp[i] = Convert.ToString(Convert.ToDouble(postorderExp[i - 2]) * Convert.ToDouble(postorderExp[i - 1]));
                    i = CalculatorHelper(i);
                }
                else if (postorderExp[i] == "÷")
                {
                    postorderExp[i] = Convert.ToString(Convert.ToDouble(postorderExp[i - 2]) / Convert.ToDouble(postorderExp[i - 1]));
                    i = CalculatorHelper(i);
                }
                length = postorderExp.Count();
            }
            if (postorderExp.Count == 1)
            {
                result = Convert.ToDouble(postorderExp[0]);
                postorderExp.RemoveAll(o => true);
                interthemExp.RemoveAll(o => true);
            }
        }

        /// <summary>
        /// 计算结果辅助方法，用于计算时后序表达式的变换，每进行一次运算需变换一次后序表达式
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int CalculatorHelper(int i)
        {
            postorderExp.RemoveAt(i - 2);
            postorderExp.RemoveAt(i - 2);
            i -= 2;
            return i;
        }

        /// <summary>
        /// 判断输入是否正确并处理输入,0 输入失败 1 直接输入运算符；2 删除最后一个运算符后输入运算符; 3 清除所有数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns>0 输入失败 1 直接输入运算符；2 删除最后一个运算符后输入运算符</returns>
        public int SetInput(string s)
        {
            // 对运算符"+-*/)",判断输入是否合法
            if (s == "+" || s == "-" || s == "×" || s == "÷" || s == ")")
            {
                // 如果当前表达式不为空,临时变量是否存有操作数?将操作数输出至中序表达式并将temp赋null:尝试转换中序表达式最后一个元素，
                // 成功转换则为操作数，将运算符输出至中序表达式，不成功则为运算符，将表达式中运算符删掉，换为当前新的运算符
                if (interthemExp.Count != 0)
                {
                    if (temp != null)
                    {
                        interthemExp.Add(temp);
                        temp = null;
                    }
                    try
                    {
                        Convert.ToDouble(interthemExp.Last());
                    }
                    catch (FormatException)
                    {
                        if (interthemExp.Last() == ")")
                        {
                            interthemExp.Add(s);
                            return 1;
                        }
                        interthemExp.Remove(interthemExp.Last());
                        interthemExp.Add(s);
                        return 2;
                    }
                    interthemExp.Add(s);
                    return 1;
                }

                // 如果中序表达式为空，则判断是否临时变量中是否有未输出至中序表达式的操作数，
                // 如果是则将操作数输出后将运算符输出，如果否则输入有误
                else
                {
                    if (temp != null)
                    {
                        interthemExp.Add(temp);
                        temp = null;
                        interthemExp.Add(s);
                        return 1;
                    }
                    return 0;
                }
            }

            // 对"("进行特殊处理：
            // 中序表达式不为空时，如果中序表达式中最后一个元素为合法运算符，则输入合法，将"("输出至中序表达式否则输入有误;
            // 中序表达式为空时，输入正确将"("输出至中序表达式.
            else if (s == "(")
            {
                if (interthemExp.Count != 0)
                {
                    if (interthemExp.Last() == "+" || interthemExp.Last() == "-" || interthemExp.Last() == "×" || interthemExp.Last() == "÷")
                    {
                        interthemExp.Add(s);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                    interthemExp.Add(s);
                return 1;
            }
            // 对小数点进行处理：输入小数点前已经输入了数字(temp != null)，则输入合法，否则输入有误
            else if (s == ".")
            {
                if (temp != null)
                {
                    temp += s;
                    return 1;
                }
                else
                    return 0;
            }

            // 对等号进行处理：
            else if(s == "=")
            {

                // temp若不为空则先将temp输出至中序表达式，然后进行计算（对等号前是数字和右括号两种情况进行处理）
                if (temp != null)
                {
                    interthemExp.Add(temp);
                    temp = null;
                }

                // 如果等号之前有左括号或者"+-*/"运算符，则将其删除
                if (interthemExp.Last() == "(")
                    interthemExp.RemoveAt(interthemExp.IndexOf(interthemExp.Last()));
                if (interthemExp.Last() == "+" || interthemExp.Last() == "-" || interthemExp.Last() == "×" || interthemExp.Last() == "÷")
                    interthemExp.RemoveAt(interthemExp.IndexOf(interthemExp.Last()));

                StartCalculator();
                return 1;
            }

            // Del的处理：当临时变量不为空时从临时变量删除一个数字，否则从中序表达式删除；注意不等于null和元素数量不为零都要判断
            else if (s == "Del")
            {
                if (temp != null && temp.Length != 0)
                    temp = temp.Remove(temp.Length - 1);
                else if (interthemExp.Count != 0 && interthemExp != null)
                    interthemExp.RemoveAt(interthemExp.IndexOf(interthemExp.Last()));
                return 2;
            }

            // C的处理，清空所有内容
            else if (s == "C")
            {
                temp = null;
                interthemExp.RemoveAll(o => true);
                result = 0;
                postorderExp.RemoveAll(o => true);
                while (oper.Count() != 0)
                    oper.Pop();
                return 3;
            }
            // 对输入的数字进行验证，如果表达式最后一个字符为")"，则输入有误
            else
            {
                if (interthemExp.Count() != 0)
                {
                    if (interthemExp.Last() == ")")
                        return 0;
                    else
                        temp += s;
                        return 1;
                }
                else
                {
                    temp += s;
                    return 1;
                }
            }
        }

        /// <summary>
        /// 获取计算结果
        /// </summary>
        /// <returns>double计算结果</returns>
        public double GetResult()
        {
            return result;
        }
    }
}
