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
using WpfApp1.Logic;
using WpfApp1.Models;
using WpfApp1.ViewModels;

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
        /// <summary>
        /// Отображение систем пользователя в его личном кабинете. Что-то тут не так, понять как правильно реализовать???
        /// </summary>
        /// <param name="_mainwindow"></param>
        public CabinetPage(MainWindow _mainwindow)
        {
            InitializeComponent();
            mainwindow = _mainwindow;
            try
            {
                UserServiceCabinet us = new UserServiceCabinet(db);
                List<UserInfo> userInfo = us.GetUserInfo(Login.uID);
                if (userInfo != null)
                {
                    UserViewInformation userInfoview = new UserViewInformation(userInfo);
                    List<UserViewInformation> listView = userInfoview.Listout;
                    //this.DataContext = listView;
                    DataGridCabinet.ItemsSource = listView;
                    //ItemsSource = "{Binding Source=userInfo}"
                }
            } catch(Exception exc)
            {
                MessageBox.Show($"Не удалось получить информацию по пользователю. Произошла какая-то ошибка: {0}", exc.Message);
            }
           
        }

        
        

        private void AddNewSystemToBase(object sender, RoutedEventArgs e)
        {
            try
            {
                uID = Login.uID;
                mainwindow.OpenPages(MainWindow.pages.addobject);
            } catch(Exception exc)
            {
                MessageBox.Show($"Ошибка при попытке перейти в раздел добавления новой системы пользователя: {0}", exc.Message);
            }
            
        }
    }
}