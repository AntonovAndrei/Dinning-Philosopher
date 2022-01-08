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
        
        //основной метод
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
                    }
                    else
                    {
                        LeftFork.mutexObj.WaitOne();
                        LeftFork.IsFree = false;
                        if (!RightFork.IsFree)
                        {
                            LeftFork.mutexObj.ReleaseMutex();
                            LeftFork.IsFree = true;
                            ++hungreCount;
                            Thread.Sleep(500);
                        }
                        else
                        {
                            RightFork.mutexObj.WaitOne();
                            RightFork.IsFree = false;

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