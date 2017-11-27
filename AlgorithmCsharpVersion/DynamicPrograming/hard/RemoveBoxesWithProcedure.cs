/**
Given several boxes with different colors represented by different positive numbers. 
You may experience several rounds to remove boxes until there is no box left. 
Each time you can choose some continuous boxes with the same color (composed of k boxes, k >= 1), remove them and get k*k points.
Find the maximum points you can get.
Example 1:
Input:
[1, 3, 2, 2, 2, 3, 4, 3, 1]
Output:
23
Explanation:
[1, 3, 2, 2, 2, 3, 4, 3, 1] 
----> [1, 3, 3, 4, 3, 1] (3*3=9 points) 
----> [1, 3, 3, 3, 1] (1*1=1 points) 
----> [1, 1] (3*3=9 points) 
----> [] (2*2=4 points)
Note: The number of boxes n would not exceed 100.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    class RemoveBoxesWithProcedure : ITest
    {
        Tuple<List<int[]>, List<int>>[,] procedure;//记录筛选过程。前list表示当前dp[i,j]区域被分成的块，后表示相同的数字的索引。消除时先消除块，再消除相同数字

        private int Algorithm(int[] boxes)
        {
            int n = boxes.Length;
            int[,] dp = new int[n, n];
            procedure = new Tuple<List<int[]>, List<int>>[n, n];

            return GetDp(boxes, 0, n - 1, dp);
        }

        private int GetDp(int[] boxes, int former, int latter, int[,] dp)
        {
            if (former < 0 || latter < 0)
            {
                return 0;
            }
            else if (former == latter)
            {
                procedure[former, latter] = new Tuple<List<int[]>, List<int>>(null, new List<int>(new int[] { former }));
                return 1;
            }
            else if (former > latter)
            {
                return 0;
            }
            else if (dp[former, latter] != 0)
            {
                return dp[former, latter];
            }

            List<int> sameColorList = new List<int>();
            for (int i = former; i < latter; i++)
            {
                if (boxes[i] == boxes[latter])
                {
                    sameColorList.Add(i);
                }
            }
            int maxScore = 1 + GetDp(boxes, former, latter - 1, dp);
            List<int> colorInt = new List<int>();
            colorInt.Add(latter);
            procedure[former, latter] = new Tuple<List<int[]>, List<int>>(new List<int[]>() { new int[] { former, latter - 1 } }, colorInt);

            int tempLastArea = 0;
            int lastm = latter;
            List<int[]> areaDivide = new List<int[]>();
            for (int i = 0; i < sameColorList.Count; i++)
            {
                int m = sameColorList[sameColorList.Count - i - 1];
                colorInt.Add(m);
                int areaCurrent = GetDp(boxes, m + 1, lastm - 1, dp);
                if (areaCurrent != 0)
                {
                    areaDivide.Add(new int[] { m + 1, lastm - 1 });
                }
                int areaMiddle = areaCurrent + tempLastArea;

                int score = (i + 2) * (i + 2) + GetDp(boxes, former, m - 1, dp) + areaMiddle;

                if (maxScore < score)
                {
                    maxScore = score;
                    List<int[]> firstDivideList = new List<int[]>();
                    firstDivideList.AddRange(areaDivide);
                    firstDivideList.Add(new int[] { former, m - 1 });
                    procedure[former, latter] = new Tuple<List<int[]>, List<int>>(firstDivideList, colorInt);
                }
                tempLastArea = tempLastArea + GetDp(boxes, m + 1, lastm - 1, dp);
                lastm = m;
            }
            dp[former, latter] = maxScore;
            return maxScore;
        }



        public void AlgorithmTest()
        {
            //int[] boxes = new int[] { 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2 };
            //int[] boxes = new int[] { 3, 8, 8, 5, 5, 3, 9, 2, 4, 6, 5, 8, 4, 8, 6, 9, 6, 8, 6, 4, 1, 9, 5, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7 };
            //int[] boxes = new int[] { 1, 3, 2, 2, 2, 3, 4, 3, 1 };
            int[] boxes = new int[] { 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 7, 4, 9, 9, 4 };
            //int[] boxes = new int[] { 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 9, 9, 4 };
            int answer = Algorithm(boxes);
            Console.WriteLine("n=" + boxes.Length);
            Console.WriteLine(answer);
            //Console.WriteLine(dpDirectCount + "," + dpCalculateCount + "," + countN);
            //Console.WriteLine(small0 + "," + equal + "," + bigger);
            Console.WriteLine("分解：");
            PrintDetail(0, boxes.Length - 1);


            Console.WriteLine("答案：");
            answer = removeBoxes(boxes);
            Console.WriteLine(answer);

        }

        public void BruteForceTest()
        {
        }


        private void PrintDetail(int i, int j)
        {
            if (procedure[i, j] == null)
            {
                return;
            }
            if (procedure[i, j].Item1 != null)
            {
                foreach (var item in procedure[i, j].Item1)
                {
                    PrintDetail(item[0], item[1]);
                }
            }
            int len = procedure[i, j].Item2.Count;
            for (int k = 0; k < procedure[i, j].Item2.Count; k++)
            {
                Console.Write(procedure[i, j].Item2[len - 1 - k] + "  ");
            }
            Console.WriteLine();
        }


        public int removeBoxes(int[] boxes)
        {
            int n = boxes.Length;
            int[,,] dp = new int[n, n, n];
            return removeBoxesSub(boxes, 0, n - 1, 0, dp);
        }

        private int removeBoxesSub(int[] boxes, int i, int j, int k, int[,,] dp)
        {
            if (i > j) return 0;
            if (dp[i, j, k] > 0) return dp[i, j, k];

            for (; i + 1 <= j && boxes[i + 1] == boxes[i]; i++, k++) ; // optimization: all boxes of the same color counted continuously from the first box should be grouped together
            int res = (k + 1) * (k + 1) + removeBoxesSub(boxes, i + 1, j, 0, dp);

            for (int m = i + 1; m <= j; m++)
            {
                if (boxes[i] == boxes[m])
                {
                    res = Math.Max(res, removeBoxesSub(boxes, i + 1, m - 1, 0, dp) + removeBoxesSub(boxes, m, j, k + 1, dp));
                }
            }

            dp[i, j, k] = res;
            return res;
        }
    }
}
