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

    class 买卖股票最佳时间 : ITest
    {
        private int Algorithm(int[] prices, out int buyTime, out int sellTime)
        {
            buyTime = 0;
            sellTime = 0;
            return 0;
        }

        private void BruteForce()
        {

        }

        #region 测试
        public void AlgorithmTest()
        {
        }

        public void BruteForceTest()
        {
        }

        #endregion

    }
}
