using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// </summary>
    public partial class Page1 : Page
    {

        public Page1()
        {
            InitializeComponent();
            Main.Content = new Home();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                Main.Content = new Loading();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("login.xaml", UriKind.Relative));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Main.Content = new Home();
        }

     
    }
}
