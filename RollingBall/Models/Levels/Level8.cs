using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level8 : Level
    {
        public Level8()
        {
            ball_position = new Point(8, 90);
            hole_position = new Point(92, 90);

            walls.Add(new Segment(new Point(16, 99), new Point(16, 25)));
            walls.Add(new Segment(new Point(16, 25), new Point(32, 25)));
            walls.Add(new Segment(new Point(32, 25), new Point(32, 99)));
            walls.Add(new Segment(new Point(48, 1), new Point(48, 75)));
            walls.Add(new Segment(new Point(64, 25), new Point(64, 99)));
            walls.Add(new Segment(new Point(80, 50), new Point(80, 99)));

            walls_max_count = 7;
            time = 12;
        }
    }
}
