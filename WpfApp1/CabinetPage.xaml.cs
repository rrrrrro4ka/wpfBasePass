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
using WpfApp1.Logic;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for CabinetPage.xaml
    /// </summary>
    public partial class CabinetPage : Page
    {
        public MainWindow mainwindow;
        DBcontext db = new DBcontext();
        public static int uID { get; set; }

        public CabinetPage(MainWindow _mainwindow)
        {
            InitializeComponent();
            mainwindow = _mainwindow;
            UserServiceCabinet us = new UserServiceCabinet(db);
            UserInfo userInfo = us.GetUserInfo(Login.uID);
        }

        
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            uID = Login.uID;
            mainwindow.OpenPages(MainWindow.pages.addobject);
        }
    }
}
