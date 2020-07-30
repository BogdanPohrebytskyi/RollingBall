using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level1 : Level
    {
        public Level1()
        {
            ball_position = new Point(20, 50);
            hole_position = new Point(80, 50);

            walls.Add(new Segment(new Point(50, 1), new Point(50, 75)));

            walls_max_count = 3;
            time = 5;
        }
    }
}
