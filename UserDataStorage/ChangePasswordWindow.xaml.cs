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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
            DataContext = new ChangeUserPasswordView();
        }

        private void Button_BackMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow BackMainWindow = new MainWindow();
            this.Close();
            BackMainWindow.Show();
        }
    }
}
