using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Calculator
{
    public class Expressions : INotifyPropertyChanged
    {
        private string expression0;
        private string expression1;
        private bool isEqualsClick = false;
        public double result = 0;

        public void EqualsClick()
        {
            isEqualsClick = true;
        }

        public void UiDataUpdate(string s, bool b)
        {
            if (b == true)
            {
                if (expression1 != null && expression1.Length != 0)
                    expression1 = expression1.Remove(expression1.Length - 1);
                expression1 += s;
                OnProPertyChanged("Expression1");
            }
        }

        public void UiDataUpdate(string s)
        {
            if (s == "=")
            {
                isEqualsClick = true;
                expression0 = expression1;
                expression1 = result.ToString();
                result = 0;
                OnProPertyChanged("Expression0");
                OnProPertyChanged("Expression1");
            }
            else if (isEqualsClick)
            {
                expression0 = null;
                OnProPertyChanged("Expression0");
                expression1 = s;
                OnProPertyChanged("Expression1");
                isEqualsClick = false;
            }
            else
            {
                expression1 += s;
                OnProPertyChanged("Expression0");
                OnProPertyChanged("Expression1");
            }
        }

        public string Expression1
        {
            get
            {
                return expression1;
            }
            set
            {
                expression1 += value;
                OnProPertyChanged("Expression1");
            }
        }

        public void AllClear()
        {
            expression0 = null;
            expression1 = null;
            result = 0;
            isEqualsClick = false;
            OnProPertyChanged("Expression0");
            OnProPertyChanged("Expression1");
        }

        public string Expression0
        {
            get
            {
                return expression0;
            }
            set
            {
                expression0 += value;
                OnProPertyChanged("Expression0");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProPertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
