using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RollingBall.Models
{
    /// <summary>
    /// Class that describe "hole" game object. Just contain data
    /// </summary>
    public class Hole : BindableBase
    {
        BitmapImage _image;
        public BitmapImage image
        {
            get => _image;
            private set
            {
                SetProperty(ref _image, value);
            }
        }

        double _radius; // hole size
        public double radius
        {
            get => _radius;
            private set
            {
                SetProperty(ref _radius, value);
            }
        }

        Point _position; 
        public Point position
        {
            get => _position;
            private set
            {
                SetProperty(ref _position, value);
            }
        }


        public Hole(Point position, double radius, BitmapImage image)
        {
            this.radius = radius;
            this.position = position;
            this.image = image;
        }
    }
}
