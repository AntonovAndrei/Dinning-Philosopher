using System;
using System.Threading;

namespace diningPhilosophers
{
    public class Fork
    {
        public string Number { get; set; }
        public bool IsFree { get; set; }
        
        public Mutex mutexObj = new Mutex();

        public Fork(string number)
        {
            Number = number;
            IsFree = true;
        }
    }
}