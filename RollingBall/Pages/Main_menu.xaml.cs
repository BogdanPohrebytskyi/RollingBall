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
using System.Windows.Navigation;
using System.Windows.Shapes;

using RollingBall.Models;
using RollingBall.Models.Levels;
using RollingBall.Pages;

namespace RollingBall
{
    /// <summary>
    /// Navigation to specific pages
    /// </summary>
    public partial class Main_menu : Page
    {
        public Main_menu()
        {
            InitializeComponent();
        }

        private void Quit_button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Arcade_button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Arcade_page());
        }

        private void Settings_button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Settings_page());
        }

        private void Challenge_button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Challenge_page());
        }
    }
}
