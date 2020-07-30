using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level3 : Level
    {
        public Level3()
        {
            ball_position = new Point(10, 10);
            hole_position = new Point(50, 10);

            walls.Add(new Segment(new Point(25, 1), new Point(25, 40)));
            walls.Add(new Segment(new Point(45, 1), new Point(45, 40)));
            walls.Add(new Segment(new Point(55, 1), new Point(55, 40)));
            walls.Add(new Segment(new Point(75, 1), new Point(75, 40)));
            walls.Add(new Segment(new Point(25, 40), new Point(45, 40)));
            walls.Add(new Segment(new Point(55, 40), new Point(75, 40)));
            walls.Add(new Segment(new Point(25, 60), new Point(75, 60)));
            walls.Add(new Segment(new Point(25, 60), new Point(25, 99)));
            walls.Add(new Segment(new Point(75, 60), new Point(75, 99)));

            walls_max_count = 3;
            time = 4;
        }
    }
}
