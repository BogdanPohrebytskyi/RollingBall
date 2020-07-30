using System.Windows;

namespace RollingBall.Models.Levels
{
    public sealed class Level9 : Level
    {
        public Level9()
        {
            ball_position = new Point(15, 80);
            hole_position = new Point(85, 80);

            walls.Add(new Segment(new Point(50, 20), new Point(50, 99)));
            walls.Add(new Segment(new Point(20, 20), new Point(65, 20)));
            walls.Add(new Segment(new Point(75, 20), new Point(99, 20)));
            walls.Add(new Segment(new Point(1, 40), new Point(20, 40)));

            walls_max_count = 4;
            time = 7;
        }
    }
}
