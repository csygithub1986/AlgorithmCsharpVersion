/**
 Given an array of 2n integers, your task is to group these integers into n pairs of integer, say (a1, b1), (a2, b2), ..., (an, bn) which makes sum of min(ai, bi) for all i from 1 to n as large as possible.

Example 1:
Input: [1,4,3,2]

Output: 4
Explanation: n is 2, and the maximum sum of pairs is 4 = min(1, 2) + min(3, 4).
Note:
1、n is a positive integer, which is in the range of [1, 10000].
2、All the integers in the array will be in the range of [-10000, 10000].
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.Array
{
    /// <summary>
    /// 我这个算法比网上所有的算法都快，运用了“计数排序”
    /// </summary>
    class ArrayPartitionI : ITest
    {
        public int ArrayPairSum(int[] nums)
        {
            int min = nums.Min();
            int max = nums.Max();

            int moveNum = -min;
            int[] translater = new int[max - min + 1];
            for (int i = 0; i < nums.Length; i++)
            {
                translater[nums[i] + moveNum]++;
            }
            int sum = 0;
            bool needAdd = true;
            for (int i = 0; i < translater.Length; i++)
            {
                while (translater[i] > 0)
                {
                    if (needAdd)
                    {
                        sum += i - moveNum;
                    }
                    translater[i]--;
                    needAdd = !needAdd;
                }
            }
            return sum;
        }

        public void AlgorithmTest()
        {
            throw new NotImplementedException();
        }

        public void BruteForceTest()
        {
            throw new NotImplementedException();
        }
    }
}
