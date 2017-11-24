using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 平面上有M*N个格子，每个格子中放着一定数量的苹果。你从左上角的格子开始， 每一步只能向下走或是向右走，每次走到一个格子上就把格子里的苹果收集起来， 这样下去，你最多能收集到多少个苹果。
    /// 时间复杂度O(m*n)，空间复杂度O(m*n)
    /// </summary>
    public class CollectApple
    {
        private static int GetMaxApples(int[,] values, int m, int n, out Point[] roads)
        {
            //假定v[i,j]是以i,j格为终点的最大苹果值，推导m-1,n-1的情况  v[i,j]=max(v[i-1,j],v[i,j-1])+values[i,j]
            int[,] v = new int[m, n];
            v[0, 0] = values[0, 0];
            //记录全部路径
            Point[,] r = new Point[m, n];
            //递推
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int lastMax = 0;//上一步的最大值
                    if (i - 1 >= 0)
                    {
                        lastMax = v[i - 1, j];
                        r[i, j] = new Point(i - 1, j);
                    }
                    if (j - 1 >= 0)
                    {
                        if (lastMax < v[i, j - 1])
                        {
                            lastMax = v[i, j - 1];
                            r[i, j] = new Point(i, j - 1);
                        }
                    }
                    v[i, j] = lastMax + values[i, j];
                }
            }
            //反推路径，一共有(m+n-2个路径)
            roads = new Point[m + n - 2];
            roads[roads.Length - 1] = new Point(m - 1, n - 1);
            for (int i = roads.Length - 2; i >= 0; i--)
            {
                roads[i] = r[roads[i + 1].X, roads[i + 1].Y];
            }
            return v[m - 1, n - 1];
        }

        public static void Test()
        {
            #region 随意一个二维数组
            Random ran = new Random();
            int m = ran.Next(4, 7);
            int n = ran.Next(4, 7);
            int[,] values = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    values[i, j] = ran.Next(20);
                }
            }
            values[0, 1] = 0;
            values[0, 2] = 0;
            values[0, 3] = 100;
            #endregion

            Point[] roads;
            int max = GetMaxApples(values, m, n, out roads);

            Console.WriteLine("矩阵为：");
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < m; i++)
                {
                    Console.Write(values[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("最大值：" + max);
            Console.Write("路径：");
            for (int i = 0; i < roads.Length; i++)
            {
                Console.Write(roads[i]);
            }
            Console.WriteLine();
        }


        class Point
        {
            internal int X;
            internal int Y;

            public Point()
            {
                X = 0;
                Y = 0;
            }
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return string.Format(" ({0},{1}) ", X, Y);
            }
        }
    }
}
