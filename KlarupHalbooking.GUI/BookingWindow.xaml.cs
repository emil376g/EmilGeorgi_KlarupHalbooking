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
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : UserControl
    {
        Client.DataClient dataClient = null;
        public BookingWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    if(dataClient.AddData(dtpBookingDate.Value ?? DateTime.Now, dtpBookingEndDate.Value ?? DateTime.Now,(string)cmbActivity.SelectedItem, (window as MainWindow).userData))
                    {
                        MessageBox.Show("du har nu tilføjet en booking, den bliver nu kigget på af administratorerne");
                    }
                }
            }
        }

        private void cmbActivity_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
            dataClient = new Client.DataClient();
            List<string> activityNames = new List<string>();
            List<Entities.IBooking> activities = dataClient.GetData();
            foreach (Entities.Activity activity in activities)
            {
                activityNames.Add(activity.ActivityName);
            }
            cmbActivity.ItemsSource = activityNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "contact your supporter");
                Application.Current.Shutdown();
            }
        }
    }
}
