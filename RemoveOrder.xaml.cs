using System;
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
    /// Logika interakcji dla klasy RemoveOrder.xaml
    /// </summary>
    public partial class RemoveOrder : Window
    {
        string login;
        public RemoveOrder(string log)
        {
            login = log;
            InitializeComponent();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)AllOrdersData.SelectedItems[0];

            string name = row["name"].ToString();
            string order_number = row["order_number"].ToString();
            string productNumber = row["productNumber"].ToString();

            SQLBaseClass.subOrder(name, int.Parse(order_number), login, productNumber);
            SQLBaseClass.refreshOrder(AllOrdersData, login);
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQLBaseClass.refreshOrder(AllOrdersData, login);
        }
    }
}
