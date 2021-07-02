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
    /// Logika interakcji dla klasy RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        string errorText = "";
        public RegisterWindow()
        {
            InitializeComponent();
        }

        Boolean onlyNumbers(string text)
        {
            foreach (char x in text)
            {
                if (!Char.IsNumber(x))
                {
                    return false;
                }
            }
            return true;
        }
        Boolean hasNumber(string text)
        {
            foreach(char x in text)
            {
                if (Char.IsNumber(x))
                {
                    return true;
                }
            }
            return false;
        }

        Boolean notNull()
        {
            if (nameBox.Text == "" || surnameBox.Text == "" || loginBox.Text == "" || passwordBox.Text == "" || phoneBox.Text == "" || cityBox.Text == "" || streetBox.Text == "" || housenrBox.Text == "" || postcodeBox0.Text == "" || postcodeBox1.Text == "" || emailBox.Text == "")
            {
                errorText = "You have to fill all spaces";
                return false;
            }
            return true;
        }

        Boolean verification()
        {
            if (!notNull())
            {
                return false;
            }
            if (!hasNumber(nameBox.Text) && !hasNumber(surnameBox.Text) && onlyNumbers(phoneBox.Text) && phoneBox.Text.Length==9 && onlyNumbers(housenrBox.Text) && onlyNumbers(postcodeBox0.Text) && onlyNumbers(postcodeBox1.Text))
            {
                return true;
            }
            else
            {
                errorText = "Error - Make sure that you entered correct data";
                return false;
            }
        }

        void addToDatabase()
        {
            SQLBaseClass.addToBase(nameBox.Text, surnameBox.Text, loginBox.Text, passwordBox.Text, phoneBox.Text, cityBox.Text, streetBox.Text, housenrBox.Text, postcodeBox0.Text + "-" + postcodeBox1.Text, emailBox.Text);
        }

        private void ButtonConfirm(object sender, RoutedEventArgs e)
        {
            if (verification())
            {
                addToDatabase();
                MessageBox.Show("Account created succesful");
                Application.Current.MainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(errorText);
            }
        }
    }
}
