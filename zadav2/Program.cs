using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;


namespace zadav2
{
    class PriorityTest
    {
        static volatile bool loopSwitch;
        [ThreadStatic] static long threadCount = 0;

        public PriorityTest()
        {
            loopSwitch = true;
        }

        public bool LoopSwitch
        {
            set { loopSwitch = value; }
        }

        public void ThreadMethod()
        {
            while (loopSwitch)
            {
                threadCount++;
            }
            Console.WriteLine("{0,-11} with {1,11} priority " +
                "has a count = {2,13}", Thread.CurrentThread.Name,
                Thread.CurrentThread.Priority.ToString(),
                threadCount.ToString("N0"));
        }
        class Program
        {


            public string task_1(string x)
            {
                string error = "";
                if (Regex.IsMatch(x, ".*?[a-zA-Z].*?") || x == null)
                {
                    error = "-1";
                    Console.WriteLine("Warning-timer");
                }
                else
                {
                    error = "0";
                }
                return error;
            }
            public string task_2(string x)
            {
            
                string pokusaj = Regex.Replace(x, "[A-Za-z ]", "");

                return pokusaj;
            }
            public string task_3(string x)
            {
                string exeception = "";
                if (Regex.IsMatch(x, ".*?[a-zA-Z].*?") || x == null)
                {
                    Console.WriteLine("Terminating program");
                    exeception = "-1";
                }
                else
                {
                    exeception = "0";
                }

                    return exeception;
            }
            static void Main(string[] args)
            {
                string upis;
                string prvi = "";
                string drugi= "";
                string treci= "";
                int call_reset_timer = 0;
                ponovo: do
                {
                    upis = Console.ReadLine();
                    Program pro = new Program();
                    if(call_reset_timer == 0)
                    {
                        Console.WriteLine(pro.task_1(upis));
                        prvi = pro.task_1(upis);
                        if (!Regex.IsMatch(upis, ".*?[a-zA-Z].*?"))
                        {
                            call_reset_timer = 0;
                        }
                        else
                        {
                            call_reset_timer = 1;
                        }
                            
                    }
                   if(call_reset_timer == 1)
                   {
                        if (prvi == "-1")
                        {
                            PriorityTest priorityTest = new PriorityTest();
                            Thread thread1 = new Thread(priorityTest.ThreadMethod);
                            thread1.Name = "ThreadOne";
                            thread1.Priority = ThreadPriority.AboveNormal;
                            thread1.Start();
                            drugi = pro.task_2(upis);
                            Console.WriteLine(pro.task_2(upis));
                            if (!Regex.IsMatch(upis, ".*?[a-zA-Z].*?"))
                            {
                                call_reset_timer = 0;
                            }
                            else
                            {
                                call_reset_timer = 2;
                            }

                        }
                   }
                   if(call_reset_timer == 2)
                    {
                        if(drugi == "-1")
                        {
                            treci = pro.task_3(upis);
                            if (treci == "-1")
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                call_reset_timer = 0;
                                goto ponovo;
                            }
                        }
                    }
                   
                }
                while (upis != "EXIT");


            }
        }
    }
}
