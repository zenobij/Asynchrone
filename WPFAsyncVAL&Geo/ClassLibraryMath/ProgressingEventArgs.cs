using System;

namespace ClassLibraryMath
{
    public class ProgressingEventArgs : EventArgs
    {
        public int TargetNumber { get; private set; }
        public int CurrentNumber { get; private set; }
        public bool Cancel { get; set; }
        public ProgressingEventArgs(int pTarget , int pCurrent)
        {
            this.TargetNumber = pTarget;
            this.CurrentNumber = pCurrent;
        }

    }
}