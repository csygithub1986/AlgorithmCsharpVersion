/**
 You are a professional robber planning to rob houses along a street. 
 Each house has a certain amount of money stashed, the only constraint stopping you from robbing each of them is that 
 adjacent houses have security system connected and it will automatically contact the police 
 if two adjacent houses were broken into on the same night.

 Given a list of non-negative integers representing the amount of money of each house, 
 determine the maximum amount of money you can rob tonight without alerting the police.
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
    /// 题目在最顶端。这道题和0-1背包问题有点类似
    /// 蛮力解法时间复杂度为 O(2^n)。
    /// 本算法复杂度为O(n)，空间复杂度O(1)
    /// </summary>
    class 抢劫房子 : ITest
    {
        //思路，记录每一次最后一个房子打劫或不打劫的最大收益，新增一个房子的时候，也有打劫和不打劫两种方式，根据这几种情况组合讨论最大值
        private int Algorithm(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                Console.WriteLine("输入非法");
                return 0;
            }
            int max0 = 0;//表示前序的最后一个房子不打劫的最大收益
            int max1 = nums[0];//表示前序的最后一个房子要打劫的最大收益
            for (int i = 1; i < nums.Length; i++)
            {
                //要打劫的收益
                int rob = nums[i] + max0;
                //不打劫的收益
                int notRob = max0 > max1 ? max0 : max1;
                max0 = notRob;
                max1 = rob;
            }
            return Math.Max(max0, max1);
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
