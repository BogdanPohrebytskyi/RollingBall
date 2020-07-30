using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level7 : Level
    {
        public Level7()
        {
            ball_position = new Point(13, 13);
            hole_position = new Point(33, 66);

            walls.Add(new Segment(new Point(1, 25), new Point(75, 25)));
            walls.Add(new Segment(new Point(75, 25), new Point(75, 75)));
            walls.Add(new Segment(new Point(75, 75), new Point(25, 75)));
            walls.Add(new Segment(new Point(25, 75), new Point(25, 50)));
            walls.Add(new Segment(new Point(25, 50), new Point(50, 50)));

            walls_max_count = 7;
            time = 13;
        }
    }
}
