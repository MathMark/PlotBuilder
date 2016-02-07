using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using PlotBuilder.Properties;


namespace PlotBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {//

            InitializeComponent();
            p = new Pen((Color)Settings.Default["Color"]);
            p.DashStyle = (DashStyle)Settings.Default["DashStyle"];

            scale.Minimum = Convert.ToDecimal(0.5);
            scale.Maximum = 2;
            scale.Increment = Convert.ToDecimal(0.1);
            scale.Value = Convert.ToDecimal(0.5);

            label3.Text = "y = f(x)";
            secondFunctionBox.Hide();
            label4.Hide();
            groupBox1.Size = new Size(333, 57);

            Bitmap chosenColor = new Bitmap(10, 10);
            Graphics fill = Graphics.FromImage(chosenColor);
            SolidBrush brush = new SolidBrush(p.Color);
            fill.FillRectangle(brush, 0, 0, ColorButton.Width, ColorButton.Height);
            ColorButton.Image = chosenColor;
            fill.Dispose();
            statusDash.Text = "Solid";
            ChangeButton.Enabled = false;

            Draft = new Bitmap(sheet.Width, sheet.Height);
            builder = new Builder(Draft, pixelcoeff * Convert.ToSingle(scale.Value));
            builder.BuildNet();
            builder.BuildAxes();
            builder.BuildSection();
            builder.BuildCoordinates();
            Image = Draft;
        }
        const int pixelcoeff = 35;


        //Pen p = new Pen(Color.CadetBlue, 2);
        Pen p;

        public static List<Color> FunctionColors = new List<Color>();

        public static List<DashStyle> FunctionDashStyles = new List<DashStyle>();

        Bitmap Draft;

        Builder builder;

        List<Function> Functions = new List<Function>();

        Bitmap Image
        {
            get
            {
                return (Bitmap)sheet.Image;
            }
            set
            {
                sheet.Image = value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            builder.BuildNet();
            builder.BuildAxes();
            builder.BuildSection();
            builder.BuildCoordinates();
            Image = Draft;

        }

        private void Trigonometry_SelectedValueChanged(object sender, EventArgs e)
        {
            firstFunctionBox.Text = null;
            firstFunctionBox.Text = Trigonometry.SelectedItem.ToString();
        }


        private void Hyperbolical_SelectedValueChanged(object sender, EventArgs e)
        {
            firstFunctionBox.Text = null;
            //textBox1.Text = Hyperbolical.SelectedItem.ToString();
        }

        static int offset = 10;
        private void sheet_MouseMove(object sender, MouseEventArgs e)
        {

            labelStatus.Show();
            double X = (Convert.ToDouble(e.X) - Convert.ToDouble(sheet.Width / 2))
                   / (pixelcoeff * Convert.ToDouble(scale.Value));
            double Y = -(Convert.ToDouble(e.Y) - Convert.ToDouble(sheet.Height / 2))
                / (pixelcoeff * Convert.ToDouble(scale.Value));
            labelStatus.Text = "X: " + String.Format("{0:0.00}", X) + "  Y: " + String.Format("{0:0.00}", Y);
            labelStatus.Location = new Point(e.X + offset, e.Y - offset);

        }

        private void sheet_MouseLeave(object sender, EventArgs e)
        {
            labelStatus.ResetText();
            labelStatus.Hide();
        }



        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p = new Pen(colorDialog1.Color, 2);
            }
        }

        private void scale_ValueChanged(object sender, EventArgs e)
        {
            Draft = new Bitmap(sheet.Width, sheet.Height);
            builder = new Builder(Draft, pixelcoeff * Convert.ToSingle(scale.Value));
            builder.Clear();
            builder.BuildNet();
            builder.BuildAxes();
            builder.BuildSection();
            builder.BuildCoordinates();
            Image = Draft;

            if (Functions.Count != 0)
            {
                foreach (Function function in Functions)
                {
                    Pen pen = new Pen(function.color, 2);
                    pen.DashStyle = function.LineStyle;
                    builder.DrawFunction(function);
                }
            }
            else
            {
                ;
            }

        }

        double b = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (firstFunctionBox.Text != string.Empty) b = (firstFunctionBox.Text[firstFunctionBox.Text.Length - 1]);
            if ((b >= 1040) && (b <= 1103)) firstFunctionBox.ResetText();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure that you want to quit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK) Application.Exit();
        }


        Bitmap ColorFunction;

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            try
            {
                Function function = new Function(new StringBuilder(firstFunctionBox.Text), p.Color, p.DashStyle, 'x');
                RPN_Box.ResetText();
                foreach (string symbol in function.RPNsequence)
                {
                    RPN_Box.Text += " " + symbol;
                }
                if (Functions.Contains(function) == true)//It works!
                {
                    return;
                }
                else
                {
                    Functions.Add(function);
                    builder.DrawFunction(function);
                    Image = Draft;
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error in syntax (1)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Error in syntax (2). Perhaps you enter a wrong symbol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArithmeticException)
            {
                MessageBox.Show("Function cannot exist with double argument ", "ErrorInSyntaxException", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool repetition = false;
            foreach (ListViewItem func in FunctionList.Items)
            {
                if (func.Text == firstFunctionBox.Text) repetition = true;
            }


            ColorFunction = new Bitmap(imageList1.ImageSize.Width, imageList1.ImageSize.Height);
            Graphics DrawColor = Graphics.FromImage(ColorFunction);
            SolidBrush color = new SolidBrush(p.Color);

            if (repetition != true)
            {
                DrawColor.FillRectangle(color, 0, 0, imageList1.ImageSize.Width, imageList1.ImageSize.Height);
                imageList1.Images.Add(ColorFunction);
                FunctionList.Items.Add(firstFunctionBox.Text, imageList1.Images.Count - 1);
                repetition = false;
            }

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Functions.Clear();
            RPN_Box.ResetText();
            ChangeButton.Enabled = false;
            FunctionList.Items.Clear();

            builder = new Builder(Draft, pixelcoeff * Convert.ToSingle(scale.Value));
            builder.Clear();
            builder.BuildNet();
            builder.BuildAxes();
            builder.BuildSection();
            builder.BuildCoordinates();

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            firstFunctionBox.Text = TrigonomentyBox.SelectedItem.ToString();
        }

        private void HyperbolicalBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            firstFunctionBox.Text = HyperbolicalBox.SelectedItem.ToString();
        }


        static Bitmap d = new Bitmap(100, 100);
        Graphics f = Graphics.FromImage(d);
        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.DashStyle = DashStyle.Solid;
            statusDash.Text = "Solid";
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {

            p.DashStyle = DashStyle.Dash;
            statusDash.Text = "Dash";
        }

        private void dashDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.DashStyle = DashStyle.DashDot;
            statusDash.Text = "Dash Dot";
        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.DashStyle = DashStyle.DashDotDot;
            statusDash.Text = "Dash Dot Dot";
        }


        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.DashStyle = DashStyle.Dot;
            statusDash.Text = "Dot";
        }

        private void explicitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label4.Hide();
            secondFunctionBox.Hide();
            groupBox1.Size = new System.Drawing.Size(333, 57);
            label3.Text = "y = f(x)";
            // Argument = 'x';
        }

        private void parametricToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Argument = 't';
            //groupBox1.Size = new System.Drawing.Size(333, 90);
            //label4.Show();
            //secondFunctionBox.Show();
            //label3.Text = "x = " + "\u03c6" + " (t)";
            //label4.Text = "y = " + "\u03c8" + " (t)";
            //parametricMode = true;
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FunctionList.SelectedItems.Count != 0)
            {
                ListViewItem item = FunctionList.SelectedItems[0];

                firstFunctionBox.Text = item.Text;

                ChangeButton.Enabled = true;
            }
            else
            {
                ;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            firstFunctionBox.Text = OtherFunctions.SelectedItem.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p = new Pen(colorDialog1.Color, 2);
                Settings.Default["Color"] = p.Color;

                switch (statusDash.Text)
                {
                    case "Solid":
                        p.DashStyle = DashStyle.Solid;
                        break;
                    case "Dash":
                        p.DashStyle = DashStyle.Dash;
                        break;
                    case "Dash Dot":
                        p.DashStyle = DashStyle.DashDot;
                        break;
                    case "Dash Dot Dot":
                        p.DashStyle = DashStyle.DashDotDot;
                        break;
                    case "Dot":
                        p.DashStyle = DashStyle.Dot;
                        break;
                }
                Settings.Default["DashStyle"] = p.DashStyle;
                Settings.Default.Save();

                Bitmap chosenColor = new Bitmap(10, 10);
                Graphics fill = Graphics.FromImage(chosenColor);
                SolidBrush brush = new SolidBrush(p.Color);
                fill.FillRectangle(brush, 0, 0, ColorButton.Width, ColorButton.Height);
                ColorButton.Image = chosenColor;
                fill.Dispose();


            }
        }


        private void button26_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "(");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "x");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, ")");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "1");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "2");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "3");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "4");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "5");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "6");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "7");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "8");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "9");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "0");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "\u03c0");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "e");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "/");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "*");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, ";");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "+");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "-");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "^");
        }

        //Main statements
        private void button3_Click(object sender, EventArgs e)//abs
        {
            Function.AddStatement(firstFunctionBox, "abs(");
        }

        private void button4_Click(object sender, EventArgs e)//sqrt
        {
            Function.AddStatement(firstFunctionBox, "sqrt(");
        }

        private void button37_Click(object sender, EventArgs e)//sign
        {
            Function.AddStatement(firstFunctionBox, "sign(");
        }

        private void button21_Click(object sender, EventArgs e)//log
        {
            Function.AddStatement(firstFunctionBox, "log(");
        }

        private void button8_Click(object sender, EventArgs e)//ln
        {
            Function.AddStatement(firstFunctionBox, "ln(");
        }

        private void button23_Click(object sender, EventArgs e)//lg
        {
            Function.AddStatement(firstFunctionBox, "lg(");
        }

        private void button40_Click(object sender, EventArgs e)//[]
        {
            Function.AddStatement(firstFunctionBox, "E(");
        }

        private void button39_Click(object sender, EventArgs e)//{}
        {
            Function.AddStatement(firstFunctionBox, "R(");
        }

        private void button38_Click(object sender, EventArgs e)//rem
        {
            Function.AddStatement(firstFunctionBox, "rem(");
        }

        //Trygonometry
        private void button46_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "sin(");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "cos(");
        }

        private void button44_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "tan(");
        }

        private void button49_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "cot(");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arcsin(");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arccos");
        }

        private void button45_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arctan(");
        }

        private void button48_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arccot");
        }

        private void button50_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "sec(");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "csc(");
        }

        private void button52_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arcsec(");
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arccsc(");
        }

        //Hyperbolical
        private void button20_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "sinh(");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "cosh(");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "tanh(");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "cth(");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arsinh(");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arcosh(");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "artanh(");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arcth(");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            firstFunctionBox.ResetText();
            RPN_Box.ResetText();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "sech(");
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "csch(");
        }

        private void button56_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arsech(");
        }

        private void button57_Click(object sender, EventArgs e)
        {
            Function.AddStatement(firstFunctionBox, "arcsch(");
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (firstFunctionBox.SelectionStart != 0)
            {
                var selectionIndex = firstFunctionBox.SelectionStart;
                firstFunctionBox.Text = firstFunctionBox.Text.Remove(firstFunctionBox.SelectionStart - 1, 1);
                firstFunctionBox.SelectionStart = selectionIndex--;
            }
        }



    }

    interface IBuilder
    {
        void BuildNet();
        void BuildSection();
        void BuildAxes();
        void BuildCoordinates();
        void DrawFunction(Function function);
        void Clear();
    }


    class Calculate
    {
        public static double Solve(Function function, double x)
        {
            Stack P = new Stack();
            double s = 0;
            double a, b;
            for (int i = 0; i < function.RPNsequence.Length; i++)
            {
                if (string.IsNullOrEmpty(function.RPNsequence[i])) continue;

                else
                {
                    if (char.IsDigit(function.RPNsequence[i][0]))
                    {
                        P.Push(function.RPNsequence[i]);
                    }
                    else if ((char.IsLetter(function.RPNsequence[i][0])) &&
                        (function.RPNsequence[i][0] != function.Argument) && (function.RPNsequence[i][0] != '\u03c0') &&
                        (function.RPNsequence[i][0] != 'e'))
                    {
                        double X = Convert.ToDouble(P.Pop());
                        switch (function.RPNsequence[i])
                        {
                            case "sqrt":
                                P.Push(Math.Sqrt(X));
                                break;
                            case "abs":
                                P.Push(Math.Abs(X));
                                break;
                            case "sin":
                                P.Push(Math.Sin(X));
                                break;
                            case "sinh":
                                P.Push(Math.Sinh(X));
                                break;
                            case "cosh":
                                P.Push(Math.Cosh(X));
                                break;
                            case "cos":
                                P.Push(Math.Cos(X));
                                break;
                            case "cth":
                                P.Push(1 / Math.Tanh(X));
                                break;
                            case "tanh":
                                P.Push(Math.Tanh(X));
                                break;
                            case "tan":
                                P.Push(Math.Tan(X));
                                break;
                            case "cot":
                                P.Push(1 / Math.Tan(X));
                                break;
                            case "аbs":
                                P.Push(Math.Abs(X));
                                break;
                            case "ln":
                                P.Push(Math.Log(X));
                                break;
                            case "arsinh":
                                P.Push(Math.Log(X + Math.Sqrt(X * X + 1)));
                                break;
                            case "arcsin":
                                P.Push(Math.Asin(X));
                                break;
                            case "arcosh":
                                P.Push(Math.Log(X + Math.Sqrt(X + 1) * Math.Sqrt(X - 1)));
                                break;
                            case "arccos":
                                P.Push(Math.Acos(X));
                                break;
                            case "artanh":
                                P.Push(Math.Log((X + 1) / (X - 1)) / 2);
                                break;
                            case "arccot":
                                P.Push(Math.Atan(-1 * X) + Math.PI / 2);
                                break;
                            case "arcth":
                                P.Push(Math.Log((X + 1) / (1 - X)) / 2);
                                break;
                            case "arctan":
                                P.Push(Math.Atan(X));
                                break;
                            case "log":
                                P.Push(Math.Log(Convert.ToDouble(P.Pop()), X));
                                break;
                            case "sign":
                                P.Push(Math.Sign(X));
                                break;
                            case "rem":
                                P.Push(Convert.ToDouble(P.Pop()) % X);
                                break;
                            case "sec":
                                P.Push(1 / Math.Cos(x));
                                break;
                            case "csc":
                                P.Push(1 / Math.Sin(x));
                                break;
                            case "arcsec":
                                P.Push(Math.Acos(1 / X));
                                break;
                            case "arccsc":
                                P.Push(Math.Asin(1 / X));
                                break;
                            case "sech":
                                P.Push(1 / Math.Cosh(x));
                                break;
                            case "csch":
                                P.Push(1 / Math.Sinh(x));
                                break;
                            case "arsech":
                                P.Push(Math.Log(1 / x + Math.Sqrt(1 / x + 1) * Math.Sqrt(1 / x - 1)));
                                break;
                            case "arcsch":
                                P.Push(Math.Log(1 / x + Math.Sqrt(1 / x * x + 1)));
                                break;


                        }
                    }
                    else
                    {
                        if (function.RPNsequence[i][0] == 'e') P.Push(Math.E);
                        else if (function.RPNsequence[i][0] == '\u03c0') P.Push(Math.PI);
                        else if (function.RPNsequence[i][0] == function.Argument) P.Push(x);
                        else
                        {
                            b = Convert.ToDouble(P.Pop());
                            a = Convert.ToDouble(P.Pop());
                            switch (function.RPNsequence[i][0])
                            {
                                ///binary operations
                                case '+':
                                    P.Push(a + b);
                                    break;
                                case '-':
                                    P.Push(a - b);
                                    break;
                                case '*':
                                    P.Push(a * b);
                                    break;
                                case '/':
                                    P.Push(a / b);
                                    s = b;
                                    break;
                                case '^':
                                    if (b > 1)
                                    {
                                        if ((a < 0) || (a > 0))
                                        {
                                            P.Push(Math.Pow(a, b));
                                        }
                                        else
                                        {
                                            P.Push(0);
                                        }
                                    }
                                    else if ((b < 1) && (b > 0))
                                    {
                                        if (a > 0)
                                        {
                                            P.Push(Math.Pow(a, b));
                                        }
                                        else if (a < 0)
                                        {
                                            if (s % 2 == 1)
                                            {
                                                P.Push(-Math.Pow(Math.Abs(a), b));
                                            }
                                            else P.Push(Math.Pow(a, b));
                                        }
                                        else
                                        {
                                            P.Push(0);
                                        }
                                    }
                                    else if (b == 1)
                                    {
                                        P.Push(a);
                                    }
                                    else if (b == 0)
                                    {
                                        if (a == 0)
                                        {
                                            P.Push(0);
                                        }
                                        else P.Push(1);
                                    }
                                    break;
                                case ';':
                                    P.Push(b);
                                    P.Push(a);
                                    break;
                                ///unary operations
                                case '~':
                                    P.Push(a);
                                    P.Push(-1 * Convert.ToDouble(b));
                                    break;
                                default:
                                    goto y;
                            }//end switch
                        }//end else
                    }
                }
            }
            y: return Convert.ToDouble(P.Pop());

        }
    }
    class Stack
    {
        public List<object> list = new List<object>();
        public void Push(object element)
        {
            list.Add(element);
        }
        public object Pop()
        {
            if (list.Count != 0)
            {
                object temp = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                return temp;
            }
            else return null;
        }
        public void DeleteElement()
        {
            list.RemoveAt(list.Count - 1);
        }
        public bool IsEmpty()
        {
            if (list.Count == 0) return true;
            else return false;
        }
        public object CopyElement()
        {
            if (list.Count != 0)
            {
                return list[list.Count - 1];
            }
            else return null;
        }

    }

    class Builder : IBuilder

    {
        public Graphics Painter;
        Bitmap Draft;
        float scale;

        public Builder(Bitmap Draft, float scale)
        {
            this.Draft = Draft;
            Painter = Graphics.FromImage(Draft);
            Painter.SmoothingMode = SmoothingMode.AntiAlias;
            this.scale = scale;
        }
        public void Clear()
        {
            Painter.Clear(Color.White);
        }
        public void BuildNet()
        {
            Pen penNet = new Pen(Color.WhiteSmoke);
            float start = Convert.ToSingle(Draft.Width / 2);
            for (float i = 0; i < Draft.Width; i += scale)
            {
                Painter.DrawLine(penNet, start + i, 0, start + i, Draft.Height);
                Painter.DrawLine(penNet, start - i, 0, start - i, Draft.Height);
            }
            start = Convert.ToSingle(Draft.Height / 2);
            for (float i = 0; i < Draft.Height / 2; i += scale)
            {
                Painter.DrawLine(penNet, 0, start + i, Draft.Width, start + i);
                Painter.DrawLine(penNet, 0, start - i, Draft.Width, start - i);
            }
        }

        public void BuildSection()
        {
            Pen pen = new Pen(Color.Black);
            //X
            float start = Convert.ToSingle(Draft.Width / 2);
            for (float i = 0; i < Draft.Width; i += scale)
            {
                Painter.DrawLine(pen, start + i, Convert.ToSingle(Draft.Height / 2) - 2, start + i, Convert.ToSingle(Draft.Height / 2) + 2);
                Painter.DrawLine(pen, start - i, Convert.ToSingle(Draft.Height / 2) - 2, start - i, Convert.ToSingle(Draft.Height / 2) + 2);
            }
            //Y
            start = Convert.ToSingle(Draft.Height / 2);
            for (float i = 0; i < Draft.Height; i += scale)
            {
                Painter.DrawLine(pen, Convert.ToSingle(Draft.Width / 2) - 2, start + i, Convert.ToSingle(Draft.Width / 2) + 2, start + i);
                Painter.DrawLine(pen, Convert.ToSingle(Draft.Width / 2) - 2, start - i, Convert.ToSingle(Draft.Width / 2) + 2, start - i);
            }


        }
        public void BuildAxes()
        {
            Pen pen = new Pen(Color.Black, 1);
            Painter.DrawLine(pen, Draft.Width / 2, 0, Draft.Width / 2, Draft.Height);
            Painter.DrawLine(pen, 0, Draft.Height / 2, Draft.Width, Draft.Height / 2);

        }
        public void BuildCoordinates()
        {
            Pen p = new Pen(Color.Gray);
            short w = 1, v = -1;
            Font font = new Font("Consolas", 7);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            SolidBrush drawBrush = new SolidBrush(Color.Brown);
            PointF P;
            PointF F;
            Painter.Clip = new Region(new Rectangle(0, Draft.Height - 20, Draft.Width, Draft.Height));
            Painter.Clear(Color.WhiteSmoke);
            string zero = "0";
            float k = Convert.ToSingle(Draft.Width / 2);
            for (float i = Convert.ToSingle(Draft.Width / 2) + scale; i < Draft.Width; i += scale)
            {
                P = new PointF(i - 3, Draft.Height - 15);
                F = new PointF(Draft.Width - i - 7, Draft.Height - 15);

                Painter.DrawString(w.ToString(), font, drawBrush, P);
                w++;
                Painter.DrawString(v.ToString(), font, drawBrush, F);
                v--;

            }
            P = new PointF(Draft.Width / 2 - 5, Draft.Height - 15);
            Painter.DrawString(zero, font, drawBrush, P);
            w = 1;
            v = -1;
            Painter.Clip = new Region(new Rectangle(0, 0, 20, Draft.Height));
            Painter.Clear(Color.WhiteSmoke);
            for (float i = Convert.ToSingle(Draft.Height / 2) + scale; i < Draft.Height; i += scale)
            {
                P = new PointF(1, i - 5);
                F = new PointF(5, Draft.Height - i - 7);
                Painter.DrawString(v.ToString(), font, drawBrush, P);
                v--;
                Painter.DrawString(w.ToString(), font, drawBrush, F);
                w++;
            }
            Painter.Clip = new Region(new Rectangle(15, 0, Draft.Width, Draft.Height - 15));
        }
        public void DrawFunction(Function function)
        {
            double prototype;
            Pen pen = new Pen(function.color, 2);
            pen.DashStyle = function.LineStyle;
            PointF[] coordinates;
            List<PointF> Coordinates = new List<PointF>();

            if (function.Argument == 'x')
            {
                for (double x = -Draft.Width / 2; x < Draft.Width / 2; x += 1)
                {
                    prototype = Calculate.Solve(function, x / scale);
                    if (prototype > Draft.Height / 2)
                    {
                        prototype = Draft.Height / 2;
                    }
                    else if (prototype < -Draft.Height / 2)
                    {
                        prototype = -Draft.Height / 2;
                    }
                    else if ((double.IsNaN(prototype)) || (double.IsInfinity(prototype)))
                    {
                        if (Coordinates.Count != 0)
                        {
                            coordinates = new PointF[Coordinates.Count];
                            coordinates = Coordinates.ToArray();
                            try
                            {
                                Painter.DrawLines(pen, coordinates);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                            Coordinates.Clear();
                        }
                        continue;
                    }
                    else
                    {
                        Coordinates.Add(new PointF(Convert.ToSingle(Draft.Width / 2 + x),
                            Convert.ToSingle(Draft.Height / 2 - scale * prototype)));
                    }
                }
                try
                {
                    if (Coordinates.Count != 0)
                    {
                        coordinates = new PointF[Coordinates.Count];
                        coordinates = Coordinates.ToArray();
                        Painter.DrawLines(pen, coordinates);
                    }
                }
                catch (OverflowException)
                {
                    ;
                }
                catch (ArgumentException)
                {
                    return;
                }
            }
            else
            {
                for (double x = -Draft.Width / 2; x < Draft.Width / 2; x += 1)
                {
                    prototype = Calculate.Solve(function, x / scale);

                    if ((double.IsNaN(prototype)) || (double.IsInfinity(prototype)))
                    {
                        if (Coordinates.Count != 0)
                        {
                            coordinates = new PointF[Coordinates.Count];
                            coordinates = Coordinates.ToArray();
                            Painter.DrawLines(pen, coordinates);
                            Coordinates.Clear();
                        }
                        continue;
                    }
                    else
                    {
                        Coordinates.Add(new PointF(Convert.ToSingle(Draft.Width / 2 + scale * prototype),
                                       Convert.ToSingle((Draft.Height / 2) - x)));
                    }
                }
                if (Coordinates.Count != 0)
                {
                    coordinates = new PointF[Coordinates.Count];
                    coordinates = Coordinates.ToArray();
                    Painter.DrawLines(pen, coordinates);
                }
            }

        }
        //public static void DrawGraphic(Pen pen, Graphics g, PictureBox sheet, string[] OutputLine_1, string[] OutputLine_2, double scale, char Argument)
        //{
        //    PointF[] coordinates;
        //    List<PointF> Coordinates = new List<PointF>();
        //    double parametric_1;
        //    double parametric_2;

        //    for (double x = -sheet.Width / 2; x < sheet.Width / 2; x += 1)
        //    {
        //        //parametric_1 = Calculate.Solve((string[])OutputLine_1.Clone(), x / scale, Argument);
        //        //parametric_2 = Calculate.Solve((string[])OutputLine_2.Clone(), x / scale, Argument);

        //       // if ((double.IsNaN(parametric_1)) || (double.IsInfinity(parametric_1))||
        //            //(double.IsNaN(parametric_2)) || (double.IsInfinity(parametric_2)))
        //        {
        //            if (Coordinates.Count != 0)
        //            {
        //                coordinates = new PointF[Coordinates.Count];
        //                coordinates = Coordinates.ToArray();
        //                g.DrawLines(pen, coordinates);
        //                Coordinates.Clear();
        //            }
        //            continue;
        //        }
        //        else
        //        {
        //            Coordinates.Add(new PointF(Convert.ToSingle(sheet.Width / 2 + scale*parametric_1),
        //                           Convert.ToSingle((sheet.Height / 2) - scale * parametric_2)));
        //        }

        //    }
        //    if (Coordinates.Count != 0)
        //    {
        //        coordinates = new PointF[Coordinates.Count];
        //        coordinates = Coordinates.ToArray();
        //        g.DrawLines(pen, coordinates);
        //    }
        //}



    }


}

