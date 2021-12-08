using System;
using System.Threading;

namespace diningPhilosophers
{
    public class Fork
    {
        public string Number { get; set; }
        public bool IsFree { get; set; }
        
        //в чем разница между мьютексом и лок
        public Mutex mutexObj = new Mutex();

        public Fork(string number)
        {
            Number = number;
            IsFree = true;
        }
        
        //возможно более правильный
        //public Semaphore semaphore = new Semaphore(0, 2);
    }
}