using NCalc;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private static string[] sciencificOperationsArr =
            {
                 "x^y", "√", "³√", "sin", "cos",
                         "tan", "log", "ln", "e^x", "10^x",
                         "x!", "mod", "|x|", "π", "e",
                         "sin⁻¹", "cos⁻¹", "tan⁻¹", "sinh", "cosh",
                         "tanh", "Ran", "Deg", "Rad", "Grad"
            };
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateCalculatorButtons();

        }

        private void CreateCalculatorButtons()
        {
            string[] buttonTexts = { "7", "8", "9", "/", "C",
                         "4", "5", "6", "*", "±",
                         "1", "2", "3", "-", "(",
                         "0", ".", "=", "+", ")",
                         "√", "³√", "sin", "cos",
                         "tan", "log", "ln", "e^x", "10^x",
                         "x!", "mod", "|x|", "π", "e",
                         "sin⁻¹", "cos⁻¹", "tan⁻¹", "sinh", "cosh",
                         "tanh",  "Deg", "Rad"};


            int buttonWidth = 50;
            int buttonHeight = 50;
            int padding = 10;
            int currentX = 0;
            int currentY = 0;
            int rowCount = 1;
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                Button button = new Button();
                button.Text = buttonTexts[i];
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Location = new Point(currentX, currentY);
                button.Click += Button_Click;

                panel1.Controls.Add(button);

                currentX += buttonWidth + padding;

                if ((i + 1) % 5 == 0)
                {
                    rowCount++;
                    currentX = 0;
                    currentY += buttonHeight + padding;
                }
            }

            textBox1.Location = new Point(0, currentY+60);
            textBox1.Width = ((buttonWidth + padding) * 5) - padding;

            this.Width = textBox1.Width + 100 + padding * 2 - 4;
            this.Height = rowCount * (padding + buttonHeight) + padding + 120;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            if (sciencificOperationsArr.Any(x => x == buttonText))
            {
                double calculatedResutl = equalButtonOperations();
                ScienceficOperations(buttonText, calculatedResutl);
            }
            else
            {
                StandartCalculator(buttonText);

            }
        }

        private void ScienceficOperations(string buttonText, double calculatedResutl)
        {
            if (buttonText == "sin") textBox1.Text = Math.Sin(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "sinh") textBox1.Text = Math.Sinh(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "sin⁻¹") textBox1.Text = Math.Asin(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "cos") textBox1.Text = Math.Cos(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "cosh") textBox1.Text = Math.Cosh(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "cos⁻¹") textBox1.Text = Math.Acos(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "tan⁻¹") textBox1.Text = Math.Atan(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "mod")
            {
                if (textBox1.Text.Trim().Length <= 0) MessageBox.Show("Plesease give an number");
                else textBox1.Text += "%";
            }
            if (buttonText == "tan") textBox1.Text = Math.Tan(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "tanh") textBox1.Text = Math.Tanh(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "π") textBox1.Text = Math.PI.ToString();
            if (buttonText == "e") textBox1.Text = Math.E.ToString();
            if (buttonText == "log") textBox1.Text = Math.Log(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "ln") textBox1.Text = Math.Log(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "e^x") textBox1.Text = Math.Exp(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "10^x") textBox1.Text = Math.Pow(10, Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "|x|") textBox1.Text = Math.Abs(Convert.ToDouble(calculatedResutl)).ToString();
            if (buttonText == "x!") textBox1.Text = CalculateFactorial(Convert.ToInt32(calculatedResutl)).ToString();
            if (buttonText == "³√") textBox1.Text = Math.Pow(Convert.ToInt32(calculatedResutl), 1.0 / 3.0).ToString();
            if (buttonText == "√") textBox1.Text = Math.Pow(Convert.ToInt32(calculatedResutl), 1.0 / 2.0).ToString();
            if (buttonText == "Rad") textBox1.Text = (Convert.ToInt32(calculatedResutl) * (Math.PI / 180)).ToString();
            if (buttonText == "Deg") textBox1.Text = (Convert.ToInt32(calculatedResutl) * (180 / Math.PI)).ToString(); ;
        }

        private void StandartCalculator(string buttonText)
        {
            try
            {
                switch (buttonText)
                {
                    case "C":
                        textBox1.Text = "";
                        break;
                    case "=":
                        double a = equalButtonOperations();
                        //if (a == 0) MessageBox.Show("Please give an input");
                        break;
                    case "±":
                        if (!string.IsNullOrEmpty(textBox1.Text))
                        {
                            if (textBox1.Text.StartsWith("-"))
                                textBox1.Text = textBox1.Text.Substring(1);
                            else
                                textBox1.Text = "-" + textBox1.Text;
                        }
                        break;
                    default:
                        textBox1.Text += buttonText;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hatalı ifade: " + ex.Message);
            }
        }

        private double equalButtonOperations()
        {
            try
            {
                char lastChar = textBox1.Text[textBox1.Text.Length - 1];
                if (!char.IsDigit(lastChar))
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                }
                DataTable table = new DataTable();
                var str = textBox1.Text.Replace(',', '.');
                table.Columns.Add("expression", typeof(string), str);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                double result = double.Parse((string)row["expression"]);
                textBox1.Text = result.ToString();
                return Convert.ToDouble(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Düzgün gir lan");
                return 0;

            }

        }

        public string GetUntilOrEmpty(string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
        public int CalculateFactorial(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }
            else
            {
                return n * CalculateFactorial(n - 1);
            }
        }

        public class Sukro
        {
            public string Name { get; set; }
            public int Kol { get; set; } = 31;//cm
            public bool Damarlimi { get; set; } = true;
            public int Bacak { get; set; } = 56;//cm
        }
    }
}