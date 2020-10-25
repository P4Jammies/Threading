using System;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            int darts, threads;
            Console.WriteLine("How many threads would you like to open?");
            threads = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many darts would you like to throw in each thread?");
            darts = Convert.ToInt32(Console.ReadLine());


        }
    }

    class FindPiThread
    {
        int darts;
        int hits { get { return hits; } set { hits = value; } }
        Random throws;

        FindPiThread(int ammo)
        {
            darts = ammo;
            hits = 0;
            throws = new Random();
        }

        public void throwDarts()
        {
            while (darts > 0)
            {
                darts--;
                double x = throws.NextDouble();
                double y = throws.NextDouble();
                if(Math.Sqrt(x*x + y*y) <= 1.0)
                {
                    hits++;
                }
            }
        }
    }
}
