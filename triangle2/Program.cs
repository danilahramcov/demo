using System;
using System.Linq;
using System.Text;

namespace Тtriangle2
{
    public class Point3D
    {
        public double X;
        public double Y;
        public double Z;

        public Point3D(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }
    }
    public class Vector
    {
        public double X;
        public double Y;
        public double Z;

        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public static double DotProduct(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
    }
    public class Line
    {
        // Точка, через которую проходит прямая
        public Point3D M;
        // Нормальный вектор
        public Vector p;
        public Line(Point3D m, Vector p)
        {
            M = m;
            this.p = p;
        }

        public Line(Point3D p1, Point3D p2)
        {
            M = p1;
            p = new Vector(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        }
    }

    public class triangle
    {
        double A, B, C, D;
        int M1, M2, M3;
        public int m { get; set; }
        public int m2 { get; set; }
        public int m3 { get; set; }
        // Задание плоскости по трём точкам
        public triangle(Point3D p1, Point3D p2, Point3D p3)
        {
            A = (p2.Y - p1.Y) * (p3.Z - p1.Z) - (p3.Y - p1.Y) * (p2.Z - p1.Z);
            B = (p3.X - p1.X) * (p2.Z - p1.Z) - (p2.X - p1.X) * (p3.Z - p1.Z);
            C = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            D = -p1.X * A - p1.Y * B - p1.Z * C;
        }
        public triangle(double a, double b, double c, double d)
        {
            M1 = m;
            M2 = m2;
            M3 = m3;
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }
        // Точка пересечения прямой и данной плоскости
        public Point3D? CrossPoint(Line l)
        {
            //Если скалярное произведение нормалей прямой и плоскости равно нулю, значит они не пересекаются
            if (Vector.DotProduct(l.p, new Vector(A, B, C)) == 0)
            {
                Console.WriteLine("Прямая не пересикает триугольник.");
                return null;
            }
            Console.Write("Прямая пересекает триугольник. ");
            double t0 = -(A * l.M.X + B * l.M.Y + C * l.M.Z + D) / (A * l.p.X + B * l.p.Y + C * l.p.Z);
            M1 = (int)(t0 * l.p.X + l.M.X);
            M2 = (int)(t0 * l.p.Y + l.M.Y);
            M3 = (int)(t0 * l.p.Z + l.M.Z);
            return new Point3D(M1, M2, M3);
        }

        //длина вектора до точки пересечения с треугольником
        public int LenghtM()
        {
            int m = M1 - 1;
            int m2 = M2 - (-2);
            int m3 = M3 - (-1);
            int lenghtM = (int)(Math.Sqrt(Math.Pow(m, 2) + Math.Pow(m2, 2) + Math.Pow(m3, 2)));
            return lenghtM;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int x1, x2, x3, y1, y2, y3, z1, z2, z3;

            Random rng = new Random();
            Console.Write("Введите количество треугольников: ");
            int kol = Convert.ToInt32(Console.ReadLine());
            Line l = new Line(new Point3D(1, -2, -1), new Point3D(-1, 4, 1));
            int[] mas = new int[kol];
            for (int i = 0; i < kol; i++)
            {
                x1 = rng.Next(-10, 10);
                x2 = rng.Next(-10, 10);
                x3 = rng.Next(-10, 10);
                y1 = rng.Next(-10, 10);
                y2 = rng.Next(-10, 10);
                y3 = rng.Next(-10, 10);
                z1 = rng.Next(-10, 10);
                z2 = rng.Next(-10, 10);
                z3 = rng.Next(-10, 10);
                triangle tr = new triangle(new Point3D(x1, x2, x3), new Point3D(y1, y2, y3), new Point3D(z1, z2, z3));
                Console.Write("Треугольник " + (i + 1) + ". Точка пересечения: " + tr.CrossPoint(l) + " ");
                Console.Write("Длина: " + tr.LenghtM());
                mas[i] = tr.LenghtM();
                Console.WriteLine();
            }

            // Находим минимальное значение
            int minVal = mas.Min();
            // Находим индекс
            int indexMin = Array.IndexOf(mas, minVal);
            Console.WriteLine();
            Console.WriteLine("Самый ближний треугольник, который пересекает прямая, єто треугольник: " + (indexMin + 1) + " его длина " + minVal);
        }
    }
}
