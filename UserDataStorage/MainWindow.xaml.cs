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
using UserDataStorage.ViewModels;

namespace UserDataStorage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        private void Button_AddNewPassword(object sender, RoutedEventArgs e)
        {
            NewAccountWindow NPW = new NewAccountWindow();
            this.Close();
            NPW.Show();
        }

        private void Button_ChangePassword(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow CPW = new ChangePasswordWindow();
            this.Close();
            CPW.Show();
        }
    }

}
