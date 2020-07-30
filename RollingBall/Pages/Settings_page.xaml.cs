using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RollingBall.Pages
{
    /// <summary>
    /// Логика взаимодействия для Settings_page.xaml
    /// </summary>
    public partial class Settings_page : Page, INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<StackPanel> balls_panel_item_source { get; set; } // ComboBox items source. 

        public Settings_page()
        {
            InitializeComponent();
            DataContext = this;

            this.Loaded += (s, e) =>
            {
                balls_panel_item_source = get_ball_list(); // get ComboBox items source. 
                OnPropertyChanged("balls_panel_item_source");
                load_settings(); // get TextBox & ComboBox selected items
            };
            //
        }
        /// <summary>
        /// Get StackPanel list of available balls images and its names
        /// </summary>
        ObservableCollection<StackPanel> get_ball_list()
        {
            ObservableCollection<StackPanel> list = new ObservableCollection<StackPanel>();

            string[] balls_names = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources", "*.png").
                Where(s => s.ToLower().Contains("ball")).
                Select(s => System.IO.Path.GetFileName(s)).
                ToArray(); // get balls file names

            foreach(string b in balls_names) // create StackPanel
            {
                StackPanel sp = new StackPanel();
                sp.Width = balls_list.ActualWidth;
                sp.Height = balls_list.ActualHeight;
                sp.Orientation = Orientation.Horizontal;

                Image i = new Image();// ball icon
                i.Source = new BitmapImage(new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\" + b));
                sp.Children.Add(i);

                Label l = new Label();//ball name
                l.Content = b;
                l.FontFamily = new FontFamily("Comic Sans MS");
                l.FontStyle = FontStyles.Italic;
                l.FontSize = balls_list.ActualHeight/ 2;
                sp.Children.Add(l);

                list.Add(sp);
            }

            return list;
        }

        /// <summary>
        /// Textbox text imput check. Only digits
        /// </summary>
        private void Textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            e.Handled =  ((TextBox)sender).Text.Length > 3;
        }

        /// <summary>
        /// Save data to Properties, change window size
        /// </summary>
        void save_settings()
        {
            try
            {
                Properties.Settings.Default.window_size = new Point(Convert.ToInt32(x.Text), Convert.ToInt32(y.Text));
                Properties.Settings.Default.ball_name = ((Label)((StackPanel)balls_list.SelectedItem).Children[1]).Content.ToString();
                Properties.Settings.Default.Save();

                Window.GetWindow(this).Width = Convert.ToInt32(x.Text);
                Window.GetWindow(this).Height = Convert.ToInt32(y.Text);
            }
            catch { throw new Exception("Incorect settings"); }
        }
        void load_settings()
        {
            x.Text = Properties.Settings.Default.window_size.X.ToString();
            y.Text = Properties.Settings.Default.window_size.Y.ToString();
            balls_list.SelectedItem = balls_panel_item_source.Where(x => ((Label)x.Children[1]).Content.ToString() == Properties.Settings.Default.ball_name).First();
        }
        /// <summary>
        /// Save settings and exit
        /// </summary>
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            save_settings();
            this.NavigationService.Navigate(new Main_menu());
        }
    }
}
