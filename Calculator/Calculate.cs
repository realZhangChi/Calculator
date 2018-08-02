using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculate
    {
        public List<int> num = new List<int>();

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

        private readonly Dictionary<string, int> keyValues = new Dictionary<string, int> { { "+", 0 }, { "-", 1 }, { "*", 2 }, { "/", 3 }, { "(", 4 }, { ")", 5 } };

        public void Num(int n)
        {
            num.Add(n);
        }

        public double result;
        public List<string> interthemExp = new List<string>();
        private List<string> oper = new List<string>();
        private List<string> postorderExp = new List<string>();
        public void Exp(string c)
        {
            postorderExp.Add(c);
        }

        /// <summary>
        /// 转换为后序表达式时，操作符的处理方法
        /// </summary>
        /// <param name="c"></param>
        public void Oper(string c)
        {
            if(oper.Count() == 0)
            {
                oper.Add(c);
            }
            else if(oper.Count() != 0)
            {
                if(vs[keyValues[oper.Last()], keyValues[c]] == 1)
                {
                    oper.Add(c);
                }
                else if (vs[keyValues[oper.Last()], keyValues[c]] == 0)
                {
                    while (oper.Count() != 0 && (vs[keyValues[oper.Last()], keyValues[c]] == 0))
                    {
                        if (oper.Last() != "(")
                        {
                            postorderExp.Add(oper.Last());
                            oper.Remove(oper.Last());
                        }
                        else if (oper.Last() == "(")
                        {
                            oper.Remove(oper.Last());
                            break;
                        }
                    }
                    if (c != ")")
                        oper.Add(c);
                }
            }
        }
        
        /// <summary>
        /// 转换为后序表达式
        /// </summary>
        public void ToPostorder()
        {
            foreach (var item in interthemExp)
            {
                if (item == "+" || item == "-" || item == "*" || item == "/" || item == "(" || item == ")")
                {
                    Oper(item);
                }
                else
                {
                    postorderExp.Add(item);
                }
            }
            while (oper.Count() != 0)
            {
                postorderExp.Add(oper.Last());
                oper.Remove(oper.Last());
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
                else if (postorderExp[i] == "*")
                {
                    postorderExp[i] = Convert.ToString(Convert.ToDouble(postorderExp[i - 2]) * Convert.ToDouble(postorderExp[i - 1]));
                    i = CalculatorHelper(i);
                }
                else if (postorderExp[i] == "/")
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
    }
}
