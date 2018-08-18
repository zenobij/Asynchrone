using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using BLL.Repository;
using System.Diagnostics;
using System.Threading;

namespace WPFGeonameAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string directoryPath = null;
        public string DirectoryPath
        {
            get
            {
                return directoryPath;
            }
            set
            {
                directoryPath = value;
            }

        }
        public MainWindow()
        {
            InitializeComponent();
        }



        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                DirectoryPath = fbd.SelectedPath;
            }
        }

        private void BtnUnzip_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (string filePath in Directory.EnumerateFiles(DirectoryPath, "??.zip"))
            {
                
                string copy = filePath;
                WriteZipInTheTextBlock(copy);
            }

        }

        private void WriteZipInTheTextBlock(string filePath)
        {
            using (RepoFile repo = new RepoFile())
            {
                IEnumerable<string> myLines = repo.ExtractZipFiles(filePath);
                WriteInTextCountryInfo(myLines);
            }
            Dispatcher.Invoke(() => { });


        }

        private void WriteInTextCountryInfo(IEnumerable<string> myLines)
        {
            //StringBuilder sb = new StringBuilder();
            foreach (string test in myLines)
            {
                txtCountryInfos.Items.Add(test);
            }
            //sb.AppendLine();
            //txtCountryInfos.Text += sb.ToString();
        }
    }
}
