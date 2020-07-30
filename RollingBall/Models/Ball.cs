using System;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace RollingBall.Models
{
    /// <summary>
    /// Class that describe "ball" game object, control it movement according the list of "wall" and "hole" game objects
    /// Represet Model part of MVVM pattern
    /// </summary>
    public class Ball: BindableBase, IDisposable
    {
        readonly double speed; // count of pixels ball moves by one step in vector direction
        readonly double _radius; // ball size
        public double radius { get => _radius; }
        Vector vector; //move direction
        BitmapImage _image;
        public BitmapImage image
        {
            get => _image;
            private set
            {
                SetProperty(ref _image, value);
            }
        }
        Point _position;//curent position of this ball 
        public Point position
        {
            get => _position;
            set
            {
                SetProperty(ref _position, value);
            }
        }

        ObservableCollection<Wall> walls; // List of Wall objects
        Hole hole; //aim that must be hited

        public event Action hit;//aim hit event

        // used to dispose this object
        CancellationTokenSource cancelTokenSource; 
        CancellationToken token;

        bool pause=false;//if true ball dont move

        MediaPlayer mediaPlayer = new MediaPlayer();
        string sound = "wood_sound.mp3";

        /// <summary>
        /// Define ball object
        /// </summary>
        /// <param name="position">Initial position of ball in pixels by left top window corner</param>
        /// <param name="speed">Count of pixels it moves by one step</param>
        /// <param name="radius">Ball size</param>
        /// <param name="walls">ObservableCollection of "Wall" objects</param>
        /// <param name="hole">Hole object that must be hited</param>
        public Ball(BitmapImage image, Point position, double speed, int radius, ObservableCollection<Wall> walls, Hole hole)
        {
            //Define start vector
            Random random = new Random();
            double test = random.Next(-10, 11) / 10d;
            vector = new Vector(random.Next(-10, 11) / 10d, random.Next(-10, 11) / 10d);
            vector.Normalize();

            this.image = image;
            this.position = position;
            this.speed = speed;
            this._radius = radius;
            this.walls = walls;
            this.hole = hole;

            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
           
            start_move_async(); //move task start

            mediaPlayer.Open(new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\" + sound)); //initialize MediaPlayer
        }
        /// <summary>
        /// Ethernal loop that change position
        /// </summary>
        async void start_move_async()
        {
            await Task.Run(() =>
            {
                int mps = 1000 / 40; // кол-во смещений шара (40) в секунду //moves per second
                while (true)
                {
                    if (!pause)
                    {
                        move2();//change ball position
                        if (new Segment(position, hole.position).get_lenght() < hole.radius)// level complite check
                        {
                            hit?.Invoke();
                        } 
                    }
                    if (token.IsCancellationRequested) // game class dispose
                        break;
                    Thread.Sleep(mps);
                }
            });
        }

        /// <summary>
        /// Calculte new ball position, check it for collision with walls and if true change vector of movement accompany with sound
        /// </summary>
        public void move2()
        {
            bool collide = false;
            Point new_position = position + vector * speed; // calculate new ball position
            try// InvalidOperationException "Collection was modified; enumeration operation may not execute."  error
            {
                foreach (Wall w in walls) // check new position for collide each wall
                {
                    if (geometry_lib.is_collide(new_position, radius, w)) // if collide wall segment
                    {
                        collide = true;
                        vector = geometry_lib.reflect(vector, w);
                        break;
                    }
                    if (collide == false && geometry_lib.is_collide(new_position, radius, w.A)) // if collide segment start point
                    {
                        collide = true;
                        vector = geometry_lib.reflect(position, w.A);
                        break;
                    }
                    if (collide == false && geometry_lib.is_collide(new_position, radius, w.B))// if collide segment end point
                    {
                        collide = true;
                        vector = geometry_lib.reflect(position, w.B);
                        break;
                    }
                }
                if (collide == false)
                {
                    position = new_position;
                }
                else
                {
                    ball_reflect_sound();
                }
            }
            catch { }
        }

        /// <summary>
        /// Play mediaplayer
        /// </summary>
        void ball_reflect_sound()
        {
             mediaPlayer.Dispatcher.Invoke(new Action(() => mediaPlayer.Stop()));         
             mediaPlayer.Dispatcher.Invoke(new Action(() => mediaPlayer.Play()));           
        }
        /// <summary>
        /// Prevent ball of moving
        /// </summary>
        public void on_pause()
        {
            pause = pause == true ? false : true;
        }
        /// <summary>
        /// Free ball resources: task and mediaplayer;
        /// </summary>
        public void Dispose()
        {
            cancelTokenSource.Cancel();//end move loop task
            //desposing mediaplayer 
            mediaPlayer.Stop();
            mediaPlayer.Close();
        }
    }
}
