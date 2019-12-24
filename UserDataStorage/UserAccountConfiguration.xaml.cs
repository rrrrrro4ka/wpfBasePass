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
using UserDataStorage.ViewModels;

namespace UserDataStorage
{
    /// <summary>
    /// Interaction logic for UserAccountConfiguration.xaml
    /// </summary>
    public partial class UserAccountConfiguration : Window
    {
        public UserAccountConfiguration()
        {
            InitializeComponent();
            DataContext = new UserInformationViewModel();
        }
    }
}
