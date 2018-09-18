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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : UserControl
    {
        Client.DataClient dataClient = null;

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dataClient = new Client.DataClient();
            List<Entities.HallBooking> bookings = new List<Entities.HallBooking>();
            foreach (Entities.HallBooking booking in dataClient.GetData())
            {
                if (!booking.Confirmed)
                {
                    bookings.Add(booking);
                }
            }
            dgBookings.ItemsSource = bookings;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookings.SelectedItem is Entities.HallBooking selectedBooking)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        dataClient.Confirm(selectedBooking, (window as MainWindow).userData);
                        List<Entities.HallBooking> bookings = new List<Entities.HallBooking>();
                        foreach (Entities.HallBooking booking in dataClient.GetData())
                        {
                            if (!booking.Confirmed)
                            {
                                bookings.Add(booking);
                            }
                        }
                        dgBookings.ItemsSource = bookings;
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int result = dataClient.remove(dgBookings.SelectedItem as Entities.HallBooking);
            if(result >= 1)
            {
                MessageBox.Show("du har nu fjernet den fra listen");
            }
            else
            {
                MessageBox.Show("noget gik galt spørg til it support");
            }
            List<Entities.HallBooking> bookings = new List<Entities.HallBooking>();
            foreach (Entities.HallBooking booking in dataClient.GetData())
            {
                if (!booking.Confirmed)
                {
                    bookings.Add(booking);
                }
            }
            dgBookings.ItemsSource = bookings;
        }
    }
}
