namespace calculator
{
    public partial class Calculator : Form
    {
        public enum Operators { noOne, plus, minus, times, division }
        Operators op = Operators.noOne;

        decimal memory = 0;
        decimal display = 0;
        decimal number1 = 0;
        decimal number2 = 0;
        decimal previous = 0;

        bool commonInTheStack = false;
        bool common = false;

        bool typed = false;
        bool calculated = false;

        public Calculator()
        {
            InitializeComponent();
        }

        private void Show(decimal num)
        {
            List<char> num3 = num.ToString().ToList();

            if (num3.Contains(','))
            {
                for (int i = num3.Count - 1; i >= 0; i--)
                {
                    char v = num3[i];

                    if (v == '0' || v == ',')
                    {
                        num3.RemoveAt(i);
                    }
                    else
                    {
                        break;
                    }

                    num = decimal.Parse(string.Join("", num3.ToArray()));
                }
            }

            display = num;
            Visor.Text = $"{display}";
        }

        private void AddNumber(int num)
        {
            String str;

            if (typed)
            {
                str = $"{(display != 0 ? display : "")}{(commonInTheStack ? "," : "")}{num}";
            }
            else
            {
                str = $"{num}";
            }

            if (str.Length <= 29)
            {
                if (commonInTheStack)
                {
                    common = true;
                    commonInTheStack = false;
                };
                Show(decimal.Parse(str));
            }
            else
            {
                Visor.Text = "E";
            }

            previous = display;

            typed = true;
        }

        private void Calculate()
        {
            number2 = previous;

            switch (op)
            { 
                case Operators.plus:
                    number1 += number2;
                    break;
                case Operators.minus:
                    number1 -= number2;
                    break;
                case Operators.times:
                    number1 *= number2;
                    break;
                case Operators.division:
                    if (number2 != 0)
                    {
                        number1 /= number2;
                    }
                    else
                    {
                        typed = false;
                        Visor.Text = "E";
                        return;
                    }
                    break;
            }

            Show(number1);
            typed = false;
        }

        private void AddOperator(Operators ope)
        {
            if (typed)
            {
                if (calculated)
                {
                    number2 = display;
                }
                else
                {
                    number1 = display;
                }
                Calculate();
                common = false;
                commonInTheStack = false;
            }
            calculated = true;
            op = ope;
        }

        private void Reset(bool clearMemory = false)
        {
            if (clearMemory) memory = 0;

            number1 = 0;
            number2 = 0;
            calculated = false;
            op = Operators.noOne;
        }

        private void Memory_Click(object sender, EventArgs e)
        {
            Show(memory);
            typed = false;
        }

        private void MemoryAdd_Click(object sender, EventArgs e)
        {
            memory += display;
            typed = false;
        }

        private void MemorySub_Click(object sender, EventArgs e)
        {
            memory -= display;
            typed = false;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (!typed) Reset(true);

            Show(0);
            typed = false;
            common = false;
            commonInTheStack = false;
        }

        private void Backslash_Click(object sender, EventArgs e)
        {
            String str = $"{display}";

            if (str.Length > 1)
            {
                Show(decimal.Parse(str.Remove(str.Length - 1)));
            }
            else
            {
                Show(0);
            }

            if (!Visor.Text.Contains(','))
            {
                common = false;
                commonInTheStack = false;
            }

            Reset();
            typed = true;
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            AddNumber(0);
        }

        private void One_Click(object sender, EventArgs e)
        {
            AddNumber(1);
        }

        private void Two_Click(object sender, EventArgs e)
        {
            AddNumber(2);
        }

        private void Tree_Click(object sender, EventArgs e)
        {
            AddNumber(3);
        }

        private void Four_Click(object sender, EventArgs e)
        {
            AddNumber(4);
        }

        private void Five_Click(object sender, EventArgs e)
        {
            AddNumber(5);
        }

        private void Six_Click(object sender, EventArgs e)
        {
            AddNumber(6);

        }

        private void Seven_Click(object sender, EventArgs e)
        {
            AddNumber(7);
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            AddNumber(8);
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            AddNumber(9);
        }

        private void Common_Click(object sender, EventArgs e)
        {
            if (!common) commonInTheStack = true;
        }

        private void Plus_Click(object sender, EventArgs e)
        {
            AddOperator(Operators.plus);
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            AddOperator(Operators.minus);
        }

        private void Times_Click(object sender, EventArgs e)
        {
            AddOperator(Operators.times);
        }

        private void Division_Click(object sender, EventArgs e)
        {
            AddOperator(Operators.division);
        }

        private void Sign_Click(object sender, EventArgs e)
        {
            Show(-display);
        }

        private void SquareRoot_Click(object sender, EventArgs e)
        {

        }

        private void Square_Click(object sender, EventArgs e)
        {
            if (display < 79228162514264337593543950334m)
            {
                Show(display * display);
            }
        }

        private void Equal_Click(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}