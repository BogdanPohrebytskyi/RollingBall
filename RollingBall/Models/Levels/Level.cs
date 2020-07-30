using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RollingBall.Models;

namespace RollingBall.Models.Levels
{
    /// <summary>
    /// Contains start ball position, hole position and position of static walls. 
    /// Data in percentage value, 1-99 strictly
    /// </summary>
    public abstract class Level
    {
        List<Segment> _walls = new List<Segment>{ new Segment ( new Point(1,1),new Point(99,1)),
                                                    new Segment ( new Point(1,1),new Point(1,99)),
                                                    new Segment ( new Point(99,1),new Point(99,99)),
                                                    new Segment ( new Point(1,99),new Point(99,99))
        };
        public List<Segment> walls { get { return _walls; } protected set { _walls = value; } }
        Point _ball_position;
        public Point ball_position { get { return _ball_position; } protected set { _ball_position = value; } }
        Point _hole_position;
        public Point hole_position { get { return _hole_position; } protected set { _hole_position = value; } }
        int _walls_max_count;
        public int walls_max_count { get { return _walls_max_count; } protected set { _walls_max_count = value; } }
        int _time;
        public int time { get { return _time; } protected set { _time = value; } }

    }
}
