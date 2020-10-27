using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            int darts, threads, hits;
            Console.WriteLine("How many threads would you like to open?");
            threads = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many darts would you like to throw in each thread?");
            darts = Convert.ToInt32(Console.ReadLine());

            List<Thread> threadList = new List<Thread>(threads);
            List<FindPiThread> piThreads = new List<FindPiThread>(threads);
            
            var calctime = new Stopwatch();
            calctime.Start();

            for (int i = 0; i < threads; i++)
            {
                FindPiThread piThread = new FindPiThread(darts);
                piThreads.Add(piThread);
            
                Thread thread = new Thread(new ThreadStart(piThread.throwDarts));
                threadList.Add(thread);
                thread.Start();

                Thread.Sleep(16);
            }
            
            foreach (Thread thread in threadList)
            {
                thread.Join();
            }

            hits = 0;
            foreach (FindPiThread piThread in piThreads)
            {
                hits += piThread.target();
            }

            double pi = 4 * (hits / (double)(darts * threads));
            calctime.Stop();

            Console.WriteLine($"Took {calctime.ElapsedMilliseconds} ms to calculate pi: {pi}\n");
            Thread.Sleep(2000);
        }
    }

    class FindPiThread
    {
        int darts;
        int hits;
        Random throws;

        public FindPiThread(int d)
        {
            darts = d;
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

        public int target()
        {
            return hits;
        }
    }
}
