using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SearchForm
{
    class Cell
    {
        private int value;
        private Label label;

        public Cell()
        {
            value = 0;
            label = null;
        }

        public Cell(Label newLabel, int newValue)
        {
            value = newValue;
            label = newLabel;
            NewCellText();
        }

        public int GetValue()
        {
            return value;
        }

        public string GetValueAsString()
        {
            return value.ToString();
        }

        public void SetValue(int newValue)
        {
            value = newValue;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetLabel(Label newLabel)
        {
            label = newLabel;
        }

        private int HowManyDigits()
        {
            if(value < 10)
            {
                return 1;
            }
            else if(value < 100)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private void NewCellText()
        {
            switch(HowManyDigits())
            {
                case 1: label.Invoke(new Action(() => label.Text = "00" + value.ToString()));
                    break;
                case 2: label.Invoke(new Action(() => label.Text = "0" + value.ToString()));
                    break;
                case 3: label.Invoke(new Action(() => label.Text = value.ToString()));
                    break;
            }
        } // end of NewCellText

        public void UpdateText()
        {
            NewCellText();
        }

        public static bool operator >(Cell lhs, Cell rhs)
        {
            bool status = false;

            if (lhs.GetValue() > rhs.GetValue())
            {
                status = true;
            }
            return status;
        }

        public static bool operator <(Cell lhs, Cell rhs)
        {
            bool status = false;

            if (lhs.GetValue() < rhs.GetValue())
            {
                status = true;
            }
            return status;
        }
    }//end of Cell Class
}
