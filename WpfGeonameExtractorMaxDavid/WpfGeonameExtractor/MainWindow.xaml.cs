using Ionic.Zip;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfGeonameExtractor.Helper;

namespace WpfGeonameExtractor
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

        private async void BtnDoTheWork_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> LaListeDesFichiersZip = CreateFileList(fbd.SelectedPath).ToList();

                System.Windows.Forms.FolderBrowserDialog fbdOutput = new System.Windows.Forms.FolderBrowserDialog();

                if (fbdOutput.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string TargetDirectory = fbdOutput.SelectedPath;
                    await ExtractAllZipAndAddInDatabase(LaListeDesFichiersZip, TargetDirectory);
                }
            }
        }

        private static async Task ExtractAllZipAndAddInDatabase(List<string> LaListeDesFichiersZip, string TargetDirectory)
        {
            foreach (var archive in LaListeDesFichiersZip)
            {
                await Task.Factory.StartNew(() =>
                {
                    using (ZipFile zip = ZipFile.Read(archive))
                    {
                        foreach (ZipEntry e in zip)
                        {
                            if (!e.FileName.EndsWith("readme.txt"))
                            {
                                e.Extract(TargetDirectory);
                                SuperHelperDeDavid.AjoutDB(TargetDirectory + "\\" + e.FileName, "TFNSDOTDE0402B", "db_geoname", "Country");
                            }
                        }
                    }
                });
            }
        }

        private static IEnumerable<string> CreateFileList(string selectedPath)
        {
            string[] ZipFiles = Directory.GetFiles(selectedPath);
            foreach (var item in ZipFiles)
            {
                if (item.EndsWith(".zip") && item[item.Length - 7] == '\\') //item.LastIndexOf('/') ==
                {
                    yield return item;
                }
            }
        }
    }
}