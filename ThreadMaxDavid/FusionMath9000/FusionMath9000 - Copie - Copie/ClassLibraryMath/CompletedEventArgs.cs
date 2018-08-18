
using System;
using System.Numerics;

namespace ClassLibraryMath
{
    public class CompletedEventArgs : EventArgs
    {
        public int TargetNumber { get; private set; }
        public BigInteger Result { get; private set; }

        public TimeSpan ElapsedTime { get; private set; }

        public CompletedEventArgs(int pTarget, BigInteger pResult, TimeSpan pElapsedTime)
        {
            this.TargetNumber = pTarget;
            this.Result=pResult ;
            this.ElapsedTime = pElapsedTime;
        }
    }
}