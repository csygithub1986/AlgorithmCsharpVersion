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
    /// <summary>
    /// 算法时间复杂度为O(n^4)，空间复杂度为O(n^3)，拟合结果y = 0.0051x4 - 0.9224x3 + 249.05x2 - 23132x + 469303
    /// </summary>
    class RemoveBoxes : ITest
    {
        long circulateCount = 0;
        private int Algorithm(int[] boxes)
        {
            circulateCount = 0;
            int n = boxes.Length;
            int[,,] dp = new int[n, n, n];
            return AlgorithmSub(boxes, 0, n - 1, 0, dp);
        }

        private int AlgorithmSub(int[] boxes, int former, int latter, int repetition, int[,,] dp)
        {
            int originLatter = latter;
            for (; former < latter && boxes[latter] == boxes[latter - 1]; repetition++, latter--) ;
            int group = 0;
            if (latter - 1 >= 0)
            {
                if (dp[former, latter - 1, 0] > 0)
                {
                    group = dp[former, latter - 1, 0];
                }
                else if (former == latter - 1)
                {
                    group = 1;
                }
                else if (former < latter - 1)
                {
                    group = AlgorithmSub(boxes, former, latter - 1, 0, dp);
                }
            }

            int maxScore = (repetition + 1) * (repetition + 1) + group;
            for (int i = former; i < latter; i++)
            {
                circulateCount++;          //累计遍历次数
                if (boxes[i] == boxes[latter])
                {
                    int groupFormer = 0;
                    if (dp[former, i, repetition + 1] > 0)
                    {
                        groupFormer = dp[former, i, repetition + 1];
                    }
                    else if (former == i)
                    {
                        groupFormer = (repetition + 2) * (repetition + 2);
                    }
                    else if (former < i)
                    {
                        groupFormer = AlgorithmSub(boxes, former, i, repetition + 1, dp);
                    }
                    int groupLatter = 0;
                    if (dp[i + 1, latter - 1, 0] > 0)
                    {
                        groupLatter = dp[i + 1, latter - 1, 0];
                    }
                    else if (i + 1 == latter - 1)
                    {
                        groupLatter = 1;
                    }
                    else if (i + 1 < latter - 1)
                    {
                        groupLatter = AlgorithmSub(boxes, i + 1, latter - 1, 0, dp);
                    }

                    maxScore = Math.Max(maxScore, groupFormer + groupLatter);
                }
            }
            dp[former, latter, repetition] = maxScore;
            for (int i = latter + 1; i <= originLatter; i++)
            {
                dp[former, i, repetition + latter - i] = maxScore;
            }
            return maxScore;
        }

        public void AlgorithmTest()
        {
            //int[] boxes = new int[] { 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7, 3, 8, 8, 5, 5, 3, 9, 2 };
            //int[] boxes = new int[] { 3, 8, 8, 5, 5, 3, 9, 2, 4, 6, 5, 8, 4, 8, 6, 9, 6, 8, 6, 4, 1, 9, 5, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7 };
            //int[] boxes = new int[] { 1, 3, 2, 2, 2, 3, 4, 3, 1 };
            //int[] boxes = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 8, 6, 4, 1, 9, 5, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4 };
            Random ran = new Random();
            for (int i = 1; i < 2; i++)
            {
                int[] boxes = new int[i];
                for (int j = 0; j < i; j++)
                {
                    boxes[j] = ran.Next(1, 20);
                }
                int answer = Algorithm(boxes);
                Console.WriteLine(i + "\t" + circulateCount);
            }
        }

        public void BruteForceTest()
        {
        }

    }
}
