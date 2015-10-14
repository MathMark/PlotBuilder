using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace PlotBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            fill.FillRectangle(brush, 0, 0, ColorLabel.Width, ColorLabel.Height);
            ColorLabel.Image = chosenColor;
            fill.Dispose();

        }
        const  int pixelcoeff=35;


        public StringBuilder buf;

         static string[] OutputLine;

         static string[] OutputLine_2;

        Pen p = new Pen(Color.CadetBlue,2);

        public static List<Color> FunctionColors = new List<Color>();

        public static List<DashStyle> FunctionDashStyles = new List<DashStyle>();

        Graphics g;
<<<<<<< HEAD
        
        List<string> list = new List<string>();
=======
>>>>>>> Refactoring

        bool parametricMode = false;

        public static char Argument = 'x';
    

        List<string>list=new List<string>();


        private void button2_Click(object sender, EventArgs e)
        {
            
            list.Clear();
            g = sheet.CreateGraphics();

            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Builder.BuildNet(g, sheet, pixelcoeff*Convert.ToSingle(scale.Value));
            Builder.BuildAxes(g, sheet);
            Builder.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            Builder.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));

        }

        private void sheet_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));

            Builder.BuildNet(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            Builder.BuildAxes(e.Graphics, sheet);
            Builder.BuildSection(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));

            Builder.DrawCoordinates(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            e.Graphics.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (list.Count!=0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    buf =new StringBuilder( list[i].ToString());
                    Calculate.ConvertToRPN(buf, ref OutputLine, Argument);
                    Pen pen = new Pen(FunctionColors[i],2);
                    pen.DashStyle = FunctionDashStyles[i];
                    Builder.DrawGraphic(pen, e.Graphics, sheet, OutputLine, pixelcoeff * Convert.ToDouble(scale.Value), Argument);
                }
            }
            else
            {
                ;
            }
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
            labelStatus.Location = new Point(e.X+offset, e.Y-offset);

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
                p=new Pen(colorDialog1.Color,2);
            }
        }

        private void scale_ValueChanged(object sender, EventArgs e)
        {
            Graphics g = sheet.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(Color.White);

            Builder.BuildNet(g, sheet, pixelcoeff*Convert.ToSingle(scale.Value));
            Builder.BuildAxes(g, sheet);
            Builder.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            Builder.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));

            g.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));

            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    buf = new StringBuilder(list[i].ToString());
                    Calculate.ConvertToRPN(buf, ref OutputLine, Argument);
                    Pen pen = new Pen(FunctionColors[i],2);
                    pen.DashStyle=FunctionDashStyles[i];
                    Builder.DrawGraphic(pen, g, sheet, OutputLine, pixelcoeff * Convert.ToDouble(scale.Value), Argument);
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
            if(firstFunctionBox.Text!=string.Empty) b = (firstFunctionBox.Text[firstFunctionBox.Text.Length - 1]);
            if((b>=1040)&&(b<=1103))firstFunctionBox.ResetText();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure that you want to quit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK) Application.Exit();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "x = f(y)";
            Argument = 'y';
        }


        Bitmap ColorFunction;
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (list.Contains(firstFunctionBox.Text) == true)
            {
                return;
            } 
            else
            {
                list.Add(firstFunctionBox.Text);
                FunctionColors.Add(p.Color);
                FunctionDashStyles.Add(p.DashStyle);
                g = sheet.CreateGraphics();
                buf = new StringBuilder(firstFunctionBox.Text);
                OutputLine = new string[buf.Length];
                try
                {
                    Calculate.ConvertToRPN(buf, ref OutputLine, Argument);
                    if (parametricMode == true)
                    {
                        buf = new StringBuilder(secondFunctionBox.Text);
                        OutputLine_2 = new string[buf.Length];
                        Calculate.ConvertToRPN(buf, ref OutputLine_2, Argument);
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
                catch(Exception)
                {
                    MessageBox.Show("You have forgotten to close brackets!","ErrorInSyntaxException",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }

                g.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (parametricMode == true)
                {
                    g.Clear(Color.White);

                    Builder.BuildNet(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
                    Builder.BuildAxes(g, sheet);
                    Builder.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
                    Builder.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
                    g.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));


                    Builder.DrawGraphic(p, g, sheet, OutputLine, OutputLine_2, pixelcoeff * Convert.ToDouble(scale.Value), Argument);

                }
                else Builder.DrawGraphic(p, g, sheet, OutputLine, pixelcoeff * Convert.ToDouble(scale.Value), Argument);


                bool repeat = false;
                if (parametricMode == true)
                {
                    for (int i = 0; i < FunctionList.Items.Count; i++)
                    {
                        if (FunctionList.Items[i].ToString() == "{ " + firstFunctionBox.Text + " | " + secondFunctionBox.Text + " }") repeat = true;
                    }

                }
                else
                {
                    foreach(ListViewItem function in FunctionList.Items)
                    {
                        if (function.Text == firstFunctionBox.Text) repeat = true;
                    }
                }

                ColorFunction = new Bitmap(imageList1.ImageSize.Width, imageList1.ImageSize.Height);
                Graphics DrawColor = Graphics.FromImage(ColorFunction);
                SolidBrush color = new SolidBrush(p.Color);

                if (repeat != true)
                {
                    if (parametricMode == true)
                    {
                        DrawColor.FillRectangle(color, 0, 0, imageList1.ImageSize.Width, imageList1.ImageSize.Height);
                        imageList1.Images.Add(ColorFunction);
                        FunctionList.Items.Add(firstFunctionBox.Text+" | "+secondFunctionBox.Text,imageList1.Images.Count-1);
                    }
                    else
                    {
                        DrawColor.FillRectangle(color, 0, 0, imageList1.ImageSize.Width, imageList1.ImageSize.Height);
                        imageList1.Images.Add(ColorFunction);
                        FunctionList.Items.Add(firstFunctionBox.Text, imageList1.Images.Count-1);
                    }
                    repeat = false;
                }

            }

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            list.Clear();
            FunctionColors.Clear();
            FunctionDashStyles.Clear();
            FunctionList.Items.Clear();
            g = sheet.CreateGraphics();

            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Builder.BuildNet(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            Builder.BuildAxes(g, sheet);
            Builder.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            Builder.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));

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
            Argument = 'x';
            parametricMode = false;
        }

        private void parametricToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Argument = 't';
            groupBox1.Size = new System.Drawing.Size(333, 90);
            label4.Show();
            secondFunctionBox.Show();
            label3.Text = "x = " + "\u03c6" + " (t)";
            label4.Text = "y = " + "\u03c8" + " (t)";
            parametricMode = true;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p = new Pen(colorDialog1.Color, 2);
                Bitmap chosenColor = new Bitmap(10, 10);
                Graphics fill = Graphics.FromImage(chosenColor);
                SolidBrush brush = new SolidBrush(p.Color);
                fill.FillRectangle(brush, 0, 0, ColorLabel.Width, ColorLabel.Height);
                ColorLabel.Image = chosenColor;
                fill.Dispose();
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FunctionList.SelectedItems.Count != 0)
            {
                ListViewItem item = FunctionList.SelectedItems[0];
                firstFunctionBox.Text = null;
                firstFunctionBox.Text = item.Text;
            }
            else
            {
                ;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    class Calculate
    {
        public static void ConvertToRPN(StringBuilder buf, ref string[] line,char Argument)
        {
            if (buf[0] == '-')
            {
                buf.Remove(0, 1);
                buf.Insert(0, '~');
            }
            buf.Replace("(-", "(~");
            

            Stack S = new Stack(buf.Length);
            line = new string[buf.Length];

            int j = 0;

            ///Перевод выражения в польскую запись
            string function=string.Empty;


            int e = 0;
            for (; j < buf.Length; j++)
            {
                if ((char.IsDigit(buf[j])) || (buf[j] == ',') 
                    ||(buf[j] == 'p') || (buf[j] == 'e')||(buf[j]==Argument))//в буфере число?
                {
                    line[e] += buf[j];
                    if ((j != buf.Length - 1) && (!char.IsDigit(buf[j + 1]))) e++;
                }
                else if((char.IsLetter(buf[j]))&&(buf[j]!=Argument)&&(buf[j]!='p')&&(buf[j]!='e'))
                {
                    while(char.IsLetter(buf[j]))
                    {
                        function += buf[j];
                        j++;
                    }
                    j--;
                    S.Push(function);
                    function = "";
                }
                else
                {
                    if (S.IsEmpty() == true)
                    {
                        S.Push(buf[j]);
                    }
                    else if (priority(Convert.ToString(S.CopyElement())) <priority(buf[j].ToString())) //сравнение приоритетов операций
                    {
                        S.Push(buf[j]);
                    }
                    ///
                    else if (buf[j] == '(')
                    {
                        S.Push(buf[j]);
                    }
                    else if (buf[j] == ')')
                    {
                        while (priority(Convert.ToString(S.CopyElement())) != 1)
                        {
                            line[e] += S.Pop().ToString();
                            e++;
                        }
                        S.DeleteElement();
                    }
                    else
                    {
                        while (priority(Convert.ToString(S.CopyElement())) >= priority(buf[j].ToString()))
                        {
                            line[e] += S.Pop().ToString();
                            e++;
                        }
                        S.Push(buf[j]);
                    }


                }

            }

            
                while (S.IsEmpty() != true)
                {
                    e++;
                    line[e] += S.Pop().ToString();
                }
            
        }
        public static double Solve(string[] line,double x,char Argument)
        {
            Stack P = new Stack(10);
             double a,b;
             for (int i = 0; i < line.Length; i++)
             {
                 if (string.IsNullOrEmpty(line[i]))continue;
                    
                 else
                 {
                     if (char.IsDigit(line[i][0]))
                     {
                          P.Push(line[i]);
                     }
                     else if((char.IsLetter(line[i][0]))&&(line[i][0]!=Argument)&&(line[i][0]!='p')&&(line[i][0]!='e'))
                     {
                         double X = Convert.ToDouble(P.Pop());
                         switch (line[i])
                         {
                             case"sqrt":
                                 P.Push(Math.Sqrt(X));
                                     break;
                            case "abs":
                                P.Push(Math.Abs(X));
                                break;
                             case"sin":
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
                             case"cth":
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
                                     P.Push(Math.Log(X+ Math.Sqrt(X * X + 1)));
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

                         }
                     }
                     else
                     {
                         if (line[i][0] == 'e') P.Push(Math.E);
                         else if (line[i][0] == 'p') P.Push(Math.PI);
                         else if (line[i][0] == Argument) P.Push(x);
                         else
                         {
                             b = Convert.ToDouble(P.Pop());
                             a = Convert.ToDouble(P.Pop());
                             switch (line[i][0])
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
                                     break;
                                 case '^':
                                     if ((a < 0)&&(b<1))
                                     {
                                         a = -1 * a;
                                         P.Push(-Math.Pow(a, b));
                                     }
                                     else P.Push(Math.Pow(a, b));
                                     break;

                                 ///unary operations
                                 case '~':
                                     P.Push(-1 * Convert.ToDouble(b));
                                     break;
                                 default: 
                                     goto y;
                             }//end switch
                         }//end else
                     }
                 }
             }
             y:return Convert.ToDouble(P.Pop());
        }
       static string[] functions = {"~","sqrt","abs","sin","cos","tan","cot","arcsin","arccos","arctan","arccot","sinh","cosh",
                                 "tanh","cth","arsinh","arcosh","artanh","arcth"};
        public static short priority(string q)//returnes priority of function
        {

            for (byte i = 0; i < functions.Length; i++)
            {
                if (q == functions[i]) return 5;
            }
                if (q == "^") return 4;
                else if ((q == "*") || (q == "/")) return 3;
                else if ((q == "+") || (q == "-")) return 2;
                else if (q == "(") return 1;
                else
                {
                    //Console.WriteLine("Error");
                    return 0;//e.g. '\0' -> 0
                }

        }
    }
    class Stack
    {
        object[] list = new object[0];
        public Stack(int length)
        {
            if ((length > 0) && (length <= 20)) Array.Resize<object>(ref list, length);//Dinamic Array
            else Array.Resize<object>(ref list, 20);
        }
        public int Length()
        {
            return list.Length;
        }
        public void Push(object element)
        {
            for (int i = 1; i < list.Length; i++)
            {
                list[i - 1] = list[i];
            }
            list[list.Length - 1] = element;
        }
        public object Pop()
        {
            object temp = list[list.Length - 1];
            for (int i = list.Length - 1; i >= 1; i--)
            {
                list[i] = list[i - 1];
            }
            list[0] = null;
            return temp;
        }
        public void DeleteElement()
        {
            for (int m = list.Length - 1; m >= 1; m--)
            {
                list[m] = list[m - 1];
            }
            list[0] = null;
        }
        public bool IsEmpty()
        {
           if( object.Equals(list[list.Length-1],null))return true;
            else return false;
        }
        public object CopyElement()
        {
            object temp = list[list.Length-1];
            return temp;
        }

    }
    class Builder
    {
        public static void BuildNet(Graphics g, PictureBox sheet, float scale)
        {
            Pen penNet = new Pen(Color.WhiteSmoke);
            float start = Convert.ToSingle(sheet.Width / 2);
            for (float i=0; i < sheet.Width; i += scale)
            {
                g.DrawLine(penNet, start+i, 0, start+i, sheet.Height);
                g.DrawLine(penNet, start - i, 0, start - i, sheet.Height);
            }
            start = Convert.ToSingle(sheet.Height / 2);
            for(float i=0;i<sheet.Height/2;i+=scale)
            {
                g.DrawLine(penNet, 0, start + i, sheet.Width, start + i);
                g.DrawLine(penNet, 0, start -i, sheet.Width, start - i);
            }
        }

        public static void BuildSection(Graphics g,PictureBox sheet, float scale)
        {
            Pen pen = new Pen(Color.Black);
            //X
            float start = Convert.ToSingle(sheet.Width / 2);
               for(float i=0;i<sheet.Width;i+=scale)
            {
                g.DrawLine(pen, start+i,Convert.ToSingle(sheet.Height/2)-2,start+i,Convert.ToSingle(sheet.Height/2)+2);
                g.DrawLine(pen, start - i, Convert.ToSingle(sheet.Height / 2) - 2, start - i, Convert.ToSingle(sheet.Height / 2) + 2);
            }
               //Y
            start = Convert.ToSingle(sheet.Height / 2);
            for(float i=0;i<sheet.Height;i+=scale)
            {
                g.DrawLine(pen,Convert.ToSingle(sheet.Width/2)-2,start+i, Convert.ToSingle(sheet.Width / 2) + 2,start+ i);
                g.DrawLine(pen, Convert.ToSingle(sheet.Width / 2) - 2, start - i, Convert.ToSingle(sheet.Width / 2) + 2, start - i);
            }
            

        }
        public static void BuildAxes(Graphics g, PictureBox sheet)
        {
            Pen pen = new Pen(Color.Black, 1);
            g.DrawLine(pen, sheet.Width / 2, 0, sheet.Width / 2, sheet.Height);
            g.DrawLine(pen, 0, sheet.Height / 2, sheet.Width, sheet.Height / 2);   

        }
        public static void DrawCoordinates(Graphics h,PictureBox sheet,float scale)
        {
            Pen p=new Pen(Color.Gray);
            short w=1, v = -1;
            Font font = new Font("Consolas", 7);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            SolidBrush drawBrush = new SolidBrush(Color.Brown);
            PointF P;
            PointF F;
            h.Clip = new Region(new Rectangle(0, sheet.Height-20, sheet.Width, sheet.Height));
            h.Clear(Color.WhiteSmoke);
            string zero = "0";
            float k = Convert.ToSingle(sheet.Width / 2);
            for (float i = Convert.ToSingle(sheet.Width / 2)+scale; i < sheet.Width; i += scale)
            {
                P = new PointF(i-3, sheet.Height-15);
                F = new PointF(sheet.Width - i-7, sheet.Height-15);

                h.DrawString(w.ToString(), font, drawBrush, P);
                w++;
                h.DrawString(v.ToString(), font, drawBrush, F);
                v--;

            }
            P = new PointF(sheet.Width / 2-5,sheet.Height-15);
            h.DrawString(zero, font, drawBrush, P);
            w=1;
            v=-1;
            h.Clip = new Region(new Rectangle(0, 0, 20, sheet.Height));
            h.Clear(Color.WhiteSmoke);
            for (float i = Convert.ToSingle(sheet.Height / 2)+scale; i < sheet.Height; i += scale)
            {
                P = new PointF(1, i-5);
                F = new PointF(5, sheet.Height-i-7);
                h.DrawString(v.ToString(), font, drawBrush, P);
                v--;
                h.DrawString(w.ToString(), font, drawBrush, F);
                w++;
            }
        }
        public static void DrawGraphic(Pen pen, Graphics g, PictureBox sheet, string[] line, double scale, char Argument)
        {
            double prototype;
            PointF[] coordinates;
            List<PointF> Coordinates = new List<PointF>();

            if (Argument == 'x')
            {
                for (double x = -sheet.Width/2; x < sheet.Width/2; x += 1)
                {
                    prototype = Calculate.Solve((string[])line.Clone(), x / scale, Argument);
                    if (prototype > sheet.Height / 2)
                    {
                        prototype = sheet.Height / 2;
                    }
                    if(prototype < -sheet.Height / 2)
                    {
                        prototype = -sheet.Height / 2;
                    }
                    if ((double.IsNaN(prototype)) || (double.IsInfinity(prototype)))
                    {
                        if (Coordinates.Count != 0)
                        {
                            coordinates = new PointF[Coordinates.Count];
                            coordinates = Coordinates.ToArray();
                            g.DrawLines(pen, coordinates);
                            Coordinates.Clear();
                        }
                        continue;
                    }
                    else
                    {
                        Coordinates.Add(new PointF(Convert.ToSingle(sheet.Width/2+x),
                            Convert.ToSingle(sheet.Height/2-scale * prototype)));
                    }
                }
                try
                {
                    if (Coordinates.Count != 0)
                    {
                        coordinates = new PointF[Coordinates.Count];
                        coordinates = Coordinates.ToArray();
                        g.DrawLines(pen, coordinates);
                    }
                }
                catch(OverflowException)
                {
                    ;
                }
            }
            else
            {
                for (double x = -sheet.Width / 2; x < sheet.Width / 2; x += 1)
                {
                    prototype = Calculate.Solve((string[])line.Clone(), x / scale, Argument);

                    if ((double.IsNaN(prototype)) || (double.IsInfinity(prototype)))
                    {
                        if (Coordinates.Count != 0)
                        {
                            coordinates = new PointF[Coordinates.Count];
                            coordinates = Coordinates.ToArray();
                            g.DrawLines(pen, coordinates);
                            Coordinates.Clear();
                        }
                        continue;
                    }
                    else
                    {
                        Coordinates.Add(new PointF(Convert.ToSingle(sheet.Width / 2 + scale*prototype),
                                       Convert.ToSingle((sheet.Height / 2) - x)));
                    }
                }
                if (Coordinates.Count != 0)
                {
                    coordinates = new PointF[Coordinates.Count];
                    coordinates = Coordinates.ToArray();
                    g.DrawLines(pen, coordinates);
                }
            }

        }
        public static void DrawGraphic(Pen pen, Graphics g, PictureBox sheet, string[] OutputLine_1, string[] OutputLine_2, double scale, char Argument)
        {
            PointF[] coordinates;
            List<PointF> Coordinates = new List<PointF>();
            double parametric_1;
            double parametric_2;

            for (double x = -sheet.Width / 2; x < sheet.Width / 2; x += 1)
            {
                parametric_1 = Calculate.Solve((string[])OutputLine_1.Clone(), x / scale, Argument);
                parametric_2 = Calculate.Solve((string[])OutputLine_2.Clone(), x / scale, Argument);

                if ((double.IsNaN(parametric_1)) || (double.IsInfinity(parametric_1))||
                    (double.IsNaN(parametric_2)) || (double.IsInfinity(parametric_2)))
                {
                    if (Coordinates.Count != 0)
                    {
                        coordinates = new PointF[Coordinates.Count];
                        coordinates = Coordinates.ToArray();
                        g.DrawLines(pen, coordinates);
                        Coordinates.Clear();
                    }
                    continue;
                }
                else
                {
                    Coordinates.Add(new PointF(Convert.ToSingle(sheet.Width / 2 + scale*parametric_1),
                                   Convert.ToSingle((sheet.Height / 2) - scale * parametric_2)));
                }

            }
            if (Coordinates.Count != 0)
            {
                coordinates = new PointF[Coordinates.Count];
                coordinates = Coordinates.ToArray();
                g.DrawLines(pen, coordinates);
            }
        }
        

    }
}

