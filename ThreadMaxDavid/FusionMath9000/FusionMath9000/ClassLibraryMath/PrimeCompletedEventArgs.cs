
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ClassLibraryMath
{
    public class PrimeCompletedEventArgs : EventArgs
    {
        public int TargetNumber { get; private set; }
        public List<int> PrimeNumbersList { get; private set; }

        public TimeSpan ElapsedTime { get; private set; }

        public PrimeCompletedEventArgs(int pTarget, List<int> pResult, TimeSpan pElapsedTime)
        {
            this.TargetNumber = pTarget;
            this.PrimeNumbersList = pResult ;
            this.ElapsedTime = pElapsedTime;
        }
    }
}