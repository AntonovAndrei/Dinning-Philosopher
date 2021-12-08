using System;
using System.Collections.Generic;
using System.Threading;

namespace diningPhilosophers
{
    class Program
    {
        //количество философов
        private static int PHILOSOPHERS = 5;
        //количество вилок
        private static int FORKS = PHILOSOPHERS;

        static void Main(string[] args)
        {
            Fork[] forks = new Fork[FORKS];
            for (int i = 0; i < FORKS; i++)
            {
                forks[i] = new Fork($"fork{i + 1}");
                Console.WriteLine($"Вилка {forks[i].Number} создана");
            }
            
            Philosopher[] philosophers = new Philosopher[PHILOSOPHERS];
            for (int i = 0; i < PHILOSOPHERS; i++)
            {
                philosophers[i] = new Philosopher($"Philosopher{i + 1}");
                philosophers[i].LeftFork = forks[i];
                if (i != PHILOSOPHERS - 1)
                {
                    philosophers[i].RightFork = forks[i + 1];
                }
                else
                {
                    philosophers[i].RightFork = forks[0];
                }
                
                Console.WriteLine($"Философ {philosophers[i].Name} создан с левой вилкой {philosophers[i].LeftFork.Number}, с правой - {philosophers[i].RightFork.Number}.");
            }

            Thread[] philosopherThreads = new Thread[PHILOSOPHERS];
            
            for (int i = 0; i < PHILOSOPHERS; i++)
            {
                philosopherThreads[i] = new Thread(philosophers[i].Eat);
                philosopherThreads[i].Name = $"philosopher{i + 1} thread";
                philosopherThreads[i].Start();
            }
        }
    }
}