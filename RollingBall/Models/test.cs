using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RollingBall.Models
{
    class test
    {
        public event Action hit;//aim hit event
        public BitmapImage _image;

        MediaPlayer mediaPlayer = new MediaPlayer();
        string sound = "wood_sound.mp3";

        CancellationTokenSource cancelTokenSource;
        CancellationToken token;
        public test() 
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            mediaPlayer.Open(new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\" + sound)); //initialize MediaPlayer
            mediaPlayer.Play();
        }

        ~test() { Console.WriteLine("test dispose"); }
    }
}
