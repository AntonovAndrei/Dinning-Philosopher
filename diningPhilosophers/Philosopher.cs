using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace diningPhilosophers
{
    public class Philosopher
    {
        public string Name { get; set; }

        public Fork LeftFork { get; set; }
        public Fork RightFork { get; set; }

        public Philosopher(string name)
        {
            this.Name = name;
        }

        private int hungreCount = 0;
        public void Eat()
        {
            while (true)
            {
                if (hungreCount < 3)
                {
                    if (!LeftFork.IsFree)
                    {
                        ++hungreCount;
                        Thread.Sleep(500);
                        //ConsoleHelper.WriteToConsole(LeftFork.Number, "в чей то руке");
                    }
                    else
                    {
                        LeftFork.mutexObj.WaitOne();
                        LeftFork.IsFree = false;
                        //ConsoleHelper.WriteToConsole(Name, $"взял вилку {LeftFork.Number}");
                        if (!RightFork.IsFree)
                        {
                            //ConsoleHelper.WriteToConsole(RightFork.Number, "в чей то руке");
                            LeftFork.mutexObj.ReleaseMutex();
                            LeftFork.IsFree = true;
                            ++hungreCount;
                            Thread.Sleep(500);
                            //ConsoleHelper.WriteToConsole(Name, $"положил вилку {LeftFork.Number} обратно на стол");
                        }
                        else
                        {
                            RightFork.mutexObj.WaitOne();
                            RightFork.IsFree = false;
                            //ConsoleHelper.WriteToConsole(Name, $"взял вилку {RightFork.Number}");

                            ConsoleHelper.WriteToConsole(Name, "Начал есть");
                            Thread.Sleep(1000);
                            ConsoleHelper.WriteToConsole(Name,
                                "покушал, теперь на протяжении 3 секунд он думает о чем угодно кроме еды");

                            LeftFork.mutexObj.ReleaseMutex();
                            LeftFork.IsFree = true;
                            RightFork.mutexObj.ReleaseMutex();
                            RightFork.IsFree = true;
                        }
                    }
                }
                else
                {
                    ConsoleHelper.WriteToConsole(Name,
                        "взбесился и как только осводиться вилка, он сразу ее возьмет и не отпустит пока не поест");
                    LeftFork.mutexObj.WaitOne();
                    RightFork.mutexObj.WaitOne();
                    ConsoleHelper.WriteToConsole(Name, "Начал есть");
                    Thread.Sleep(1000);
                    ConsoleHelper.WriteToConsole(Name,
                        "покушал, теперь на протяжении 5 секунд он думает о чем угодно кроме еды");
                    hungreCount = 0;
                    LeftFork.mutexObj.ReleaseMutex();
                    RightFork.mutexObj.ReleaseMutex();
                }
                
                Thread.Sleep(5000);
            }
        }
    }
}