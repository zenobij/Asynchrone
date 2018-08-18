using ClassLibraryMath;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WebNewControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Factorial myFact = null;
        public MainWindow()
        {
            InitializeComponent();

            //PgbEvolution.Value = e.ProgressPercentage;

            //report
            //int percentage = (e.CurrentNumber + 1) * 100 / e.TargetNumber;
            //bgw.ReportProgress(percentage);

        }



        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                TxtMessage.Text = "Thread cancelled";
                TxtResult.Text = string.Empty;
            }
            else if (e.Error != null)
            {
                TxtMessage.Text = e.Error.ToString();
                TxtResult.Text = "";
            }
            else
            {
                TxtResult.Text = e.Result.ToString();
            }

        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {

            myFact = new Factorial();
            BigInteger result = myFact.Calculate((int)e.Argument);
            if (myFact.CancelCalculate)
            {
                e.Cancel = true;
            }

            e.Result = result;

        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    bool resultParse = int.TryParse(TheControl.Number, out int number);
        //    if (resultParse)
        //    {
        //        //init pgb
        //        PgbEvolution.Minimum = 0;
        //        PgbEvolution.Maximum = 100;
        //        //do work

        //    }

        //}



        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            bool resultParse = int.TryParse(TheControl.Number, out int number);
            if (resultParse)
            {

                //init pgb
                PgbEvolution.Minimum = 0;
                PgbEvolution.Maximum = 100;
                //call async work
                var progressIndicator = new Progress<int>(ReportProgress);
                BigInteger result = await DoWorkAsync(number, progressIndicator);
               
                TxtResult.Text = result.ToString();

            }

        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // click on cancel
        }


        public async Task<BigInteger> DoWorkAsync(int number, IProgress<int> progress)
        {
            int totalCount = number;

            int processCount = await Task.Run<int>(async () =>
            {
                int tempCount = 0;
                BigInteger result = 1;
                for (int decrement = number;
                            decrement > 1;
                                decrement--)
                {
                    //await the processing and uploading logic here
                    BigInteger processed = await DoTheSuperJob(result, decrement);

                    if (progress != null)
                    {
                        progress.Report((tempCount * 100 / totalCount));
                    }
                    tempCount++;
                }
                return tempCount;
            });
            return processCount;
        }


        public async Task<BigInteger> DoTheSuperJob(BigInteger result, int decrement)
        {
            Thread.Sleep(50);
            result = result * decrement;
            return result;
        }

        void ReportProgress(int value)
        {
            PgbEvolution.Value = value;
        }
    }
}
