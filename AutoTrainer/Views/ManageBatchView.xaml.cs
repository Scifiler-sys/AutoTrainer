using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Services;
using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ManageBatchView.xaml
    /// Views are mostly for concerning on how you should show the view
    /// </summary>
    public partial class ManageBatchView : UserControl
    {
        public static readonly DependencyProperty WarnCommandProperty = DependencyProperty.Register("WarnCommand", typeof(ICommand), typeof(ManageBatchView), new PropertyMetadata(null));
        public ICommand WarnCommand
        {
            get { return (ICommand)GetValue(WarnCommandProperty); }
            set { SetValue(WarnCommandProperty, value); }
        }

        public ManageBatchView()
        {
            InitializeComponent();
        }

        //Decided to not use ViewModel only because routing routedEventArgs is not that easy in a command
        private void WarnButton_Click(object sender, RoutedEventArgs e)
        {
            AssociateViewModel associate = (AssociateViewModel)((Button)e.Source).DataContext;

            if (MessageBox.Show($"Are you sure you want to send {associate.FullName} a technical warning?",
                    "Send Email",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (WarnCommand != null)
                {
                    WarnCommand.Execute(associate);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            AssociateViewModel associate = (AssociateViewModel)((Button)e.Source).DataContext;

            MessageBox.Show($"{associate.firstName}");
        }
    }
}
