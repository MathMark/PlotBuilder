using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PlotBuilder
{
    class Function
    {

        static string[] operators = {"~","sqrt","abs","sin","cos","tan","cot","arcsin","arccos","arctan","arccot","sinh","cosh",
                                 "tanh","cth","arsinh","arcosh","artanh","arcth","ln","log","sign","rem",
            "sec","csc","arcsec","arcsc","sech","csch","arsech","arcsch"};

        public static short GetPriority(string @operator)//returnes priority of function
        {

            for (byte i = 0; i < operators.Length; i++)
            {
                if (@operator == operators[i]) return 5;
            }
            if (@operator == "^") return 4;
            else if ((@operator == "*") || (@operator == "/")) return 3;
            else if ((@operator == "+") || (@operator == "-")) return 2;
            else if (@operator == "(") return 1;
            else
            {
                //Console.WriteLine("Error");
                return 0;//e.g. '\0' -> 0
            }

        }

        public StringBuilder name;//e.g. sin(x)+5*x^2
        public string[] RPNsequence;//x sin x 2 ^ 5 * +
        public Color color;
        public DashStyle LineStyle;
        public char Argument;
        public Function(StringBuilder name, Color color, DashStyle LineStyle, char Argument)
        {
            this.name = name;
            RPNsequence = new string[name.Length];
            this.color = color;
            this.LineStyle = LineStyle;
            this.Argument = Argument;
            ConvertToRPN();
        }
        public static void AddStatement(TextBox line, string insertText)
        {
            var selectionIndex = line.SelectionStart;
            line.Text = line.Text.Insert(selectionIndex, insertText);
            line.SelectionStart = selectionIndex + insertText.Length;
        }

        private void ConvertToRPN()
        {
            if (name[0] == '-')
            {
                name.Remove(0, 1);
                name.Insert(0, '~');
            }
            name.Replace("(-", "(~");


            Stack S = new Stack();
            RPNsequence = new string[name.Length];

            int j = 0;

            ///Перевод выражения в польскую запись
            string function = string.Empty;


            int e = 0;
            for (; j < name.Length; j++)
            {
                if ((char.IsDigit(name[j])) || (name[j] == '\u03c0')
                    || (name[j] == 'e') || (name[j] == Argument))//в буфере число?
                {
                    RPNsequence[e] += name[j];
                    if ((j != name.Length - 1) && (!char.IsDigit(name[j + 1]))) e++;
                }
                else if ((char.IsLetter(name[j])) && (name[j] != Argument) && (name[j] != '\u03c0') && (name[j] != 'e'))
                {
                    while (char.IsLetter(name[j]))
                    {
                        function += name[j];
                        j++;
                    }
                    j--;
                    S.Push(function);
                    function = string.Empty;
                }
                else if (name[j] == ';')
                {
                    continue;
                }
                else
                {
                    if (S.IsEmpty() == true)
                    {
                        S.Push(name[j]);
                    }
                    else if (GetPriority(Convert.ToString(S.CopyElement())) < GetPriority(name[j].ToString())) //сравнение приоритетов операций
                    {
                        S.Push(name[j]);
                    }
                    ///
                    else if (name[j] == '(')
                    {
                        S.Push(name[j]);
                    }
                    else if (name[j] == ')')
                    {
                        while (GetPriority(Convert.ToString(S.CopyElement())) != 1)
                        {
                            RPNsequence[e] += S.Pop().ToString();
                            e++;
                        }
                        S.DeleteElement();
                    }
                    else
                    {
                        while (GetPriority(Convert.ToString(S.CopyElement())) >= GetPriority(name[j].ToString()))
                        {
                            RPNsequence[e] += S.Pop().ToString();
                            e++;
                        }
                        S.Push(name[j]);
                    }


                }

            }

            while (S.IsEmpty() != true)
            {
                e++;
                RPNsequence[e] += S.Pop().ToString();
            }

        }

        public override bool Equals(object inputFunction)
        {
            if((inputFunction is Function)&&(inputFunction!=null))
            {
                Function tempInputFunction = (Function)inputFunction;
                if(this.name.Equals(tempInputFunction.name))
                {
                    return true;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        //public static bool operator ==(Function firstFunction, Function secondFunction)
        //{
        //    if(firstFunction.name==secondFunction.name)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //public static bool operator !=(Function firstFunction, Function secondFunction)
        //{
        //    if (firstFunction.name != secondFunction.name)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
