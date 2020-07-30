using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level5 : Level
    {
        public Level5()
        {
            ball_position = new Point(10, 20);
            hole_position = new Point(37.5, 50);

            walls.Add(new Segment(new Point(1, 50), new Point(37.5, 25)));
            walls.Add(new Segment(new Point(37.5, 25), new Point(75, 50)));
            walls.Add(new Segment(new Point(75, 50), new Point(37.5, 75)));
            walls.Add(new Segment(new Point(37.5, 75), new Point(20, 63)));

            walls_max_count = 4;
            time = 11;
        }
    }
}
