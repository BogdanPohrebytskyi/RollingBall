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

namespace RollingBall.Pages
{
    /// <summary>
    /// Initialize window that containt pages 
    /// </summary>
    public partial class Main_window : Window
    {
        /// <summary>
        /// Main window initialization + page navigation settings
        /// </summary>
        public Main_window()
        {
            InitializeComponent();
            set_window_size();//change window size acording to settings

            Mainframe.NavigationService.Navigating += (s, e) => { page_dispose(); }; // dispose page after navigaton
            Mainframe.NavigationService.LoadCompleted += (s, e) => { ClearHistory(); }; // delete links to old pages to enable garbage collecton

            Mainframe.Content = new Main_menu(); // set first page
        }
        /// <summary>
        /// Set window size acording to settings or default
        /// </summary>
        void set_window_size()
        {
            try
            {
                this.Width = Properties.Settings.Default.window_size.X;
                this.Height = Properties.Settings.Default.window_size.Y;
            }
            catch
            {
                this.Width = 800;
                this.Height = 450;
            }
        }
        /// <summary>
        /// Delet lincks to old pages before navigation 
        /// </summary>
        public void ClearHistory()
        {
            if (this.Mainframe.CanGoBack || this.Mainframe.CanGoForward)
            {
                while (this.Mainframe.RemoveBackEntry() != null)
                {
                    this.Mainframe.RemoveBackEntry();
                    GC.Collect(); // Start .NET CLR Garbage Collection
                    GC.WaitForPendingFinalizers(); // Wait for Garbage Collection to finish
                }
            }
        }
        /// <summary>
        /// Stop ball move task and dispose it with game page after navigation
        /// </summary>
        private void page_dispose()
        {
            if (Mainframe.NavigationService.Content is IDisposable)
            {
                ((IDisposable)Mainframe.NavigationService.Content).Dispose();
                GC.Collect(); // Start .NET CLR Garbage Collection
                GC.WaitForPendingFinalizers(); // Wait for Garbage Collection to finish
            }
        }
    }
}
