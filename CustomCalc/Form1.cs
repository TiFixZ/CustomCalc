using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CustomCalc
{
    
    public partial class Form1 : Form
    {
        Operation op;
        public Form1()
        {
            InitializeComponent();
        }
        private string Summ(float first, float second)
        {
            return Result(first + second);
        }
        private string Minus(float first, float second)
        {
            return Result(first - second);
        }
        private string Multiply(float first, float second)
        {
            return Result(first * second);
        }
        private string Divide(float first, float second)
        {
            if (second == 0)
            {
                throw new DivideByZeroException("Деление на ноль невозможно.");
            }
            return Result(first / second);
        }
        private string Result(float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
            {
                throw new ArithmeticException("Результат выходит за диапазон float.");
            }
            return value.ToString();
        }
        private string Calculate()
        {
            float first;
            float second;
            bool firstOk = float.TryParse(tbOperand1.Text, out first) &&
                !float.IsNaN(first) && !float.IsInfinity(first);
            bool secondOk = float.TryParse(tbOperand2.Text, out second) &&
                !float.IsNaN(second) && !float.IsInfinity(second);

            if (!firstOk && !secondOk)
            {
                throw new FormatException("Некорректно введены первое и второе числа.");
            }
            if (!firstOk)
            {
                throw new FormatException("Некорректно введено первое число.");
            }
            if (!secondOk)
            {
                throw new FormatException("Некорректно введено второе число.");
            }
            if (op == null)
            {
                throw new InvalidOperationException("Выберите арифметическую операцию.");
            }
            return op(first, second);
        }
        private void btnResult_Click(object sender, EventArgs e)
        {
            try
            {
                lbResult.Text = Calculate();
            }
            catch (Exception ex)
            {
                lbResult.Text = "Ошибка";
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            op = Summ;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            op = Minus;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            op = Multiply;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            op = Divide;
        }
        delegate string Operation(float first, float second);
    }
}
