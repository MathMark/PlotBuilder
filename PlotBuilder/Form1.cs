using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Threading;


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
            scale.Value = Convert.ToDecimal(2);
            radioButton1.Select();
            label3.Text = "y = f(x)";
            textBox2.Hide();
            label4.Hide();
            groupBox1.Size = new System.Drawing.Size(333, 57);
        }
        const int pixelcoeff=35;

        public StringBuilder buf;

        string[] OutputLine;

        string[] OutputLine_2;
        Pen p = new Pen(Color.CadetBlue,2);
        bool argument = false;


        Bitmap buffer;
        Graphics g;

        bool parametricMode = false;

        public static char Argument = 'x';


        List<string>list=new List<string>();


        Build D;
        public void button1_Click(object sender, EventArgs e)
        {

            if (list.Contains(textBox1.Text) == true) goto t;
            else
            {
                list.Add(textBox1.Text);
                argument = false;
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if (textBox1.Text[i] == Argument)
                    {
                        argument = true;
                        break;
                    }
                }
                if (argument != true)
                {
                    MessageBox.Show("You've forgotten to add argument");
                    goto t;
                }

               // g = sheet.CreateGraphics();
                
             

               buf = new StringBuilder(textBox1.Text);
                OutputLine = new string[buf.Length];
                try
               {
                    Calculate.ConvertToRPN(buf, ref OutputLine,Argument);
                    if (parametricMode == true)
                    {
                        buf = new StringBuilder(textBox2.Text);
                        OutputLine_2 = new string[buf.Length];
                        Calculate.ConvertToRPN(buf, ref OutputLine_2, Argument);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Error in syntax (1)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto t;
                }
                catch (NullReferenceException)
                {
                   MessageBox.Show("Error in syntax (2). Perhaps you enter a wrong symbol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto t;
               }

                //buffer = null;
                buffer = new Bitmap(sheet.Width, sheet.Height);
                g = Graphics.FromImage(buffer);

                g.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));
             // g.SmoothingMode = SmoothingMode.AntiAlias;
              D = new Build();
              
               if (parametricMode == true)
               {
                   g.Clear(Color.White);

                   D.BuildNet(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
                   D.BuildAxes(g, sheet);
                   D.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
                   D.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
                  // g.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));


                   D.DrawGraphic(p, g, sheet, OutputLine, OutputLine_2, pixelcoeff * Convert.ToDouble(scale.Value), Argument);

               }
               else D.DrawGraphic(p, g, sheet, OutputLine, pixelcoeff * Convert.ToDouble(scale.Value), Argument);



               sheet.Image = buffer;
               g.Dispose();


                bool repeat = false;
                if (parametricMode == true)
                {
                    for (int i = 0; i < Hystory.Items.Count; i++)
                    {
                        if (Hystory.Items[i].ToString() == "{ " + textBox1.Text + " | " + textBox2.Text + " }") repeat = true;
                    }

                }
                else
                {
                    for (int i = 0; i < Hystory.Items.Count; i++)
                    {
                        if (Hystory.Items[i].ToString() == textBox1.Text) repeat = true;
                    }
                }



                if (repeat != true)
                {
                    if (parametricMode == true)
                    {
                        Hystory.Items.Add("{ " + textBox1.Text + " | " + textBox2.Text + " }");
                    }
                    else Hystory.Items.Add(textBox1.Text);
                    repeat = false;
                }

            }
        
          
        t: return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list.Clear();
            //g = sheet.CreateGraphics();

            buffer = new Bitmap(sheet.Width, sheet.Height);
            g = Graphics.FromImage(buffer);



            D = new Build();

            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            D.BuildNet(g, sheet, pixelcoeff*Convert.ToSingle(scale.Value));
            D.BuildAxes(g, sheet);
            D.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            D.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));

            sheet.Image = buffer;
            g.Dispose();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            Rectangle r = sheet.RectangleToScreen(sheet.ClientRectangle);
            Bitmap b = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(r.Location, new Point(0, 0), r.Size);
            b.Save("111.jpg");
       
        }

        private void sheet_Paint(object sender, PaintEventArgs e)
        {
            D = new Build();
            e.Graphics.Clip = new Region(new Rectangle(15, 0, sheet.Width, sheet.Height - 15));
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Pen black = new Pen(Color.Black);
            D.BuildNet(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            D.BuildAxes(e.Graphics, sheet);
            D.BuildSection(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));

            D.DrawCoordinates(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            if (textBox1.Text != "") D.DrawGraphic(p, e.Graphics, sheet, OutputLine, pixelcoeff*Convert.ToDouble(scale.Value), Argument);
        }



        private void Trigonometry_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.Text = Trigonometry.SelectedItem.ToString();
        }

        private void scale_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void Hyperbolical_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.Text = Hyperbolical.SelectedItem.ToString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AboutBox1 b= new AboutBox1();
            if (b.ShowDialog() == DialogResult.OK) this.Close();
        }

        private void sheet_MouseMove(object sender, MouseEventArgs e)
        {
            double X = (Convert.ToDouble(e.X) - Convert.ToDouble(sheet.Width / 2))
                   / (pixelcoeff * Convert.ToDouble(scale.Value));
            double Y = -(Convert.ToDouble(e.Y) - Convert.ToDouble(sheet.Height / 2))
                / (pixelcoeff*Convert.ToDouble(scale.Value));
            label1.Text = "X:  " + String.Format("{0:0.00}", X) + "\nY:  " + String.Format("{0:0.00}", Y);
            label1.Location =new Point(e.X,e.Y);
        }

        private void sheet_MouseLeave(object sender, EventArgs e)
        {
            //label1.Text = "X:" + "\nY:";
            label1.ResetText();
            label1.Location = new Point(0, 0);
        }

        private void Hystory_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.Text = Hystory.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // Hystory.SelectedItem = null;
        }

        private void toolStripButton4_Click(object sender, EventArgs e, Pen p)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                 p = new Pen(colorDialog1.Color);
            }
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
            //Graphics p = sheet.CreateGraphics();
           buffer = new Bitmap(sheet.Width, sheet.Height);
            g = Graphics.FromImage(buffer);


            D = new Build();
            scaletable.Text = null;
            scaletable.Text += "1 : " + scale.Value.ToString() + " (cm)";
            g.Clear(Color.White);

            //DoubleBuffered = true;

            D.BuildNet(g, sheet, pixelcoeff*Convert.ToSingle(scale.Value));
            D.BuildAxes(g, sheet);
            D.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            D.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            if (textBox1.Text != "") D.DrawGraphic(p, g, sheet, OutputLine, pixelcoeff * Convert.ToDouble(scale.Value), Argument);

            sheet.Image = buffer;
            p.Dispose();

        }

        private void groupBox4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Выбор образа функции", groupBox4);
        }
        double b = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (char.(textBox1.Text[textBox1.Text.Length - 1])) MessageBox.Show("Hello");
            
            if(textBox1.Text!="") b = (textBox1.Text[textBox1.Text.Length - 1]);
            if((b>=1040)&&(b<=1103))textBox1.ResetText();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure that you want to quit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK) Application.Exit();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "y = f(x)";
            Argument = 'x';
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "x = f(y)";
            Argument = 'y';
        }

      
        private void parametricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label4.Hide();
            textBox2.Hide();
            groupBox4.Enabled = true;
            groupBox1.Size = new System.Drawing.Size(333, 57);
            if (radioButton1.Checked == true)
            {
                label3.Text = "y = f(x)";
                Argument = 'x';
            }
            else
            {
                label3.Text = "x = f(y)";
                Argument = 'y';
            }
            parametricMode = false;
        }

        private void parametricToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Argument = 't';
            groupBox1.Size = new System.Drawing.Size(333, 90);
            label4.Show();
            textBox2.Show();
            label3.Text = "x = " + "\u03c6" + " (t)";
            label4.Text = "y = " + "\u03c8" + " (t)";
            parametricMode = true;
            groupBox4.Enabled = false;
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
            string function="";

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
                        //MessageBox.Show(""+buf[j]);
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
                    else if (Calculate.priority(Convert.ToString(S.CopyElement())) < Calculate.priority(buf[j].ToString())) //сравнение приоритетов операций
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
                        while (Calculate.priority(Convert.ToString(S.CopyElement())) != 1)
                        {
                            line[e] += S.Pop().ToString();
                            e++;
                        }
                        S.DeleteElement();
                    }
                    else
                    {
                        while (Calculate.priority(Convert.ToString(S.CopyElement())) >= Calculate.priority(buf[j].ToString()))
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
            //string function="";
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
                             case "lg":
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
                                     P.Push(-1 * Convert.ToDouble(P.Pop()));
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
       static string[] functions = {"sqrt","abs","sin","cos","tan","cot","arcsin","arccos","arctan","arccot","sinh","cosh",
                                 "tanh","cth","arsinh","arcosh","artanh","arcth"};
        public static short priority(string q)//ставит приоритет операции
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
            //if (char.IsControl(Convert.ToChar(list[list.Length - 1]))) return true;
            //if (string.IsNullOrEmpty(list[list.Length - 1].ToString())) return true;
            else return false;
        }
        public object CopyElement()
        {
            object temp = list[list.Length-1];
            return temp;
        }

    }
    class Build
    {
        public void BuildNet(Graphics g, PictureBox sheet, float scale)//рассчитан на квадратный лист
        {
            Pen penNet = new Pen(Color.WhiteSmoke);
            for (float i = Convert.ToSingle(sheet.Height / 2); i < sheet.Height; i += scale)
            {
                g.DrawLine(penNet, 0, i, Convert.ToSingle(sheet.Width), i);
                g.DrawLine(penNet, i, 0, i, Convert.ToSingle(sheet.Height));
            }
            for (float i = Convert.ToSingle(sheet.Width / 2); i >= 0; i -= scale)
            {
                g.DrawLine(penNet, 0, i, Convert.ToSingle(sheet.Width), i);
                g.DrawLine(penNet, i, 0, i, Convert.ToSingle(sheet.Height));
            }
        }

        public void BuildSection(Graphics g,PictureBox sheet, float scale)
        {
            
            
                Pen pen = new Pen(Color.Black);
                for (float i = Convert.ToSingle(sheet.Height / 2); i < sheet.Height; i += scale)
                {
                    g.DrawLine(pen, Convert.ToSingle(sheet.Width / 2) - 2, i, Convert.ToSingle(sheet.Width / 2) + 2, i);
                    g.DrawLine(pen, i, Convert.ToSingle(sheet.Height / 2) + 2, i, Convert.ToSingle(sheet.Height / 2) - 2);

                }
                for (float i = Convert.ToSingle(sheet.Width / 2); i >= 0; i -= scale)
                {
                    g.DrawLine(pen, Convert.ToSingle(sheet.Width / 2) - 2, i, Convert.ToSingle(sheet.Width / 2) + 2, i);
                    g.DrawLine(pen, i, Convert.ToSingle(sheet.Height / 2) + 2, i, Convert.ToSingle(sheet.Height / 2) - 2);
                }
            

        }
        public void BuildAxes(Graphics g, PictureBox sheet)
        {
            Pen pen = new Pen(Color.Black, 1);
            g.DrawLine(pen, sheet.Width / 2, 0, sheet.Width / 2, sheet.Height);
            g.DrawLine(pen, 0, sheet.Height / 2, sheet.Width, sheet.Height / 2);   

        }
        public void DrawCoordinates(Graphics h,PictureBox sheet,float scale)
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
               // G = new PointF(k - 3, sheet.Height - 15);
                F = new PointF(sheet.Width - i-7, sheet.Height-15);
                //h.DrawString(l, font, drawBrush, G);
                h.DrawString(w.ToString(), font, drawBrush, P);
                w++;
                h.DrawString(v.ToString(), font, drawBrush, F);
                v--;
                //k += Convert.ToSingle((Math.PI / 2)*scale);
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
        public void DrawGraphic(Pen pen, Graphics g, PictureBox sheet, string[] line, double scale, char Argument)
        {
            PointF point;
            PointF step;
            if (Argument=='x')
            {
                for (double x = -sheet.Width/2; x < sheet.Width/2; x += 0.5)
                {
                    try
                    {
                        point = new PointF(Convert.ToSingle(sheet.Width / 2 + x),
                           Convert.ToSingle((sheet.Height / 2) - scale * Calculate.Solve((string[])line.Clone(), x / scale, Argument)));
                        step = new PointF(Convert.ToSingle(sheet.Width / 2 + x + 0.5),
                            Convert.ToSingle((sheet.Height / 2) - scale * Calculate.Solve((string[])line.Clone(), (x + 0.5) / scale, Argument)));
                        g.DrawLine(pen, point, step);
                    }
                    catch (OverflowException)
                    {
                        continue;
                    }
                    //catch (ArgumentException)
                    {
                        //continue;
                    }
                }
            }
            else
            {
                for (double x = -sheet.Width / 2; x < sheet.Width / 2; x += 0.5)
                {
                    try
                    {
                        g.DrawLine(pen, Convert.ToSingle((sheet.Height / 2) + scale *Calculate.Solve((string[])line.Clone(), x / scale,Argument) ),
                             Convert.ToSingle((sheet.Width / 2) + x),
                             Convert.ToSingle((sheet.Height / 2) + scale *Calculate.Solve((string[])line.Clone(), (x + 1) / scale,Argument) ),
                             Convert.ToSingle((sheet.Width / 2) + x + 1));
                    }
                    catch (OverflowException)
                    {
                        continue;
                    }
                }
            }
        }
        public void DrawGraphic(Pen pen,Graphics g, PictureBox sheet, string[] OutputLine_1, string[] OutputLine_2, double scale,char Argument)
        {
            PointF point;
            PointF step;
            for (double x = -sheet.Width / 2; x < sheet.Width / 2; x += 0.5)
            {
                try
                {
                    point = new PointF(Convert.ToSingle(sheet.Width / 2 + scale*Calculate.Solve((string[])OutputLine_1.Clone(),x/scale,Argument)),
                       Convert.ToSingle((sheet.Height / 2) - scale * Calculate.Solve((string[])OutputLine_2.Clone(), x / scale, Argument)));
                    step = new PointF(Convert.ToSingle(sheet.Width / 2 + scale * Calculate.Solve((string[])OutputLine_1.Clone(), (x + 1) / scale, Argument)),
                       Convert.ToSingle((sheet.Height / 2) - scale * Calculate.Solve((string[])OutputLine_2.Clone(), (x + 1) / scale, Argument)));
                    g.DrawLine(pen, point, step);
                }
                catch (OverflowException)
                {
                    continue;
                }
            }
        }
        

    }
}

