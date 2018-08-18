using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfUsingTaskScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSync_Click(object sender, RoutedEventArgs e)
        {
            this.TxtContent.Text = String.Empty;
            string msg;

            try
            {
                msg = TaskMethod().Result;
            }
            catch (Exception ex)
            {
                msg = ex.InnerException.Message;
            }

            this.TxtContent.Text = msg;
        }

        private void BtnAsync_Click(object sender, RoutedEventArgs e)
        {
            this.TxtContent.Text = String.Empty;
            Mouse.OverrideCursor = Cursors.Wait;
            Task<string> tsk = TaskMethod();
            tsk.ContinueWith(t =>
            {
                this.TxtContent.Text = t.Exception.InnerException.Message;
                Mouse.OverrideCursor = null;
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void BtnAsyncOK_Click(object sender, RoutedEventArgs e)
        {

        }


        private Task<string> TaskMethod()
        {
            return TaskMethod(TaskScheduler.Default);
        }
        private Task<string> TaskMethod(TaskScheduler scheduler)
        {
            Task delay = Task.Delay(3000);
            return delay.ContinueWith(t =>
            {
                string str = $"Task is running on Thread id {Thread.CurrentThread.ManagedThreadId}" +
                $". Is thread Pool : {Thread.CurrentThread.IsThreadPoolThread}";
                TxtContent.Text = str;
                return str;
            }, scheduler);
        }
    }
}
