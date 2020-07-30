using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level4 : Level
    {
        public Level4()
        {
            ball_position = new Point(10, 10);
            hole_position = new Point(90, 90);

            walls.Add(new Segment(new Point(25, 1), new Point(25, 20)));
            walls.Add(new Segment(new Point(1, 40), new Point(75, 40)));
            walls.Add(new Segment(new Point(25, 60), new Point(99, 60)));
            walls.Add(new Segment(new Point(75, 80), new Point(75, 99)));

            walls_max_count = 7;
            time = 17;
        }
    }
}
