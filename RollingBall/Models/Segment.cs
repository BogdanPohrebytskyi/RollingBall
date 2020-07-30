using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
namespace RollingBall.Models
{
    /// <summary>
    /// Math model of Line segment
    /// </summary>
    public class Segment
    {
        readonly Point _A, _B;
        public Point A { get => _A; }
        public Point B { get => _B; }

        public Segment(System.Windows.Point A, System.Windows.Point B)
        {
            this._A = A;
            this._B = B;
        }
        public Vector get_vector()
        {
            return new Vector(A.X - B.X, A.Y - B.Y);
        }
        public double get_lenght()
        {
            return Math.Sqrt(Math.Pow(B.X - A.X, 2) + Math.Pow(B.Y - A.Y, 2));
        }
    }
}
