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
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        public NewAccountWindow()
        {
            InitializeComponent();
            DataContext = new NewAccountUserViewModel();
        }

        private void Button_BackToMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow BackToMainWindow = new MainWindow();
            this.Close();
            BackToMainWindow.Show();
        }
    }
}
