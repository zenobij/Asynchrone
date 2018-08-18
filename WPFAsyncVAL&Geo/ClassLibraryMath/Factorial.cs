using System;
using System.Numerics;
using System.Diagnostics;
using System.Threading;

namespace ClassLibraryMath
{
    // le générateur de documentation sandcastle
    /// <summary>
    /// cette classe gère tout ce qui concerne les factorielles
    /// </summary>
    public class Factorial
    {
        public event EventHandler Initializing;
        public event EventHandler<ProgressingEventArgs> Progressing;
        public event EventHandler<CompletedEventArgs> Completed;
        public event EventHandler<CancelledEventArgs> Cancelled;
        public bool CancelCalculate { get; set; }
        /// <summary>
        /// cette méthode calcule la factorielle d'un nombre
        /// </summary>
        /// <param name="pNumber">paramètre nombre entier positif obligatoire</param>
        /// <returns>entier avec le résultat de la factorielle</returns>
        public BigInteger Calculate(int pNumber)
        {
            if (pNumber < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Initializing?.Invoke(this, new EventArgs());
            Stopwatch myWatch = new Stopwatch();
            myWatch.Start();
            // exemple while
            BigInteger result = 1;
            {
                int decrement = pNumber;
                while (decrement > 1)
                {
                    result = result * decrement;
                    //pNumber = pNumber - 1; // solution étendue
                    decrement -= 1; // solution abrégée
                                    //pNumber--;  // solution courte postfixée
                                    //--pNumber;  // solution courte prefixée
                }
            }
            //exemple for 
            result = 1;
            for (int decrement = pNumber;
                        decrement > 1;
                            decrement--)
            {
                if (this.CancelCalculate)
                {
                    myWatch.Stop();
                    if (Cancelled != null)
                    {
                        CancelledEventArgs CEA = new CancelledEventArgs(pNumber,  myWatch.Elapsed);
                        Cancelled(this, CEA);
                    }
                    return 0;
                }
                if (Progressing != null)
                {
                    ProgressingEventArgs PEA = new ProgressingEventArgs(pNumber, pNumber - decrement + 1);
                    OnProgressing(PEA);
                }
                Thread.Sleep(20);
                result = result * decrement;
            }
            myWatch.Stop();
            if (Completed != null)
            {
                CompletedEventArgs CEA = new CompletedEventArgs(pNumber, result, myWatch.Elapsed);
                OnCompleted(CEA);
            }
            return result;
        }

        public BigInteger CalculateAsync(int pNumber,CancellationToken CT)
        {
            if (pNumber < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Initializing?.Invoke(this, new EventArgs());
            Stopwatch myWatch = new Stopwatch();
            myWatch.Start();
            // exemple while
            BigInteger result = 1;
            /*{
                int decrement = pNumber;
                while (decrement > 1)
                {
                    result = result * decrement;
                    //pNumber = pNumber - 1; // solution étendue
                    decrement -= 1; // solution abrégée
                                    //pNumber--;  // solution courte postfixée
                                    //--pNumber;  // solution courte prefixée
                }
            }*/
            //exemple for 
            result = 1;
            for (int decrement = pNumber;
                        decrement > 1;
                            decrement--)
            {
                if (CT.IsCancellationRequested)
                {
                    myWatch.Stop();


                    if (Cancelled != null)
                    {
                        CancelledEventArgs CEA = new CancelledEventArgs(pNumber, myWatch.Elapsed);
                        Cancelled(this, CEA);
                    }
                    return -1;
                }

                if (Progressing != null)
                {
                    ProgressingEventArgs PEA = new ProgressingEventArgs(pNumber, pNumber - decrement + 1);
                    OnProgressing(PEA);
                }
                Thread.Sleep(50);
                result = result * decrement;
            }
            myWatch.Stop();
            if (Completed != null)
            {
                CompletedEventArgs CEA = new CompletedEventArgs(pNumber, result, myWatch.Elapsed);
                OnCompleted(CEA);
            }
            return result;
        }

        protected virtual void OnCompleted(CompletedEventArgs CEA)
        {
            Completed(this, CEA);
        }

        protected virtual void OnProgressing(ProgressingEventArgs PEA)
        {
            Progressing(this, PEA);
        }

        public BigInteger CalculateRecursif(int pNumber)
        {
            if (pNumber < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (pNumber > 1) ? pNumber * CalculateRecursif(pNumber - 1) : 1;

            if (pNumber > 1)
            {
                // branche récursive
                return pNumber * CalculateRecursif(pNumber - 1);
            }
            else
            {
                // branche non récursive qui arrête la récursivité
                return 1;
            }


        }
    }
}