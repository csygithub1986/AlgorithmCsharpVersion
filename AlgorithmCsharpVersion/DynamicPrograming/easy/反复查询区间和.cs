/**
Given an integer array nums, find the sum of the elements between indices i and j(i ≤ j), inclusive.
Example:
Given nums = [-2, 0, 3, -5, 2, -1]
sumRange(0, 2) -> 1
sumRange(2, 5) -> -1
sumRange(0, 5) -> -3
 */

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 难度★☆☆☆☆
    /// 题目在最上。这道题以前没有见过，原理是典型的空间换时间。
    /// 蛮力解法时间复杂度O(m*n)，空间换时间后，时间复杂度O(m)
    /// </summary>
    class 反复查询区间和 : ITest
    {

        private int[] sum;

        private void InitArray(int[] nums)
        {
            sum = new int[nums.Length + 1];
            for (int i = 0; i < nums.Length; i++)
            {
                sum[i + 1] = sum[i] + nums[i];
            }
        }

        public int Algorithm(int i, int j)
        {
            return sum[j + 1] - sum[i];
        }

        #region 测试

        public 反复查询区间和()
        {
            int[] array = new int[100];
            InitArray(array);
        }
        public void AlgorithmTest()
        {
        }

        public void BruteForceTest()
        {
        }
        #endregion

    }
}
