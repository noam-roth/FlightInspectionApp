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

namespace FG4._0
{
    /// <summary>
    /// Interaction logic for Navigators.xaml
    /// </summary>
    public partial class Navigators : UserControl
    {
        NavigatorsViewModel mv;
        public Navigators()
        {
            mv = (Application.Current as App).NVM;
            DataContext = mv;
            InitializeComponent();
        }
    }
}
