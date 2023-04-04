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
                         "x^y", "√", "³√", "sin", "cos",
                         "tan", "log", "ln", "e^x", "10^x",
                         "x!", "mod", "|x|", "π", "e",
                         "sin⁻¹", "cos⁻¹", "tan⁻¹", "sinh", "cosh",
                         "tanh", "Ran", "Deg", "Rad", "Grad"};


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

            textBox1.Location = new Point(0, currentY);
            textBox1.Width = ((buttonWidth + padding) * 5) - padding;

            this.Width = textBox1.Width + padding * 2 - 4;
            this.Height = rowCount * (padding + buttonHeight) + padding;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            if (sciencificOperationsArr.Any(x => x == buttonText))
            {
                double calculatedResutl = equalButtonOperations();
                if (buttonText == "sin")
                    textBox1.Text = Math.Sin(Convert.ToDouble(calculatedResutl)).ToString();
                if (buttonText == "cos")
                    textBox1.Text = Math.Cos(Convert.ToDouble(calculatedResutl)).ToString();
            }
            else
            {
                StandartCalculator(buttonText);

            }
        }

        private void StandartCalculator(string buttonText)
        {
            switch (buttonText)
            {
                case "C":
                    textBox1.Text = "";
                    break;
                case "=":
                    try
                    {
                        equalButtonOperations();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hatalı ifade: " + ex.Message);
                    }
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

        private double equalButtonOperations()
        {
            char lastChar = textBox1.Text[textBox1.Text.Length - 1];
            if (!char.IsDigit(lastChar))
            {
                textBox1.Text=textBox1.Text.Substring(0,textBox1.Text.Length - 1);
            }
            DataTable table = new DataTable();
            var str =  textBox1.Text.Replace(',','.');
            table.Columns.Add("expression", typeof(string), str);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            double result = double.Parse((string)row["expression"]);
            textBox1.Text = result.ToString();
            return Convert.ToDouble(textBox1.Text);
        }

        public  string GetUntilOrEmpty( string text, string stopAt = "-")
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
    }
}