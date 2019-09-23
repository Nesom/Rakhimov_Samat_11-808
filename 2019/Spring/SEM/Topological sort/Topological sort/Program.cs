using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace TopologicalSort
{
    class Program
    {
        //here all fields+Main
        #region
        public static int depth = 0;
        public static Dictionary<int, bool> dict = new Dictionary<int, bool>();
        public static Dictionary<int, bool> dict1 = new Dictionary<int, bool>();
        public static Stack<int> blacks = new Stack<int>();
        public static Stack<int> blacks1 = new Stack<int>();
        public static List<int>[] graph = new List<int>[4] { new List<int> { 3 }, null, new List<int> { 1 }, new List<int> { 1, 2 } };
        public static LinkedList<int[]> ar = new LinkedList<int[]>();
        static void Main(string[] args)
        {

            ar.AddLast(new int[] { 3 });
            ar.AddLast(new int[0]);
            ar.AddLast(new int[] { 1 });
            ar.AddLast(new int[] { 1, 2 });
            //SortTopologically(graph);
            //SortList(ar);
            //for (int i = 0; i < blacks1.Count; i++)
            //    Console.WriteLine(blacks1.Pop());
            //Console.WriteLine(blacks1.Pop());
            //Console.WriteLine(blacks1.Pop());
            //Console.ReadKey();
            //var watch = new Stopwatch();
            //watch.Start();
            //for (int m = 3; m < 10; m++)
            //{
            //    Console.WriteLine(0);
            //for (int i = 0; i < 100; i++)
            //{
                var t = Generate(10);
            SortTopologically(t);
            //for (int i = 0;i<t.Length; i++)
            //{
            //    for (int j = 0; j < t[i].Count; j++)
            //        Console.Write(t[i][j]);
            //    Console.WriteLine();
            //}
            //SortTopologically(t);
            //    Console.WriteLine(i);
            //    SortTopologically(t);
            //}
            //    SortTopologically(t);
            //}
            //watch.Stop();
            //Console.Write(watch.ElapsedMilliseconds);
            //for (int i = 0; i < t.Length; i++)
            //{
            //    for (int j = 0; j < t[i].Count; j++)
            //        Console.Write(t[i][j]);
            //    Console.WriteLine();
            //}
            //Drawing();
        }
        #endregion //Main and all public fields

        static void SortList(LinkedList<int[]> graphic)//doesnt work correctly(mb DSL's fault)
        {
            #region
            for (int i = 0; i < graphic.Count; i++)
            {
                if (!dict1.ContainsKey(i))
                    DepthSearchList(graphic, i);
            }
            for (int j = 0; j < graphic.Count; j++)
            {
                if (!dict1.ContainsKey(j)) blacks1.Push(j);
            }
            #endregion
        }

        static void DepthSearchList(LinkedList<int[]> graphic, int pointnumber)
        {
            #region
            if (graphic.ElementAt(pointnumber).Length != 0)
                for (int i = 0; i < graphic.ElementAt(pointnumber).Length; i++)
                {
                    if (dict1.ContainsKey(graphic.ElementAt(pointnumber)[i]))
                    {
                        if (i == graphic.ElementAt(pointnumber).Length - 1 && !dict1.ContainsKey(pointnumber))
                        {
                            blacks1.Push(pointnumber);
                            dict1[pointnumber] = true;
                        }
                        else
                            continue;
                    }
                    DepthSearchList(graphic, graphic.ElementAt(pointnumber)[i]);
                }
            else if (!dict1.ContainsKey(pointnumber))
            {
                blacks1.Push(pointnumber);
                dict1[pointnumber] = true;
            }
            #endregion
        }

        static void SortTopologically(List<int>[] graphic)
        {
            #region
            for (int i = 0; i < graphic.Length; i++)
            {
                if (!dict.ContainsKey(i))
                    DepthSearch(graphic, i);
            }
            for (int j = 0; j < graphic.Length; j++)
            {
                if (!dict.ContainsKey(j)) blacks.Push(j);
            }
            #endregion
        }

        static void DepthSearch(List<int>[] graphic, int pointnumber)
        {
            depth++;
            #region
            if (graphic[pointnumber] != null)
                for (int i = 0; i < graphic[pointnumber].Count; i++)
                {
                    if (dict.ContainsKey(graphic[pointnumber][i]))
                    {
                        if (i == graphic[pointnumber].Count - 1 && !dict.ContainsKey(pointnumber))
                        {
                            blacks.Push(pointnumber);
                            dict[pointnumber] = true;
                        }
                        else
                            continue;
                    }
                    DepthSearch(graphic, graphic[pointnumber][i]);
                }
            else if (!dict.ContainsKey(pointnumber))
            {
                blacks.Push(pointnumber);
                dict[pointnumber] = true;
            }
            #endregion
        }

        static List<int>[] Generate(int pointCount)
        {
            Dictionary<int, bool> used = new Dictionary<int, bool>();
            string[] b = new string[pointCount];
            for (int u = 0; u<pointCount; u++)
            {
                b[u] = string.Empty;
            }
            var rnd = new Random();
            List<int>[] rndGraph = new List<int>[pointCount];
            for (int i = 0; i<pointCount; i++)
            {
                used.Clear();
                used[i] = true;
                var k = /*rnd.Next(pointCount / 2 + 1) + */1;
                rndGraph[i] = new List<int>();
                //количество ребер из точки i
                for (int j = 0; j < k; j++)
                {
                    var l = rnd.Next(pointCount);
                    while (used.ContainsKey(l))
                    {
                        l = rnd.Next(pointCount);
                    }
                    if (!b[i].Contains(l.ToString()))
                    {
                        rndGraph[i].Add(l);
                        b[l] += i + b[i];
                        if (rndGraph[l] != null)
                            for (int c = 0; c < rndGraph[l].Count; c++)
                                b[rndGraph[l][c]] += b[l];
                        for (int u = 0; u < b[i].Length; u++)
                        {
                            if (!b[l].Contains(b[i][u])) b[l] += b[i][u];
                        }
                    }
                    used[l] = true;
                    //если в какую то вершину пошел, больше не иду
                }
            }

            return rndGraph;
        }

        static void Drawing()//works
        {
            #region
            var chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());
            var raw = new Series();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            SortTopologically(graph);
            SortList(ar);
            stopwatch.Stop();
            for (int i = 0; i < 100; i++)
            {
                raw.Points.Add(new DataPoint(i, stopwatch.ElapsedMilliseconds));
            }
            raw.ChartType = SeriesChartType.FastLine;
            raw.Color = Color.Red;
            chart.Series.Add(raw);
            chart.Dock = DockStyle.Fill;
            var form = new Form();
            form.Controls.Add(chart);
            form.WindowState = FormWindowState.Maximized;
            Application.Run(form);
            #endregion
        }

    }
}
