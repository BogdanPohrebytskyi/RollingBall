using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RollingBall.Models;
using RollingBall.Models.Levels;

namespace RollingBall.Pages
{
    /// <summary>
    /// Main game class.
    /// It create objects of main classes (Ball, Wall, Hole) that process data. Also it control GUI and user input.
    /// Used to realize inheritance and implement new challenge mode functional.
    /// Represent View-Model part of MVVM pattern.
    /// </summary>
    public class Base_game_page : Page, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Ball ball { get; set; } 
        public Hole hole { get; set; } 
        public ObservableCollection<Wall> walls { get; set; }
        public Point window_size { get; set; }//Data from "Properties.Settings" to calculate game object positions
        public Level level { get; set; }// current level


        //Used for new wall creation
        Point? _A, _B; 
        public Point? A
        {
            get => _A;
            set
            {
                _A = value;
                OnPropertyChanged("A");
            }
        }
        public Point? B
        {
            get => _B;
            set
            {
                _B = value;
                OnPropertyChanged("B");
            }
        }
        bool _line_eanbled = true;
        public bool line_eanbled
        {
            get => _line_eanbled;
            set
            {
                _line_eanbled = value;
                OnPropertyChanged("line_eanbled");
            }
        }


        //Predeterminated data
        int point_size = 5;
        int ball_radius = 25;
        int hole_radius = 30;// ball_radius + ball_radius / 5; 
        protected double ball_speed = 3d;

       
        bool _pause = false; // Game pause
        public bool pause
        {
            get => _pause;
            private set
            {
                _pause = value;
                OnPropertyChanged("pause");
            }
        }

        bool _level_complite = false; // Show level complite menu
        public bool level_complite
        {
            get => _level_complite;
            private set
            {
                _level_complite = value;
                OnPropertyChanged("level_complite");
            }
        }


        // GUI initializetion, game start and game events  -------------------------------------------------------------------------------------------
        public Base_game_page(Level level)
        {
            //InitializeComponent();
            window_size = Properties.Settings.Default.window_size;// set window size
            DataContext = this;
            Loaded += (s, e) => // enable keybord for this page
            {
                Window.GetWindow(this).KeyDown += new KeyEventHandler(KeyDown_event);
            };
            load_level(level);//game start
        }
        /// <summary>
        /// Initialize walls, ball, and hole game objects 
        /// </summary>
        /// <param name="level">Current level</param>
        virtual protected void load_level(Level level)
        {
            this.level = level;
            walls = new ObservableCollection<Wall>();
            walls.CollectionChanged += TimersOnCollectionChanged;
            walls.CollectionChanged += (s, e) => OnPropertyChanged("walls");
            foreach (Segment s in level.walls) // load walls
                walls.Add(new Wall(new Point((window_size.X - 20) * (s.A.X / 100),
                                            (window_size.Y - 40) * (s.A.Y / 100)),
                                   new Point((window_size.X - 20) * (s.B.X / 100),
                                            (window_size.Y - 40) * (s.B.Y / 100)), false));
            hole = create_hole(new Point((window_size.X - 20) * (level.hole_position.X / 100), (window_size.Y - 40) * (level.hole_position.Y / 100)),
                hole_radius);//load hole
            OnPropertyChanged("hole");

            ball = create_ball(new Point((window_size.X - 20) * (level.ball_position.X / 100), (window_size.Y - 40) * (level.ball_position.Y / 100)),
                ball_speed, ball_radius, walls, hole, Properties.Settings.Default.ball_name); // load ball
        }
        /// <summary>
        /// Quick reset: move ball to start posotion, delete mortal walls 
        /// </summary>
        virtual protected void restart()
        {
            //walls.Clear();
            //foreach (Segment s in level.walls)
            //    walls.Add(new Wall(new Point((window_size.X - 20) * (s.A.X / 100),
            //                                (window_size.Y - 40) * (s.A.Y / 100)),
            //                       new Point((window_size.X - 20) * (s.B.X / 100),
            //                                (window_size.Y - 40) * (s.B.Y / 100)), false));
            foreach (Wall w in walls)
                if (w.mortal == true)
                    walls.Remove(w);
            ball.position = new Point((window_size.X - 20) * (level.ball_position.X / 100), (window_size.Y - 40) * (level.ball_position.Y / 100));
        }
        /// <summary>
       /// Stop ball, show pause menu
       /// </summary>
        void on_pause()
        {
            ball.on_pause();
            pause = pause == true ? false : true;
            //pause_grid.Visibility = pause_grid.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
        /// <summary>
        /// Dispose ball object
        /// </summary>
        virtual public void Dispose()
        {
            Window.GetWindow(this).KeyDown -= new KeyEventHandler(KeyDown_event);
            ball_dispose();
        }
        void ball_dispose()
        {
            ball.PropertyChanged -= (s, e) => { OnPropertyChanged("ball"); };
            ball.hit -= level_complite_event;
            ball.Dispose();
            ball = null;
            GC.Collect(); // Start .NET CLR Garbage Collection
            GC.WaitForPendingFinalizers(); // Wait for Garbage Collection to finish
        }
        /// <summary>
        /// Return next level or null
        /// </summary>
        Level find_next_level(Level level)
        {
            string s = new string(level.ToString().Where(char.IsDigit).ToArray());//current level number
            int i = Convert.ToInt32(s) + 1;//next level number
            string path = "RollingBall.Models.Levels.Level" + i.ToString();
            try//try to find next level
            {
                Type type = Type.GetType(path);
                var instanse = System.Reflection.Assembly.GetAssembly(type).CreateInstance(path);
                return (Level)instanse;
            }
            catch//search fail
            {
                return null;
            }
        }


        //Ball and Walls events -----------------------------------------------------------------
        /// <summary>
        /// Add update and delete on ObservableCollection collection changed 
        /// </summary>
        protected void TimersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)//subscribe new wall
            {
                foreach (Wall newItem in e.NewItems)
                {
                    newItem.Ended += DeleteOldItemsEvent;
                    newItem.updated += update_old_items_event;
                }
            }

            if (e.OldItems != null)//unsubscrube deleted wall
            {
                foreach (Wall old_item in e.OldItems)
                {
                    old_item.Ended -= DeleteOldItemsEvent;
                    old_item.updated -= update_old_items_event;
                }
            }
        }
        /// <summary>
        /// Change wall on wall timer tick. Currently dont used 
        /// </summary>
        protected void update_old_items_event(Wall w)
        {

        }
        /// <summary>
        /// Delete subscribed wall
        /// </summary>
        protected void DeleteOldItemsEvent(Wall w)
        {
            walls.Remove(w);
        }
        /// <summary>
        /// Calls when ball hit hole. Show level complite menu
        /// </summary>
        protected void level_complite_event()
        {
            level_complite = true;
            ball.on_pause();
        }
        
        //Mouse events (user imput)------------------------------------------------------------------
        /// <summary>
        /// Start drawing wall 
        /// </summary>
        virtual protected void game_area_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            A = new Point(e.GetPosition((Canvas)sender).X - point_size / 2, e.GetPosition((Canvas)sender).Y - point_size / 2);
            B = new Point(e.GetPosition((Canvas)sender).X - point_size / 2, e.GetPosition((Canvas)sender).Y - point_size / 2);
        }
        /// <summary>
        /// Drawing wall
        /// </summary>
        protected void game_area_MouseMove(object sender, MouseEventArgs e)
        {
            if (A != null)//if drawning wall
            {
                B = new Point(e.GetPosition((Canvas)sender).X - point_size / 2, e.GetPosition((Canvas)sender).Y - point_size / 2);

                if (geometry_lib.is_collide(ball.position, ball.radius, (Point)A)
                    || geometry_lib.is_collide(ball.position, ball.radius, (Point)B)
                    || geometry_lib.is_collide(ball.position, ball.radius, new Segment((Point)A, (Point)B))
                    ) // if wall call be drawed 
                {
                    line_eanbled = false;
                }
                else
                {
                    line_eanbled = true;
                }
            }
        }
        /// <summary>
        /// Draw wall on mouse button up
        /// </summary>
        protected void game_area_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            game_area_draw_line();
        }
        /// <summary>
        /// Cansile drawing
        /// </summary>
        protected void game_area_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (A != null)
                A = B = null;
        }
        /// <summary>
        /// Draw wall when cursor leave window
        /// </summary>
        protected void game_area_MouseLeave(object sender, MouseEventArgs e)
        {
            game_area_draw_line();
        }
        /// <summary>
        /// Draw wall
        /// </summary>
        virtual protected void game_area_draw_line()
        {
            if ( A != null &&
                !(geometry_lib.is_collide(ball.position, ball.radius, (Point)A)
                || geometry_lib.is_collide(ball.position, ball.radius, (Point)B)
                || geometry_lib.is_collide(ball.position, ball.radius, new Segment((Point)A, (Point)B))
                )) // if wall can be drawed 
            {
                line_eanbled = true;
                walls.Add(new Wall((Point)A, (Point)B));
            }
            A = null;// clear
            B = null;
        }

        //GUI elements creation -------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Return new ball object
        /// </summary>
        /// <param name="position">Start ball posotion</param>
        /// <param name="ball_speed"></param>
        /// <param name="ball_radius">Ball size</param>
        /// <param name="walls">ObservableCollection list</param>
        /// <param name="hole">Hole object</param>
        /// <param name="image_name"></param>
        /// <returns>New ball</returns>
        Ball create_ball(Point position, double ball_speed, int ball_radius, ObservableCollection<Wall> walls, Hole hole, string image_name = "Ball.png")
        {
            Ball new_ball = new Ball(new BitmapImage(get_resource_by_URI(image_name)),
                position,
                ball_speed,
                ball_radius,
                walls,
                hole);
            new_ball.PropertyChanged += (s, e) => { OnPropertyChanged("ball"); };
            new_ball.hit += level_complite_event;
            return new_ball;
        }
        /// <summary>
        /// Return new ball object
        /// </summary>
        /// <param name="position">Hole position</param>
        /// <param name="radius">Size</param>
        /// <param name="image_name"></param>
        /// <returns></returns>
        Hole create_hole(Point position, double radius, string image_name = "Hole.png")
        {
            Hole new_hole = new Hole(
                position,
                radius,
                new BitmapImage(get_resource_by_URI(image_name))
                );
            new_hole.PropertyChanged += (s, e) => OnPropertyChanged("hole");
            return new_hole;
        }
        /// <summary>
        /// Return URI of file in resources folder
        /// </summary>
        /// <param name="resource_name">File name</param>
        System.Uri get_resource_by_URI(string resource_name)
        {
            return new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\" + resource_name);
        }

        //Buttons----------------------------------------------------------------
        /// <summary>
        /// End pause
        /// </summary>
        virtual protected void resume_button_Click(object sender, RoutedEventArgs e)
        {
            on_pause();
        }
        /// <summary>
        /// Restart
        /// </summary>
        protected void restart_button_Click(object sender, RoutedEventArgs e)
        {
            restart();
            on_pause();
        }
        /// <summary>
        /// Launch this level again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void restart_level_button_Click(object sender, RoutedEventArgs e)
        {
            restart();
            ball.on_pause();
            level_complite = false;
        }
        /// <summary>
        /// Launch next level if it exist or this level again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void next_button_Click(object sender, RoutedEventArgs e)
        {
            Level next_level = find_next_level(level);
            if (next_level != null)
            {
                ball_dispose();
                load_level(next_level);
            }
            level_complite = false;
        }
        /// <summary>
        /// Back to level page
        /// </summary>
        protected void choose_level_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Arcade_page());
        }
        /// <summary>
        /// Navigate to main menu page
        /// </summary>
        protected void exit_button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Main_menu());
        }

        //Key getch --------------------------------------
        virtual protected void KeyDown_event(object sender, KeyEventArgs e)
        {
            if (!level_complite)
            {
                switch (e.Key)
                {
                    case Key.Escape:
                    case Key.Space:
                    case Key.P:
                        on_pause();
                        break;
                    case Key.R:
                        restart();
                        break;
                }
            }
        }
    }
}

