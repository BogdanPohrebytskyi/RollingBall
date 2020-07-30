using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level2 : Level
    {
        public Level2()
        {
            ball_position = new Point(30, 20);
            hole_position = new Point(60, 20);

            walls.Add(new Segment(new Point(50, 1), new Point(50, 32)));
            walls.Add(new Segment(new Point(50, 67), new Point(50, 99)));
            walls.Add(new Segment(new Point(25, 33), new Point(75, 33)));
            walls.Add(new Segment(new Point(25, 66), new Point(75, 66)));

            walls_max_count = 4;
            time = 6;
        }
    }
}
