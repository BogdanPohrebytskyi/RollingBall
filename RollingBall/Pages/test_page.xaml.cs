using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace RollingBall.Pages
{
    /// <summary>
    /// Логика взаимодействия для test_page.xaml
    /// </summary>
    public partial class test_page : Page
    {
        public test_page()
        {
            Console.WriteLine("start");
            Thread.Sleep(1000);
            InitializeComponent();
            test t = new test();
            t.hit += event_method;

            Console.WriteLine("test created");
            Thread.Sleep(1000);

            t._image = new BitmapImage(get_resource_by_URI("Ball.png"));

            Console.WriteLine("image loaded");
            Thread.Sleep(1000);

            Thread.Sleep(1000);
            t = null;
            GC.Collect();
            Console.WriteLine("disposed");
        }


        System.Uri get_resource_by_URI(string resource_name)
        {
            return new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\" + resource_name);
        }
        void event_method() { }

        ~test_page() { Console.WriteLine("test dispose"); }
    }
}
