
using System;
using System.Numerics;

namespace ClassLibraryMath
{
    public class CancelledEventArgs : EventArgs
    {
        public int TargetNumber { get; private set; }
        public bool Cancelled { get; private set; }

        public TimeSpan ElapsedTime { get; private set; }

        public CancelledEventArgs(int pTarget, TimeSpan pElapsedTime)
        {
            this.TargetNumber = pTarget;
            this.Cancelled = true ;
            this.ElapsedTime = pElapsedTime;
        }
    }
}