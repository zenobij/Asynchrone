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
        CancellationTokenSource CTS = null;
        //BackgroundWorker bgw = null;
        Factorial myFact = null;
        public MainWindow()
        {
            InitializeComponent();
            /*bgw = new BackgroundWorker();
            bgw.DoWork += Bgw_DoWork;
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
            bgw.ProgressChanged += Bgw_ProgressChanged;
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;*/
        }

        /*private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PgbEvolution.Value = e.ProgressPercentage;
        }*/

        /*private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
            
        }*/

        /*private void Bgw_DoWork(object sender, DoWorkEventArgs e)
    {

        myFact = new Factorial();
        myFact.Progressing += MyFact_Progressing;
        BigInteger result = myFact.Calculate((int)e.Argument);
        if (myFact.CancelCalculate)
        {
            e.Cancel = true;
        } 

        e.Result = result;

    }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool resultParse = int.TryParse(TheControl.Number, out int number);
            if (resultParse)
            {

                CTS = new CancellationTokenSource();
                Button myButton = (Button)sender;
                myButton.IsEnabled = false;
                BtnCancel.IsEnabled = true;

                PgbEvolution.Minimum = 0;
                PgbEvolution.Maximum = 100;
                //bgw.RunWorkerAsync(number);
                myFact = new Factorial();
                myFact.Progressing += MyFact_Progressing;
                myFact.Completed += MyFact_Completed;
                myFact.Cancelled += MyFact_Cancelled;

                Task t = new Task<BigInteger>(() => myFact.CalculateAsync(number, CTS.Token));
                t.Start();
                



            }

        }

        private void MyFact_Completed(object sender, CompletedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                TxtResult.Text = e.Result.ToString();
                BtnFact.IsEnabled = true;
                BtnCancel.IsEnabled = false;
            });
            
            CTS.Dispose();
        }

        private void MyFact_Cancelled(object sender, CancelledEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                TxtMessage.Text = $"Cancelled after {e.ElapsedTime.Seconds} seconds";
                BtnFact.IsEnabled = true;
            });
            

        }

        private void MyFact_Progressing(object sender, ProgressingEventArgs e)
        {
            
                this.Dispatcher.Invoke(() =>
                {
                    int percentage = (e.CurrentNumber + 1) * 100 / e.TargetNumber;
                    PgbEvolution.Value = percentage;
                });
            


            //bgw.ReportProgress(percentage);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            BtnCancel.IsEnabled = false;
            if (CTS != null)
            {
                CTS.Cancel();
                CTS.Dispose();
                
            }
            
        }
    }
}
