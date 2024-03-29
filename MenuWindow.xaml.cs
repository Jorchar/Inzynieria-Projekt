﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

namespace Inzynieria_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        string login;
        public MenuWindow(string log)
        {
            login = log;
            InitializeComponent();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)menuData.SelectedItems[0];

            string name = row["name"].ToString();
            string productNumber = row["productNumber"].ToString();

            SQLBaseClass.addOrder(name, int.Parse(ordernrLabel.Content.ToString()), loginLabel.Content.ToString(), productNumber);
            SQLBaseClass.refreshOrder(orderData, loginLabel.Content.ToString(), ordernrLabel.Content.ToString());
        }

        private void Button_Sub(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)orderData.SelectedItems[0];

            string name = row["name"].ToString();
            string productNumber = row["productNumber"].ToString();

            SQLBaseClass.subOrder(name, int.Parse(ordernrLabel.Content.ToString()), loginLabel.Content.ToString(), productNumber);
            SQLBaseClass.refreshOrder(orderData, loginLabel.Content.ToString(), ordernrLabel.Content.ToString());
        }

        private void Button_Done(object sender, RoutedEventArgs e)
        {
            ordernrLabel.Content = SQLBaseClass.getOrderNumber(loginLabel.Content.ToString());
            SQLBaseClass.refreshOrder(orderData, loginLabel.Content.ToString(), ordernrLabel.Content.ToString());
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            SQLBaseClass.resetOrder(loginLabel.Content.ToString(), int.Parse(ordernrLabel.Content.ToString()));
            MessageBox.Show("You logged out");
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loginLabel.Content = login;
            menuData.DataContext = SQLBaseClass.fillDataGrids().DefaultView;
            ordernrLabel.Content = SQLBaseClass.getOrderNumber(login);
        }

        private void Button_old_orders(object sender, RoutedEventArgs e)
        {
            RemoveOrder removeOrder = new RemoveOrder(login);
            removeOrder.Show();
        }
    }
}
