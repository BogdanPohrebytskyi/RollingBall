using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RollingBall.Models
{
    /// <summary>
    /// Math model. Calculate intersectons and reflections.
    /// Used in Game and Ball classes
    /// </summary>
    public static class geometry_lib
    {
        /// <summary>
        /// Circle and segment intersect check
        /// Firs build a triangle of segment ends(A,B) and circle center(C). 
        /// Then dropping the altitude at vertex C and check if it lenght < Circle radius 
        /// </summary>
        /// <param name="circle_center">Circle center position</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="segment">Segment</param>
        /// <returns>true - collide, false - not</returns>
        public static bool is_collide(Point circle_center, double radius, Segment segment)
        {
            //Check if altitude cross segment
            if (!((Math.Max(segment.A.X, segment.B.X) >= circle_center.X && Math.Min(segment.A.X, segment.B.X) <= circle_center.X) 
                || (Math.Max(segment.A.Y, segment.B.Y) >= circle_center.Y && Math.Min(segment.A.Y, segment.B.Y) <= circle_center.Y)))
                return false;

            Segment AB = segment;
            Segment AC = new Segment(segment.A, circle_center);
            double ab = AB.get_lenght();

            double S = Vector.CrossProduct(AB.get_vector(), AC.get_vector()) / 2; //Area of a triangle ABC as half of parallelogram area. Parallelogram area is cross product of AB and AC vectors.
            double ch = 2 * S / ab; //altitude lenght

            /*
            double p = (ab + bc + ac) / 2;
            double ch2 = (2 * Math.Sqrt(p * (p - ab) * (p - bc) * (p - ac))) / ab; // длина высоты треугольника

            double ah = Math.Sqrt(Math.Pow(ac, 2) - Math.Pow(ch, 2)); // ратояние между точкой А и местом пересечения высоты и отрезка АВ
            Vector _AB = AB.get_vector();
            _AB.Normalize();
            Point H = A + ah * _AB; // место пересечения высоты и отрезка АВ
            */

            return Math.Abs(ch) < radius;
        }

        /// <summary>
        /// Circle and point intersect check
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="point"></param>
        /// <returns>true - collide, false - not</returns>
        public static bool is_collide(Point position, double radius, Point point)  // проверка на пренадлежность точки окружности
        {
            if (Math.Abs(position.X - point.X) <= radius && Math.Abs(position.Y - point.Y) <= radius)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Two segment intersect check
        /// </summary>
        /// <param name="AB">Segment 1</param>
        /// <param name="CD">Segment 2</param>
        /// <returns>true - collide, false - not</returns>
        public static bool is_collide(Segment AB, Segment CD)
        {
            //Introduction to Algorithms by Thomas H.Cormen(Author), Charles E. Leiserson(Author), Ronald L. Rivest(Author), Clifford Stein
            //chapter 35 COMPUTATIONAL GEOMETRY; Subchapter 35.1 Line-segment properties; Paragraph Determining whether two line segments intersect

            bool result = true;
            double x1 = AB.A.X < AB.B.Y ? AB.A.X : AB.B.X;
            double y1 = AB.A.Y < AB.B.Y ? AB.A.Y : AB.B.Y;

            double x2 = AB.A.X > AB.B.X ? AB.A.X : AB.B.X;
            double y2 = AB.A.Y > AB.B.Y ? AB.A.Y : AB.B.Y;

            double x3 = CD.A.X < CD.B.X ? CD.A.X : CD.B.X;
            double y3 = CD.A.Y < CD.B.Y ? CD.A.Y : CD.B.Y;

            double x4 = CD.A.X > CD.B.X ? CD.A.X : CD.B.X;
            double y4 = CD.A.Y > CD.B.Y ? CD.A.Y : CD.B.Y;

            if (!(x2 >= x3) && (x4 >= x1) && (y2 >= y3) && (y4 >= y1)) // bounding boxes intersect check 
                result = false;

            if (result == true)
                if (!(System.Windows.Vector.CrossProduct((CD.A - AB.A), (AB.B - AB.A)) * System.Windows.Vector.CrossProduct((CD.B - AB.A), (AB.B - AB.A)) <= 0)) // if end points of segment 1 lies on different sides of segment 2 
                    result = false;

            if (result == true)
                if (!(System.Windows.Vector.CrossProduct((AB.A - CD.A), (CD.B - CD.A)) * System.Windows.Vector.CrossProduct((AB.B - CD.A), (CD.B - CD.A)) <= 0))// Check if points of segment 1 coincident segment 2
                    result = false;

            return result;
        }
        /// <summary>
        /// Two segments intersect position
        /// </summary>
        /// <param name="AB">Segment 1</param>
        /// <param name="CD">Segment 2</param>
        /// <returns></returns>
        public static Point get_collide_point(Segment AB, Segment CD) // нахождение точки пересечения двух отрезков
        {
            //https://ru.wikipedia.org/wiki/Пересечение_прямых#Если_заданы_по_две_точки_на_каждой_прямой
            //x1 = AB.A.x
            //y1 = AB.A.y
            //x2 = AB.B.x
            //y2 = AB.B.y

            //x3 = CD.A.x
            //y3 = CD.A.y
            //x4 = CD.B.x
            //y4 = CD.B.y
            double x = ((AB.A.X * AB.B.Y - AB.A.Y * AB.B.X) * (CD.A.X - CD.B.X) - (AB.A.X - AB.B.X) * (CD.A.X * CD.B.Y - CD.A.Y * CD.B.X)) /
                       ((AB.A.X - AB.B.X) * (CD.A.Y - CD.B.Y) - (AB.A.Y - AB.B.Y) * (CD.A.X - CD.B.X));
            double y = ((AB.A.X * AB.B.Y - AB.A.Y * AB.B.X) * (CD.A.Y - CD.B.Y) - (AB.A.Y - AB.B.Y) * (CD.A.X * CD.B.Y - CD.A.Y * CD.B.X)) /
                       ((AB.A.X - AB.B.X) * (CD.A.Y - CD.B.Y) - (AB.A.Y - AB.B.Y) * (CD.A.X - CD.B.X));
            return new Point(x, y);
        }

        /// <summary>
        /// Reflect vector from segment
        /// </summary>
        /// <param name="I">vector</param>
        /// <param name="AB">segment</param>
        /// <returns>new vector</returns>
        public static Vector reflect(System.Windows.Vector I, Segment AB)//расчет вектора отражения от отрезка
        {
            System.Windows.Vector n = new System.Windows.Vector(AB.A.Y - AB.B.Y, AB.B.X - AB.A.X);//Normal to vector 
            System.Windows.Vector result = I - 2 * n * ((I * n) / (n * n));// Reflected vector 
            result.Normalize();

            return result;
        }
        /// <summary>
        /// Reflect from point
        /// </summary>
        /// <param name="position">Position of reflected object</param>
        /// <param name="point">Point</param>
        /// <returns>Vector of reflection</returns>
        public static Vector reflect(Point position, Point point)//расчет вектора отражения от точки
        {
            Vector new_vector = point - position;
            new_vector = new_vector * -1; // reflect
            new_vector.Normalize();
            return new_vector;
        }
    }
}
