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

namespace Inzynieria_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow(string log)
        {
            loginLabel.Content = log;
            SQLBaseClass.fillDataGrids(menuData);
            ordernrLabel.Content = SQLBaseClass.getOrderNumber(log);
            InitializeComponent();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            SQLBaseClass.refreshOrder(orderData, loginLabel.Content.ToString(), int.Parse(ordernrLabel.Content.ToString()));
            MessageBox.Show(menuData.SelectedItem.ToString());
        }

        private void Button_Sub(object sender, RoutedEventArgs e)
        {
            SQLBaseClass.refreshOrder(orderData, loginLabel.Content.ToString(), int.Parse(ordernrLabel.Content.ToString()));
            MessageBox.Show(orderData.SelectedItem.ToString());
        }

        private void Button_Done(object sender, RoutedEventArgs e)
        {
            ordernrLabel.Content = SQLBaseClass.getOrderNumber(loginLabel.Content.ToString());
            SQLBaseClass.refreshOrder(orderData, loginLabel.Content.ToString(), int.Parse(ordernrLabel.Content.ToString()));
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            SQLBaseClass.resetOrder(loginLabel.Content.ToString(), int.Parse(ordernrLabel.Content.ToString()));
            MessageBox.Show("You logged out");
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}
