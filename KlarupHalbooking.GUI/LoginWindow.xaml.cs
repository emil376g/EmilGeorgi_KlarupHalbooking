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

namespace KlarupHalbooking.GUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : UserControl
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Client.DataClient client = new Client.DataClient();
            (bool isUser, Entities.UserData user) = client.Login(new Entities.UserData(tbxUsernameInput.Text, tbxPasswordInput.Password, "00000000"));
            if (isUser)
            {
                MainWindow mainWindow = new MainWindow();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).userData = user;
                        (window as MainWindow).isLoggedIn = true;
                    }
                }
                mainWindow.Show();
                Application.Current.MainWindow.Close();
            }
        }
    }
}
