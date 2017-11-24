/**
You are climbing a stair case. It takes n steps to reach to the top.
Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
Note: Given n will be a positive integer.

Example 1:
Input: 2
Output:  2
Explanation:  There are two ways to climb to the top.
1. 1 step + 1 step
2. 2 steps

Example 2:
Input: 3
Output:  3
Explanation:  There are three ways to climb to the top.
1. 1 step + 1 step + 1 step
2. 1 step + 2 steps
3. 2 steps + 1 step
 */
using System;
using System.Linq;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 难度★☆☆☆☆
    /// 题目在最顶端。本题答案为自己思考，但第一次有错，因为下标问题，还是d[n+1]更靠谱。
    /// 蛮力解法时间复杂度为O(m^n)，空间复杂度O(n)。算法复杂度为O(m*n)，空间复杂度O(n)
    /// </summary>
    class 爬楼梯 : ITest
    {
        //算法思路：假设走法s[m]，楼梯n梯，假设前i步一共有d[i]种走法，新加一步后，这一步可以有m种走法（在总步数允许的情况下，你懂的），根据不同走法和以前不同d[i]的步数动态规划（特别像0-1背包问题）
        private int Algorithm(int n, int[] s)
        {
            if (n < 1 || s == null || s.Length < 1)
            {
                return 0;
            }
            //d[i]表示楼梯有i梯时，的走法数量
            int[] d = new int[n + 1];
            d[0] = 1;
            //初始化，小于最小步伐的都只有0种走法。
            int sMin = s.Min();//最小步伐
            for (int i = 1; i < sMin; i++)
            {
                d[i] = 0;
            }
            for (int i = sMin; i <= n; i++)
            {
                d[i] = 0;
                for (int j = 0; j < s.Length; j++)
                {
                    if (i >= s[j])
                    {
                        d[i] += d[i - s[j]];//表示最后一步用s[j]步伐
                    }
                    if (d[i] < 0)
                    {
                        Console.WriteLine("数量太大，溢出了");
                        return -1;
                    }
                }
            }
            return d[n];
        }

        #region 测试
        int n = 50;
        int[] s = { 1, 2 };
        public void AlgorithmTest()
        {
            int count = Algorithm(n, s);
            Console.WriteLine("走法：" + count);
        }

        public void BruteForceTest()
        {
        }

        #endregion
    }
}
