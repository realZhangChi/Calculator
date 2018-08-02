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

        public void EqualsClick()
        {
            isEqualsClick = true;
        }

        public void UiDataUpdate(string s)
        {
            if (s == "=")
            {
                isEqualsClick = true;
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

        public void Ex1Clear()
        {
            expression1 = null;
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
