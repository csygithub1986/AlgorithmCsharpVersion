/**
Find the contiguous subarray within an array (containing at least one number) which has the largest sum.
For example, given the array [-2,1,-3,4,-1,2,1,-5,4],
the contiguous subarray [4,-1,2,1] has the largest sum = 6.
 */
using System;
using System.Linq;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 难度★★☆☆☆
    /// 题目在最顶端。本题想了一个小时没想出来，比较接近了。看了答案，原来有一个地方思维没绕过去，还是有一点点难度
    /// 蛮力解法时间复杂度为∑(n-i)*i，约等于(n^3)/6，即 O(n^3)，空间复杂度O(1)。
    /// 本算法复杂度为O(n)，空间复杂度O(1)
    /// </summary>
    class 最大连续子序列和 : ITest
    {
        //算法思路：
        private int Algorithm(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                Console.WriteLine("输入非法");
                return 0;
            }
            int max = nums[0];
            int dp = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (dp > 0)
                {
                    dp += nums[i];
                }
                else
                {
                    dp = nums[i];
                }
                max = max > dp ? max : dp;
            }
            return max;
        }

        #region 测试
        int n = 50;
        int[] array;
        public 最大连续子序列和()
        {
            array = new int[n];
            Random ran = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = ran.Next(-20, 20);
            }
            Console.WriteLine("数组个数：" + n);
        }
        public void AlgorithmTest()
        {
            int answer = Algorithm(array);
            Console.WriteLine("答案：" + answer);
        }

        public void BruteForceTest()
        {
        }

        #endregion
    }
}
