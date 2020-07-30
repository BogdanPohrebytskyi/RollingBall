using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level10 : Level
    {
        public Level10()
        {
            ball_position = new Point(10, 25);
            hole_position = new Point(90, 75);

            walls.Add(new Segment(new Point(25, 50), new Point(75, 50)));
            walls.Add(new Segment(new Point(50, 25), new Point(50, 75)));

            walls.Add(new Segment(new Point(15, 15), new Point(30, 30)));
            walls.Add(new Segment(new Point(15, 30), new Point(30, 15)));

            walls.Add(new Segment(new Point(85, 85), new Point(70, 70)));
            walls.Add(new Segment(new Point(70, 85), new Point(85, 70)));

            walls.Add(new Segment(new Point(85, 15), new Point(70, 30)));
            walls.Add(new Segment(new Point(70, 15), new Point(85, 30)));

            walls.Add(new Segment(new Point(15, 85), new Point(30, 70)));
            walls.Add(new Segment(new Point(15, 70), new Point(30, 85)));

            walls_max_count = 8;
            time = 10;
        }
    }
}
