using System;
using System.Threading;

namespace Filosof
{
    class Program
    {
        static object[] gaffler = new object[5];
        
        static Random rng = new Random();
        static void Main(string[] args)
        {
            gaffler[0] = new object();
            gaffler[1] = new object();
            gaffler[2] = new object();
            gaffler[3] = new object();
            gaffler[4] = new object();

            Thread filosof1 = new Thread(Eat);            
            Thread filosof2 = new Thread(Eat);            
            Thread filosof3 = new Thread(Eat);            
            Thread filosof4 = new Thread(Eat);            
            Thread filosof5 = new Thread(Eat);
            
            filosof1.Name = "filosof 1";
            filosof2.Name = "filosof 2";
            filosof3.Name = "filosof 3";
            filosof4.Name = "filosof 4";
            filosof5.Name = "filosof 5";

            filosof1.Start();
            filosof2.Start();
            filosof3.Start();
            filosof4.Start();
            filosof5.Start();

            filosof1.Join();
            filosof2.Join();
            filosof3.Join();
            filosof4.Join();
            filosof5.Join();
        }

        static void Eat()
        {
            int filosof = Convert.ToInt32(Thread.CurrentThread.Name.Substring(8));
            int venstregaffel = filosof - 2;
            int højregaffet = filosof - 1;

            if (venstregaffel < 0)
                venstregaffel = 4;

            while (true)
            {
                if (filosof == 5)
                {
                    try
                    {
                        Monitor.Enter(gaffler[højregaffet]);
                        try
                        {
                            Monitor.Enter(gaffler[venstregaffel]);
                            {
                                Console.WriteLine(Thread.CurrentThread.Name + " spiser nu");
                                Thread.Sleep(rng.Next(1000, 5000));
                                Console.WriteLine(Thread.CurrentThread.Name + " er færdig med at spise");
                                Monitor.Exit(gaffler[venstregaffel]);
                            }

                        }
                        finally
                        {
                            Monitor.Exit(gaffler[højregaffet]);
                        }
                    }
                    catch
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " venter på gaffel");
                    }
                    Thread.Sleep(rng.Next(1000, 5000));
                }
                else
                {
                    try
                    {
                        Monitor.Enter(gaffler[venstregaffel]);
                        try
                        {
                            Monitor.Enter(gaffler[højregaffet]);
                            {
                                Console.WriteLine(Thread.CurrentThread.Name + " spiser nu");
                                Thread.Sleep(rng.Next(1000, 5000));
                                Console.WriteLine(Thread.CurrentThread.Name + " er færdig med at spise");
                                Monitor.Exit(gaffler[højregaffet]);
                            }

                        }
                        finally
                        {
                            Monitor.Exit(gaffler[venstregaffel]);
                        }
                    }
                    catch
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " venter på gaffel");
                    }
                    Thread.Sleep(rng.Next(1000, 5000));
                }
            }
        }
    }
}
