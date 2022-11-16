using AutoTrainer.DL;
using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AutoTrainer.Views
{
    /// <summary>
    /// Interaction logic for BatchView.xaml
    /// </summary>
    public partial class BatchView : UserControl
    {
        public BatchView()
        {
            BatchRepository batchRepo = new BatchRepository();
            Batch test = new Batch(batchRepo);
            InitializeComponent();

            BatchDataGrid.ItemsSource = test.Associates;
        }

        private void WarnButton_Click(object sender, RoutedEventArgs e)
        {
            Associate associate = (Associate)((Button)e.Source).DataContext;

            MessageBox.Show($"{associate.Name}");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Associate associate = (Associate)((Button)e.Source).DataContext;

            MessageBox.Show($"{associate.Name}");
        }
    }
}
