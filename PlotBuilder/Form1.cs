﻿using System;
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
            //scaletable.Text += "1 : " + scale.Value.ToString() + " cm";
            string[] trigonometry = { "sin(x)", "cos(x)", "tg(x)", "ctg(x)","arcsin(x)","arccos(x)","arctg(x)","arcctg(x)" };
            string[] hyperbolical = { "sinh(x)", "cosh(x)", "th(x)", "ch(x)","arsinh(x)","arcosh(x)","arth(x)","arch(x)" };
            for (int i = 0; i < trigonometry.Length; i++)
            {
                Trigonometry.Items.Add(trigonometry[i]);
                Hyperbolical.Items.Add(hyperbolical[i]);
            }

            radioButton1.Select();
        }
        const int pixelcoeff=35;
        public StringBuilder buf;
        string[] line;
        Pen p = new Pen(Color.Navy,2);
        bool argument = false;
        Graphics g;


        Build D;
        public void button1_Click(object sender, EventArgs e)
        {
            argument = false;
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] == 'x')
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

            g = sheet.CreateGraphics();
            g.Clip = new Region(new Rectangle(15,0,sheet.Width,sheet.Height-15));

            buf = new StringBuilder(textBox1.Text);
            line=new string[buf.Length];
            try
            {
                Calculate.ConvertToRPN(buf, ref line);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error in syntax (1)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto t;
            }
            //catch (NullReferenceException)
            {
               // MessageBox.Show("Error in syntax (2). Perhaps you enter a wrong letter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               // goto t;
            }
    

            g.SmoothingMode = SmoothingMode.AntiAlias;
            D = new Build();
            

            //if ((Hystory.Items.Count==0)||(Hystory.Items[Hystory.Items.Count - 1].ToString() != textBox1.Text))
            {
                if (radioButton1.Checked == true) D.DrawGraphic(p, g, sheet, line, pixelcoeff * Convert.ToDouble(scale.Value), true);
                else D.DrawGraphic(p, g, sheet, line, pixelcoeff * Convert.ToDouble(scale.Value), false);
            }


            bool repeat = false;
            for (int i = 0; i < Hystory.Items.Count; i++)
            {
                if (Hystory.Items[i].ToString() == textBox1.Text) repeat = true;
            }
            if (repeat != true)
            {
                Hystory.Items.Add(textBox1.Text);
                repeat = false;
            }

            g.Dispose();
        
          
        t: return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g = sheet.CreateGraphics();
            D = new Build();
            //MessageBox.Show("" + line[0]);
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            D.BuildNet(g, sheet, pixelcoeff*Convert.ToSingle(scale.Value));
            D.BuildAxes(g, sheet);
            D.BuildSection(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            D.DrawCoordinates(g, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
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
            Pen black = new Pen(Color.Black);
            D.BuildNet(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            D.BuildAxes(e.Graphics, sheet);
            D.BuildSection(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));

            D.DrawCoordinates(e.Graphics, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            if (textBox1.Text != "") D.DrawGraphic(p, e.Graphics, sheet, line, pixelcoeff*Convert.ToDouble(scale.Value), true);
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
        }

        private void sheet_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = "X:" + "\nY:";
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
            Graphics p = sheet.CreateGraphics();
            D = new Build();
            scaletable.Text = null;
            scaletable.Text += "1 : " + scale.Value.ToString() + " (cm)";
            p.Clear(Color.White);
            D.BuildNet(p, sheet, pixelcoeff*Convert.ToSingle(scale.Value));
            D.BuildAxes(p, sheet);
            D.BuildSection(p, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
            D.DrawCoordinates(p, sheet, pixelcoeff * Convert.ToSingle(scale.Value));
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
            if((b>=1040)&&(b<=1103))textBox1.Text="";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure that you want to quit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK) Application.Exit();
        }
    }
    class Calculate
    {
        public static void ConvertToRPN(StringBuilder buf, ref string[] line)
        {
            if (buf[0] == '-')
            {
                buf.Remove(0, 1);
                buf.Insert(0, '~');
            }
           // MessageBox.Show(""+buf[0]);
            string[] Functions = { "arsinh","sinh","arcsin", "sin","arcosh", "cosh","arccos", "cos","arch", "ch","arcctg", "ctg","arth", "th","arctg", "tg", "abs", "sqrt", "ln", "(-" };
            string[] substitutes = {"й","ш","в","с","р","к","у","м","б","о","з","г","н","п","ю","х","а","ь","л","(~" };
            for (int i = 0; i < Functions.Length; i++)
            {
                buf.Replace(Functions[i], substitutes[i]);
            }

            Stack S = new Stack(buf.Length);
            line = new string[buf.Length];

            int j = 0;


            ///Перевод выражения в польскую запись

            int e = 0;
            for (; j < buf.Length; j++)
            {
                if ((char.IsDigit(buf[j])) || (buf[j] == ',') 
                    ||(buf[j] == 'p') || (buf[j] == 'e')||(buf[j]=='x'))//в буфере число?
                {
                    line[e] += buf[j];
                    if ((j != buf.Length - 1) && (!char.IsDigit(buf[j + 1]))) e++;
                }
                else
                {
                    if (S.IsEmpty() == true)
                    {
                        S.Push(buf[j]);
                    }
                    else if (Calculate.priority(Convert.ToChar(S.CopyElement())) < Calculate.priority(buf[j])) //сравнение приоритетов операций
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
                        while (Calculate.priority(Convert.ToChar(S.CopyElement())) != 1)
                        {
                            line[e] += Convert.ToChar(S.Pop());
                            e++;
                        }
                        S.DeleteElement();
                    }
                    else
                    {
                        while (Calculate.priority(Convert.ToChar(S.CopyElement())) >= Calculate.priority(buf[j]))
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
                line[e] += Convert.ToChar(S.Pop());
            }
        }
        public static double Solve(string[] line,double x)
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
                     else
                     {
                         if (line[i][0] == 'e') P.Push(Math.E);
                         else if (line[i][0] == 'p') P.Push(Math.PI);
                         else if (line[i][0] == 'x') P.Push(x);
                         else
                         {
                             switch (line[i][0])
                             {
                                 ///binary operations
                                 case '+':
                                     b = Convert.ToDouble(P.Pop());
                                     a = Convert.ToDouble(P.Pop());
                                     P.Push(a + b);
                                     break;
                                 case '-':
                                     b = Convert.ToDouble(P.Pop());
                                     a = Convert.ToDouble(P.Pop());
                                     P.Push(a - b);
                                     break;
                                 case '*':
                                     b = Convert.ToDouble(P.Pop());
                                     a = Convert.ToDouble(P.Pop());
                                     P.Push(a * b);
                                     break;
                                 case '/':
                                     b = Convert.ToDouble(P.Pop());
                                     a = Convert.ToDouble(P.Pop());
                                     P.Push(a / b);
                                     break;
                                 case '^':
                                     b = Convert.ToDouble(P.Pop());
                                     a = Convert.ToDouble(P.Pop());
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
                                 case 'ь':
                                     P.Push(Math.Sqrt(Convert.ToDouble(P.Pop())));
                                     break;
                                 case 'с':
                                     P.Push(Math.Sin(Convert.ToDouble(P.Pop())));
                                     break;
                                 case'ш':
                                     P.Push(Math.Sinh(Convert.ToDouble(P.Pop())));
                                     break;
                                 case'к':
                                     P.Push(Math.Cosh(Convert.ToDouble(P.Pop())));
                                     break;
                                 case 'м':
                                     P.Push(Math.Cos(Convert.ToDouble(P.Pop())));
                                     break;
                                 case'п':
                                     P.Push(Math.Tanh(Convert.ToDouble(P.Pop())));
                                     break;
                                 case 'х':
                                     P.Push(Math.Tan(Convert.ToDouble(P.Pop())));
                                     break;
                                 case'о':
                                     P.Push(1/Math.Tanh(Convert.ToDouble(P.Pop())));
                                     break;
                                 case 'г':
                                    P.Push(1 / Math.Tan(Convert.ToDouble(P.Pop())));
                                     break;
                                 case 'а':
                                     P.Push(Math.Abs(Convert.ToDouble(P.Pop())));
                                     break;
                                 case 'л':
                                     P.Push(Math.Log(Convert.ToDouble(P.Pop())));
                                     break;
                                 case'й':
                                     b=Convert.ToDouble(P.Pop());
                                     P.Push(Math.Log(b + Math.Sqrt(b * b + 1)));
                                     break;
                                 case 'в':
                                     P.Push(Math.Asin(Convert.ToDouble(P.Pop())));
                                     break;
                                 case 'р':
                                     b = Convert.ToDouble(P.Pop());
                                     P.Push(Math.Log(b + Math.Sqrt(b + 1)*Math.Sqrt(x-1)));
                                     break;
                                 case 'у':
                                     P.Push(Math.Acos(Convert.ToDouble(P.Pop())));
                                     break;
                                 case'б':
                                     b = Convert.ToDouble(P.Pop());
                                     P.Push(Math.Log((x + 1) / (x - 1)) / 2);
                                     break;
                                 case'з':
                                     P.Push(Math.Atan(-1*Convert.ToDouble(P.Pop()))+Math.PI/2);
                                     break;
                                 case'н':
                                     b = Convert.ToDouble(P.Pop());
                                     P.Push(Math.Log((x + 1) / (1-x)) / 2);
                                     break;
                                 case'ю':///error
                                     P.Push(Math.Atan(Convert.ToDouble(P.Pop())));
                                     break;

                                 default: 
                                     goto y;
                             }
                         }
                     }
                 }
             }
             y:return Convert.ToDouble(P.Pop());
        }

        public static void Show(object[] line, RichTextBox r)
        {
            for (int i = 0; i < line.Length; i++)
            {
                r.Text += line[i] + " ";
            }
            r.Text += '\n';
        }
        public static short priority(char q)//ставит приоритет операции
        {
            if ((q == '~') || (q == 'й') || (q == 'ш') || (q == 'в') || (q == 'с')
                || (q == 'р') || (q == 'к') || (q == 'у') || (q == 'м')||(q=='б')||(q=='о')||(q=='з')||(q=='г')
                || (q == 'н') || (q == 'п') || (q == 'ю') || (q == 'х') || (q == 'а') || (q == 'ь') || (q == 'л')) return 5;
            else if (q == '^') return 4;
            else if ((q == '*') || (q == '/')) return 3;
            else if ((q == '+') || (q == '-')) return 2;
            else if (q == '(') return 1;
            else
            {
                Console.WriteLine("Error");
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
            list[0] = '\0';
            return temp;
        }
        public void DeleteElement()
        {
            for (int m = list.Length - 1; m >= 1; m--)
            {
                list[m] = list[m - 1];
            }
            list[0] = '\0';
        }
        public bool IsEmpty()
        {
            if (char.IsControl(Convert.ToChar(list[list.Length - 1]))) return true;
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
            PointF G;
            h.Clip = new Region(new Rectangle(0, sheet.Height-20, sheet.Width, sheet.Height));
            h.Clear(Color.WhiteSmoke);
            string l = "P";
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
        public void DrawGraphic(Pen pen, Graphics g, PictureBox sheet, string[] line, double scale, bool Y)
        {
            PointF point;
            PointF step;
            if (Y == true)
            {
                for (double x = -sheet.Width/2; x < sheet.Width/2; x += 0.5)
                {
                    try
                    {
                        point = new PointF(Convert.ToSingle(sheet.Width / 2 + x),
                           Convert.ToSingle((sheet.Height / 2) - scale * Calculate.Solve((string[])line.Clone(), x / scale)));
                        step = new PointF(Convert.ToSingle(sheet.Width / 2 + x + 0.5),
                            Convert.ToSingle((sheet.Height / 2) - scale * Calculate.Solve((string[])line.Clone(), (x + 0.5) / scale)));
                        g.DrawLine(pen, point, step);
                    }
                   catch (OverflowException)
                    {
                        continue;
                    }
                }
            }
            else
            {
                for (double x = -sheet.Width / 2; x < sheet.Width / 2; x += 0.5)
                {
                    try
                    {
                        g.DrawLine(pen, Convert.ToSingle((sheet.Height / 2) + scale *Calculate.Solve((string[])line.Clone(), x / scale) ),
                             Convert.ToSingle((sheet.Width / 2) + x),
                             Convert.ToSingle((sheet.Height / 2) + scale *Calculate.Solve((string[])line.Clone(), (x + 1) / scale) ),
                             Convert.ToSingle((sheet.Width / 2) + x + 1));
                    }
                    catch (OverflowException)
                    {
                        continue;
                    }
                }
            }
        }
        

    }
}

