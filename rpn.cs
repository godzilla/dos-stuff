// http://www.vertex42.com/ExcelArticles/amortization-calculation.html
// r http://www.google.com/search?hl=en&q=amortization+formula&aq=f&aqi=g10&aql=&oq=&gs_rfai=


using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace rpn
{
    class Program
    {
        static Stack<double> fStack = new Stack<double>();

        static double GetPayment(double principle, double interest, double NumMonths)
        {

            double r = (interest/100) / 12;
            double rv = Math.Pow((1 + r), NumMonths);
            double top = rv * r;
            double botom = rv - 1;
            double payment = principle * (top / botom);
            return payment;

        }

        public static double Factorial(double val)
        {
            return val == 1 ? val : val * Factorial(val - 1);
        }
 




        static void DoOp(string op) 
        {

            double a;
            double b;
            double c;
            double d;
            switch (op)
            {
                case "+":
                    if (fStack.Count < 2)
                    {
                        Console.WriteLine("+ failed, stack count is " + fStack.Count.ToString());
                        return;
                    }
                    b = fStack.Pop();
                    a = fStack.Pop();
                    c = a + b;
                    fStack.Push(c);
                    break;

                case "-":
                    if (fStack.Count < 2)
                    {
                        Console.WriteLine("- failed, stack count is " + fStack.Count.ToString());
                        return;
                    }
                    b = fStack.Pop();
                    a = fStack.Pop();
                    c = a - b;
                    fStack.Push(c);
                    break;

                case "*":
                    if (fStack.Count < 2)
                    {
                        Console.WriteLine("* failed, stack count is " + fStack.Count.ToString());
                        return;
                    }
                    b = fStack.Pop();
                    a = fStack.Pop();
                    c = a * b;
                    fStack.Push(c);
                    break;

                case "/":
                    if (fStack.Count < 2)
                    {
                        Console.WriteLine("/ failed, stack count is " + fStack.Count.ToString());
                        return;
                    }
                    b = fStack.Pop();
                    a = fStack.Pop();
                    c = a / b;
                    fStack.Push(c);
                    break;

                case "!":
                    if (fStack.Count < 1)
                    {
                        Console.WriteLine("! failed, stack count is " + fStack.Count.ToString());
                        return;
                    }
                    a = fStack.Pop();
                    b = Factorial(a);
                    fStack.Push(b);
                    break;


                case "i":
                    if (fStack.Count < 1)
                    {
                        Console.WriteLine("i failed, stack count is " + fStack.Count.ToString());
                        return;
                    }
                    a = fStack.Pop();
                    b = 1 / a;
                    fStack.Push(b);
                    break;


                case "pow":
                    if (fStack.Count < 2)
                    {
                        Console.WriteLine("pow failed, stack count is " + fStack.Count.ToString());
                        return;
                    }
                    b = fStack.Pop();
                    a = fStack.Pop();
                    c = Math.Pow(a, b);
                    fStack.Push(c);
                    break;

                case "payment":
                    if (fStack.Count < 3)
                    {
                        Console.WriteLine("payment failed, stack count is " + fStack.Count.ToString());
                        Console.WriteLine("useage: 90000 6 180 payment ");
                        return;
                    }
                    // 90000 6 180 payment
                    c = fStack.Pop();  // num payments
                    b = fStack.Pop();  // interest
                    a = fStack.Pop();  // principle
                    d = GetPayment(a, b, c);
                    fStack.Push(d);
                    break;


                case "tire":
                    if (fStack.Count < 3)
                    {
                        Console.WriteLine("tire failed, stack count is " + fStack.Count.ToString());
                        Console.WriteLine("useage: 235 75 16 tire");
                        return;
                    }
                    // 90000 6 180 payment
                    c = fStack.Pop();  // width in mm
                    b = fStack.Pop();  // profile 
                    a = fStack.Pop();  // rim radius
                    d = c;
    
    
                    d =  (a/25.4)*(b/100)*2+c;
                    fStack.Push(d);
                    break;




    

                default:
                    Console.WriteLine("op(" + op + ") not supported");
                    break;
            }

        }


        static void ShowHelp()
        {

            Console.WriteLine("x y +                   simple math (+ 1 * /");
            Console.WriteLine("x i                     invert number 1/x");
            Console.WriteLine("x y pow                 power (x^y");
            Console.WriteLine("90000 6 180 payment     amortization (Principle Interest NumberOfMonths)");
            Console.WriteLine("235 75 16 tire          gives radius of tire in inches");



        }
       
        static void DoStack(string[] args) 
        {

            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }


            foreach (string s in args)
            {
                double d = double.NaN;
                if (double.TryParse(s, out d))
                {
                    fStack.Push(d);
                }
                else
                {
                    DoOp(s);
                }
            }

            foreach (double dd in fStack)
            {
                Console.WriteLine(dd.ToString());
            }
        }



        static void Main(string[] args)
        {
            try
            {
                DoStack(args);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }
    }
}

