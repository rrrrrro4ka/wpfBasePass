﻿using System;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            OpenPages(pages.login);

        }
        public enum pages
        {
            login,
            registr,
            cabinet,
            addobject
        }
        /// <summary>
        /// Метод перехода между разными страницами. 
        /// </summary>
        /// <param name="pages"></param>
        public void OpenPages(pages pages)
        {
            if(pages == pages.login)
            {
                frame.Navigate(new Login(this));
            } else
                if(pages == pages.registr)
            {
                frame.Navigate(new Registr(this));
            }
            else
                if (pages == pages.cabinet)
            {
                frame.Navigate(new CabinetPage(this));
            }
            else
                if (pages == pages.addobject)
            {
                frame.Navigate(new AddObject(this));
            }
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    WindowWork win = new WindowWork();
        //    win.Show();
        //    this.Close();
        //}
    }

}
