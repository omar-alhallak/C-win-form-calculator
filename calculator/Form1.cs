using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;




namespace calculator
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
        char[] opers = { '+', '-', '×', '÷', '%' };
        bool isOp = false;
        bool isDot = true;
        bool isZero = true;
        bool isNum = true;
        bool thefirstoper = true;
        protected static bool divByZero = false;
        private void btnClick(object sender, EventArgs e)
        {
            thefirstoper = true;
            Button button = (Button)sender;
            if (divByZero)
            {
                textBox1.Text = "0";
                divByZero = false;
            }
            if (button.Text != "0" && !isNum)
            {
                isZero = true;
                isOp = true;
                isNum = true;
            }

            if (button.Text == "0" && isOp || !isZero)
            {
                if (textBox1.Text.Last() == '0')
                {
                    return;
                }
                isZero = true;
                return;

            }
            else if (button.Text == "0")
            {
                if (LastisOperator())
                {
                    textBox1.Text += button.Text;
                    isOp = true;
                    return;
                }
            }
            if (isOp)
            {
                string temp = textBox1.Text;
                textBox1.Text = "";
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    textBox1.Text += temp[i];
                }

            }
            isOp = false;
            if (textBox1.Text == "0")
            {
                textBox1.Text = "";
                a = 0;
            }
            textBox1.Text += button.Text;
           
        }

        private void AClear(object sender, EventArgs e)
        {
            a = 0;
            thefirstoper = true;
            isDot = true;
            textBox1.Text = "0";
        }

        private void delete(object sender, EventArgs e)
        {

            thefirstoper = true;
            isOp = false;
            char[] txt = textBox1.Text.ToCharArray();
            textBox1.Text = "";
            if (txt.Length == 1)
            {
                textBox1.Text = "0";
                a = 0;
            }
            else
            {
                if (txt.Last() == '.')
                {
                    for (int i = 0; i < txt.Length - 1; i++)
                    {

                        textBox1.Text += txt[i];
                        isDot = true;
                        if (textBox1.Text.Last() == '0')
                        {
                            isZero = false;
                            isNum = false;
                            a = 0;
                        }
                    }
                }
                else
                {
                    if (txt.Last() == '-' || txt.Last() == '+' || txt.Last() == '÷' || txt.Last() == '×')
                    {
                        for (int i = 0; i < txt.Length - 1; i++)
                        {

                            textBox1.Text += txt[i];
                            isOp = true;
                            if (textBox1.Text.Last() == '0')
                            {
                                isZero = false;
                                isNum = false;                             
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < txt.Length - 1; i++)
                        {

                            textBox1.Text += txt[i];

                        }
                    }
                }
            }
        }


        private void operators(object sender, EventArgs e)
        {
            a = 0;
            isOp = false;
            string temp = textBox1.Text;
            Button button = (Button)sender;
            if (button.Text == "-" && textBox1.Text == "0")
            {
                textBox1.Text = "-";
                thefirstoper = false;
            }
            if (LastisOperator() && thefirstoper)
            {
                textBox1.Text = "";
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    textBox1.Text += temp[i];

                }
                textBox1.Text += button.Text;

            }
            else if (textBox1.Text.Last() != '.' && thefirstoper)
            {
                textBox1.Text += button.Text;
            }
            isDot = true;
        }

        private void dot(object sender, EventArgs e)
        {
            isOp = false;
            if (LastisOperator())
            {
                textBox1.Text += "0" + ".";
                isDot = false;
            }
            if (isDot)
            {
                textBox1.Text += ".";
                isDot = false;
            }
            if (!isDot)
            {
                return;
            }
        }


        bool LastisOperator()
        {
            if (textBox1.Text.Last() == '+' || textBox1.Text.Last() == '÷' || textBox1.Text.Last() == '×' || textBox1.Text.Last() == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool ThereisOperator()
        {
            if (textBox1.Text.Contains('+') || textBox1.Text.Contains('×') || textBox1.Text.Contains('÷') || textBox1.Text.Contains('-'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        int a = 0;
        private void ModClick(object sender, EventArgs e)
        {

            a++;
            if (LastisOperator())
            {
                return;
            }
            else
            {
                if (!ThereisOperator() && a <= 2)
                {
                    double Tomod = double.Parse(textBox1.Text);
                    textBox1.Text = (Tomod / 100).ToString();
                    return;
                }
                if (a <= 2)
                {
                    string temp = textBox1.Text;
                    string sub = textBox1.Text;
                    for (int i = textBox1.Text.Length - 1; i >= 0; i--)
                    {
                        if (Equation.isOperator(sub[i]))
                        {
                            sub = sub.Substring(i + 1);
                            temp = temp.Remove(i + 1);
                            break;
                        }
                    }
                    double num = double.Parse(sub);
                    num = num / 100;
                    textBox1.Text = temp + num.ToString();
                }
            }
        }

        private void res_Click(object sender, EventArgs e)
        {

            string question = "";
            if (LastisOperator() || textBox1.Text.Last() == '.')
            {
                question = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                label1.Text = question;
                textBox1.Text = "";
            }
            else
            {
                question = textBox1.Text;
                label1.Text = question;
                textBox1.Text = "";
            }
            textBox1.Text = Equation.calculate(question);
            if (textBox1.Text.Contains('.'))
                isDot = false;
        }
    }
    class Equation : Form1
    {

        public static string calculate(string quest)
        {
            List<string> question = new List<string>();

            string temporary = "";
            for (int i = 0; i < quest.Length; i++)
            {
                if (isOperator(quest[i]))
                {
                    question.Add(temporary);
                    question.Add(quest[i].ToString());
                    temporary = "";
                }
                else
                {
                    temporary += quest[i];
                }
            }
            if (temporary.Length > 0)
            {
                question.Add(temporary);
            }
            if (question[0] == "")
                question.RemoveAt(0);
            if (question[0] == "-")
            {
                question[0] += question[1];
                question.RemoveAt(1);
            }

            for (int i = 0; i < question.Count; i++)
            {
                if (!question.Contains("×") && !question.Contains("÷"))
                    break;

                if (question[i] == "×")
                {
                    double mul = twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = mul.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }
                if (question[i] == "÷")
                {
                    if (question[i + 1] == "0")
                    {
                        divByZero = true;
                        return "Cant div by zero";
                    }
                    double div = twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = div.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }

            }

            if (question[0] == "-")
            {
                question[0] = "-" + question[1];
                question.RemoveAt(1);
            }

            for (int i = 0; i < question.Count; i++)
            {
                if (!question.Contains("+") && !question.Contains("-"))
                    break;

                if (question[i] == "-")
                {
                    double min = twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = min.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }
                if (question[i] == "+")
                {
                    double plus = twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = plus.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }

            }



            string temp = "";
            for (int i = 0; i < question.Count; i++)
            {
                temp += question[i];
            }
            return temp;

        }
        public static double twoNumbers(string num11, string op, string num22)
        {
            double num1 = double.Parse(num11);
            double num2 = double.Parse(num22);
            try
            {

                switch (op)
                {
                    case "×":
                        return num1 * num2;
                    case "÷":
                        return num1 / num2;
                    case "+":
                        return num1 + num2;
                    case "-":
                        return num1 - num2;

                }
            }
            catch (Exception)
            {

            }
            return -1;
        }

        public static bool isOperator(char ch)
        {
            if (ch == '+' || ch == '-' || ch == '×' || ch == '÷')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}