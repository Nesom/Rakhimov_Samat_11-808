using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NearestPair
{
    class Program
    {
        static double GetNearestPair(List<Point> points)
        {
            if (points.Count == 0) throw new ArgumentException("Sorry, you should have more points:(");
            if (points.Count == 1) return 0;
            if (points.Count == 3) return Math.Min(GetNearestPair(points.Take(2).ToList()),
                Math.Min(GetNearestPair(points.Skip(1).ToList()),
                GetNearestPair(points.Where(r => r != points[1]).ToList())));
            else if (points.Count > 2)
            {
                double median = FindMedian(points);
                var leftPoints = points.Take(points.Count / 2).ToList();
                var rightPoints = points.Skip(points.Count / 2).ToList();
                var leftMin = GetNearestPair(leftPoints);
                var rightMin = GetNearestPair(rightPoints);
                double min = Math.Min(leftMin, rightMin);
                foreach (var e in leftPoints.Where(point => point.X<=median&&point.X>median-min))
                {
                    foreach(var neE in rightPoints.Where(point=>
                    Math.Abs(point.Y-e.Y)<=Math.Min(leftMin,rightMin)&&point.X>=median&&point.X<=median+min))
                    {
                        if (Math.Sqrt((e.X - neE.X) * (e.X - neE.X) + (e.Y - neE.Y) * (e.Y - neE.Y)) < min)
                            min = Math.Sqrt((e.X - neE.X) * (e.X - neE.X) + (e.Y - neE.Y) * (e.Y - neE.Y));
                    }
                }
                return min;
            }
            else if (points.Count == 2) return Math.Sqrt(((points[0].X - points[1].X) * (points[0].X - points[1].X) + (points[0].Y - points[1].Y) * (points[0].Y - points[1].Y)));
            return 0;
        }
        static void Main(params string[] args)
        {
            var list = new List<Point>
            {
                new Point { X = 2, Y = 0 },
                new Point { X = 1, Y = 0 },
                new Point { X = 3, Y = 0 },
                new Point { X = 4, Y = 0 },
                new Point { X = 5, Y = 6 }
            };
            
            var f = GetNearestPair(list.OrderBy(x => x.X).ThenBy(y => y.Y).ToList());
            Console.WriteLine(f);
        }

        public static double FindMedian(List<Point> points)
        {
            if (points.Count % 2 == 0) return (points[points.Count / 2-1].X + points[points.Count / 2 ].X)/2.0;
            else return points[points.Count / 2 + 1].X;
        }
    }
}
