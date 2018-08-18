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

namespace WebNewControls
{
    /// <summary>
    /// Interaction logic for SimpleUserControl.xaml
    /// </summary>
    public partial class SimpleUserControl : UserControl
    {
        public SimpleUserControl()
        {
            InitializeComponent();
        }

        private void BtnFillList_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                LstResult.Items.Add(i.ToString());
            }
        }
    }
}
