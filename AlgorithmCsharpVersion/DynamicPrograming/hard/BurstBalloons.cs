/**
Given n balloons, indexed from 0 to n-1. Each balloon is painted with a number on it represented by array nums. You are asked to burst all the balloons. If the you burst balloon i you will get nums[left] * nums[i] * nums[right] coins. Here left and right are adjacent indices of i. After the burst, the left and right then becomes adjacent.
Find the maximum coins you can collect by bursting the balloons wisely.

Note: 
(1) You may imagine nums[-1] = nums[n] = 1. They are not real therefore you can not burst them.
(2) 0 ≤ n ≤ 500, 0 ≤ nums[i] ≤ 100

Example:
Given [3, 1, 5, 8]
Return 167
nums = [3,1,5,8] --> [3,5,8] -->   [3,8]   -->  [8]  --> []
 coins =  3*1*5      +  3*5*8    +  1*3*8      + 1*8*1   = 167
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 个人觉得比RemoveBoxes稍微简单一点
    /// 算法时间复杂度为O(n^3)，空间复杂度为O(n^2)
    /// </summary>
    class BurstBalloons : ITest
    {
        //dp[i,j]为i到j的最大值，关键点：计算dp[i,j]时，要考虑i-1，j+1的值在内
        private int MaxCoins(int[] nums)
        {
            int n = nums.Length;
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return nums[0];
            }
            int[,] dp = new int[n, n];
            dp[0, 0] = nums[0] * nums[1];
            dp[n - 1, n - 1] = nums[n - 2] * nums[n - 1];
            for (int i = 1; i < n - 1; i++)
            {
                dp[i, i] = nums[i - 1] * nums[i] * nums[i + 1];
            }
            for (int len = 1; len < n; len++)//j-i的长度，最大n-1
            {
                for (int left = 0; left < n - len; left++)//i为起始坐标，len越大，i的最大值越小
                {
                    int right = left + len;
                    for (int last = left; last <= left + len; last++)//最后打的坐标
                    {
                        //在[i,j]中，以k为最后打的最大值
                        int dpFormer = last - 1 < left ? 0 : dp[left, last - 1];
                        int dpLatter = last + 1 > right ? 0 : dp[last + 1, right];
                        int former = left - 1 < 0 ? 1 : nums[left - 1];
                        int latter = right + 1 >= n ? 1 : nums[right + 1];
                        dp[left, right] = Math.Max(dp[left, right], former * nums[last] * latter + dpFormer + dpLatter);
                    }
                }
            }
            return dp[0, n - 1];
        }

        private int MaxCoinsStandard(int[] nums)
        {
            int[] numsL = new int[nums.Length + 2];
            int n = 1;
            foreach (int x in nums)
            {
                if (x > 0)
                {
                    numsL[n++] = x;
                }
            }
            numsL[0] = numsL[n++] = 1;

            int[,] dp = new int[n, n];
            for (int k = 2; k < n; k++)
            {
                for (int left = 0; left < n - k; left++)
                {
                    int right = left + k;
                    for (int i = left + 1; i < right; i++)
                    {
                        dp[left, right] = Math.Max(dp[left, right], numsL[left] * numsL[i] * numsL[right] + dp[left, i] + dp[i, right]);
                    }
                }
            }

            return dp[0, n - 1];
        }


        int[] nums;
        public BurstBalloons()
        {
            int n = 2000;
            nums = new int[n];
            Random ran = new Random();
            for (int i = 0; i < n; i++)
            {
                nums[i] = ran.Next(0, 100);
            }
        }

        public void AlgorithmTest()
        {
            int r = MaxCoins(nums);
            Console.WriteLine(r);
        }

        public void BruteForceTest()
        {
            int r = MaxCoins(nums);
            Console.WriteLine(r);
        }
    }
}
