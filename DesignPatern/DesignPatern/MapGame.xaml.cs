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
    /// Interaction logic for MapGame.xaml
    /// </summary>
    public partial class MapGame : Window
    {
        ConfigurationGame conf;

        public MapGame(ConfigurationGame _conf)
        {
            InitializeComponent();
            conf = _conf;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
