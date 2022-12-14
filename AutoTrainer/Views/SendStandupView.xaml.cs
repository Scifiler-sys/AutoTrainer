using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SendStandup.xaml
    /// </summary>
    public partial class SendStandupView : UserControl
    {
        public SendStandupView()
        {
            InitializeComponent();
        }

        //Textbox only accepts numeric values
        private void TextBox_PreviewTextInput_Numbers_Only(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_Workload(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (new Regex("[^1-5]+").IsMatch(e.Text));
        }
    }
}
