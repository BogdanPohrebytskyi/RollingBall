using System;
using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level6 : Level
    {
        public Level6()
        {
            ball_position = new Point(50, 90);
            hole_position = new Point(50, 10);

            walls.Add(new Segment(new Point(1, 1), new Point(40, 30)));
            walls.Add(new Segment(new Point(60, 30), new Point(99, 1)));
            walls.Add(new Segment(new Point(20, 48), new Point(80, 48)));

            walls_max_count = 4;
            time = 6;
        }
    }
}
