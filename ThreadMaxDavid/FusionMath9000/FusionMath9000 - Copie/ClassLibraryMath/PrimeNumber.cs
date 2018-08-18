using System;
using System.Collections.Generic;
using System.Threading;

namespace ClassLibraryMath
{
    public class PrimeNumber
    {
        public event EventHandler Initializing;
        public event EventHandler<ProgressingEventArgs> Progressing;
        public event EventHandler<PrimeCompletedEventArgs> Completed;
        public List<int> GetAllPrimesBelowNumber(int pNumber)
        {
            if (pNumber < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            Initializing?.Invoke(this, new EventArgs());
            List<int> allPrimes = new List<int>();
            for (int current = 1; current < pNumber; current++)
            {
                bool isPrime = IsPrimeNumber(current);
                if (isPrime)
                {
                    Thread.Sleep(10);
                    allPrimes.Add(current);
                    Progressing?.Invoke(this, new ProgressingEventArgs(pNumber, current));
                }
            }
            Completed?.Invoke(this, new PrimeCompletedEventArgs(pNumber, allPrimes, new TimeSpan(0)));
            return allPrimes;
        }
        public bool IsPrimeNumber(int pNumber)
        {
            if (pNumber < 2)
            {
                return false;
            }
            if (pNumber == 2)
            {
                return true;
            }
            if (pNumber % 2 == 0)
            {
                return false;
            }
            int counter = 1;
            for (int divisor = 3; divisor <= pNumber; divisor+=2)
            {
                if (pNumber % divisor == 0)
                {
                    counter += 1;
                }
                if (counter > 2)
                {
                    break;
                }
            }
            return (counter == 2);
        }
    }
}