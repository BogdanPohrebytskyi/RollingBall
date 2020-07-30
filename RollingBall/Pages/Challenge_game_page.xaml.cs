using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using RollingBall.Models.Levels;
using RollingBall.Models;

namespace RollingBall.Pages
{
    /// <summary>
    /// Inherit base game class and expand it with challenge mode functional via "new" and "override" modifiers
    /// xaml represent View part of MVVM pattern
    /// cs represent View-Model part of MVVM pattern
    /// </summary>
    public partial class Challenge_game_page : Base_game_page
    {
        int _walls_counter = 0;//curent number of walls
        public int walls_counter
        {
            get => _walls_counter;
            private set
            {
                if (_walls_counter == value)
                    return;
                _walls_counter = value;
                OnPropertyChanged("walls_counter");
            }
        }
        double _time_left; // timer
        public double time_left
        {
            get => _time_left;
            private set
            {
                if (_time_left == value)
                    return;

                if (value < 0)
                    _time_left = 0;
                else
                    _time_left = value;
                OnPropertyChanged("time_left");
            }
        }
        private DispatcherTimer challenge_timer=new DispatcherTimer(); //timer

        bool _challenge_fail = false; // timer end check
        public bool challenge_fail 
        { 
            get =>_challenge_fail; 
            private set
            {
                _challenge_fail = value;
                OnPropertyChanged("challenge_fail");
            }
        }

        // Game start and game events --------------------------------------------
        public Challenge_game_page(Level level) : base(level)
        {
            InitializeComponent();
            Loaded += (s, e) => // re enable keybord for this page
            {
                Window.GetWindow(this).KeyDown -= new KeyEventHandler(KeyDown_event);
                Window.GetWindow(this).KeyDown += new KeyEventHandler(KeyDown_event);
            };
            // Initialize timer 
            challenge_timer.Tick += challenge_timer_tick; 
            challenge_timer.Interval = TimeSpan.FromMilliseconds(100);
        }
        /// <summary>
        /// Restart for challenge mode data
        /// </summary>
        void reset_challenge()
        {
            walls_counter = 0;
            time_left = window_size.X / ball_speed / 40 + level.time;
            challenge_timer.Start();
            challenge_fail = false;
            OnPropertyChanged("level");
        }
        /// <summary>
        /// Base method + reset challenge mode data
        /// </summary>
        /// <param name="level"></param>
        protected override void load_level(Level level)
        {
            base.load_level(level);
            reset_challenge();
        }
        /// <summary>
        /// Base method + reset challenge mode data
        /// </summary>
        override protected void restart()
        {
            base.restart();
            reset_challenge();
        }

        /// <summary>
        /// Timer tick and end game check 
        /// </summary>
        protected void challenge_timer_tick(object sender, EventArgs e)
        {
            if (!level_complite)
            {
                time_left -= 0.1;
                if (time_left <= 0)
                {
                    ((DispatcherTimer)sender).Stop();
                    level_failed();
                }
            }
        }
        /// <summary>
        /// End game
        /// </summary>
        private void level_failed()
        {
            challenge_fail = true;
            ball.on_pause();
        }
        /// <summary>
        /// Base method + walls counter check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        //Mouse events (user imput) ------------------------------------------------------------------------------------
        override protected void game_area_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
        {
            if (walls_counter >= level.walls_max_count)
                return;
            base.game_area_MouseLeftButtonDown(sender, e);
        }
        /// <summary>
        /// Base method + counter ++ 
        /// </summary>
        override protected void game_area_draw_line()
        {
            if (A != null &&
                !(geometry_lib.is_collide(ball.position, ball.radius, (Point)A)
                || geometry_lib.is_collide(ball.position, ball.radius, (Point)B)
                || geometry_lib.is_collide(ball.position, ball.radius, new Segment((Point)A, (Point)B))
                ))
            {
                line_eanbled = true;
                walls.Add(new Wall((Point)A, (Point)B));

                walls_counter++;
            }
            A = null;
            B = null;
        }


        // Menu buttons -----------------------------------------------------------------------------
        /// <summary>
        /// Navigate to choose level menu
        /// </summary>
        new protected void choose_level_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Challenge_page());
        }        
        /// <summary>
        /// Base method + challnge timer resume
        /// </summary>
        override protected void resume_button_Click(object sender, RoutedEventArgs e)
        {
            challenge_timer.Start();
            base.resume_button_Click(sender, e);
        }

        // Key getch ----------------------------------------------------------------------------------------
        protected override void KeyDown_event(object sender, KeyEventArgs e)
        {
            if (challenge_fail == false)
            {
                if(e.Key== Key.Escape || e.Key == Key.Space || e.Key == Key.P)//if its pause button
                {
                    if (challenge_timer.IsEnabled)//set or decline timer on pause
                        challenge_timer.Stop();
                    else
                        challenge_timer.Start();                    
                }
                base.KeyDown_event(sender, e);//usial pause
            }
        }
      
    }
}
