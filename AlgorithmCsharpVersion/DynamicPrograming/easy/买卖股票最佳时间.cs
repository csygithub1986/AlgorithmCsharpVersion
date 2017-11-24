/**
Say you have an array for which the i element is the price of a given stock on day i.
If you were only permitted to complete at most one transaction (ie, buy one and sell one share of the stock), design an algorithm to find the maximum profit.

Example 1:
Input: [7, 1, 5, 3, 6, 4]
Output: 5
max. difference = 6-1 = 5 (not 7-1 = 6, as selling price needs to be larger than buying price)

Example 2:
Input: [7, 6, 4, 3, 1]
Output: 0

In this case, no transaction is done, i.e. max profit = 0.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 难度★☆☆☆☆
    /// 题目在本页顶端，本题为自己思考的，方法借鉴了求最大递增子序列问题。但答案没有很完美，空间维度多了。
    /// 简化题目为：给定数组a[n]，求max(a[j]-a[i])，其中j>i且a[j]>a[i]。如果对于所有j>i，都是a[j]小于等于a[i]，返回0
    /// 蛮力法将所有亮亮相减，时间复杂度为O(n^2)。本算法时间复杂度O(n)，空间复杂度O(1)。下面是算法和蛮力时间对比：
    /// n=1000，1ms，2ms
    /// n=10000，1ms，160ms
    /// n=100000，1ms，15s
    /// n=10^6，9ms，25分钟（估计）
    /// n=10^7，90ms，30小时（估计）
    /// </summary>
    class 买卖股票最佳时间 : ITest
    {
        private int Algorithm(int[] array, out int formerIndex, out int latterIndex)
        {
            //定义最大收益
            int profit = 0;
            //定义i1和i2为最大收益时的前序和后续索引
            int i1 = -1;
            int i2 = -1;
            //定义前i个序列的最小值的索引
            int iMin = 0;
            //动态规划，（看了leetcode上的答案，的确这部分比我的好，已改成他的答案）
            for (int i = 1; i < array.Length; i++)
            {
                //记录最小值
                if (array[i] < array[iMin])
                {
                    iMin = i;
                }
                else if (array[i] > profit + array[iMin])
                {
                    profit = array[i] - array[iMin];
                    i1 = iMin;
                    i2 = i;
                }
            }

            formerIndex = i1;
            latterIndex = i2;
            return profit;
        }

        private int BruteForce(int[] array, out int formerIndex, out int latterIndex)
        {
            int max = 0;
            formerIndex = 0;
            latterIndex = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (max < array[j] - array[i])
                    {
                        max = array[j] - array[i];
                        formerIndex = i;
                        latterIndex = j;
                    }
                }
            }
            return max;
        }

        #region 测试

        int n = 10000;
        int[] prices;

        public 买卖股票最佳时间()
        {
            Random ran = new Random();
            prices = new int[n];
            for (int i = 0; i < n; i++)
            {
                prices[i] = ran.Next(1, 100);
            }
            Console.WriteLine("序列数量：" + n);
            //for (int i = 0; i < n; i++)
            //{
            //    if (i % 10 == 0)
            //    {
            //        Console.Write(")(");
            //    }
            //    Console.Write(prices[i] + "  ");
            //}
            Console.WriteLine();
        }

        public void AlgorithmTest()
        {

            int i1, i2;
            int profit = Algorithm(prices, out i1, out i2);
            Console.WriteLine("最大值：{0}，前序号：{1}，后序号：{2}", profit, i1 + 1, i2 + 1);
        }

        public void BruteForceTest()
        {
            int i1, i2;
            int profit = BruteForce(prices, out i1, out i2);
            Console.WriteLine("最大值：{0}，前序号：{1}，后序号：{2}", profit, i1 + 1, i2 + 1);
        }

        #endregion

    }
}
