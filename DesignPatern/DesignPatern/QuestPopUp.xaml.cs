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
using System.Windows.Shapes;

namespace MaraudersAdventure
{
    /// <summary>
    /// Interaction logic for QuestPopUp.xaml
    /// </summary>
    public partial class QuestPopUp : Window
    {
        TypeQuete type;
        public QuestPopUp(TypeQuete tt)
        {
            type = tt;
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (type == TypeQuete.TrouverCase)
            {
                //lblExtra.SetValue = "";
                txtExtra.Text = "";
            }
        }
    }
}
