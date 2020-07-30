using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using RollingBall.Models.Levels;
using RollingBall.Pages;
namespace RollingBall
{
    /// <summary>
    /// Draw page that contains links to Game page with specific level
    /// </summary>
    public partial class Arcade_page : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int[] button_numbers { get; set; } // Buttons names array
        public string[] levels_names { get; set; } 
        public int table_size { get; set; } // used to create ItemsControl's table to locate buttons on canvas (xaml)


        /// <summary>
        /// Initialize page
        /// </summary>
        public Arcade_page()
        {
            DataContext = this;
            InitializeComponent();

            levels_names = get_class_names(); // get levels count
            if (levels_names.Length == 0)
                throw new Exception("No levels found");

            table_size = calculate_table_size(levels_names.Count()); // create ItemsControl's table

            button_numbers = Enumerable.Range(1, levels_names.Length).ToArray(); // create buttons
            OnPropertyChanged("button_numbers");
        }

        /// <summary>
        /// Get levels names from Levels folder
        /// </summary>
        /// <returns>level names string array</returns>
        string[] get_class_names()
        {
            string[] levels_names = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Models\Levels", "*.cs").
                Where(x => x.Any(char.IsDigit) && x.Contains("Level")).ToArray();//level cs file names array
            for (int i = 0; i < levels_names.Count(); i++) // delete ".cs" from string
            {
                levels_names[i] = System.IO.Path.GetFileName(levels_names[i]);
                levels_names[i] = levels_names[i].Remove(levels_names[i].Length - 3);
            }
            levels_names = levels_names.OrderBy(x => x).ToArray();//sort 
            return levels_names;
        }

        /// <summary>
        /// Calculate locations of buttons on canvas in xaml
        /// </summary>
        /// <param name="array_lenght">levels_names.Count()</param>
        /// <returns></returns>
        int calculate_table_size(int array_lenght)
        {
            int a=1;
            while(a*a<array_lenght)
            {
                a++;
            }
            return a;
        }
        /// <summary>
        /// Navigate to specific level
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i =  Convert.ToInt32(((Button)sender).Content); // get clicked button number
            string path = "RollingBall.Models.Levels.Level" + i.ToString();
            Type type = Type.GetType(path);
            var instanse = System.Reflection.Assembly.GetAssembly(type).CreateInstance(path);
            Level level = (Level)instanse; // get object of specific level class

            Game_page game_page = new Game_page(level);
            this.NavigationService.Navigate(game_page);// navigate
        }
    }
}
