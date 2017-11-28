/**
A sequence of number is called arithmetic if it consists of at least three elements and if the difference between any two consecutive elements is the same.

For example, these are arithmetic sequence:

1, 3, 5, 7, 9
7, 7, 7, 7
3, -1, -5, -9
The following sequence is not arithmetic.

1, 1, 2, 5, 7
A zero-indexed array A consisting of N numbers is given. A slice of that array is any pair of integers (P, Q) such that 0 <= P < Q < N.
A slice (P, Q) of array A is called arithmetic if the sequence:
A[P], A[p + 1], ..., A[Q - 1], A[Q] is arithmetic. In particular, this means that P + 1 < Q.
The function should return the number of arithmetic slices in the array A.

Example:
A = [1, 2, 3, 4]
return: 3, for 3 arithmetic slices in A: [1, 2, 3], [2, 3, 4] and [1, 2, 3, 4] itself.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 
    /// </summary>
    class ArithmeticSlices : ITest
    {
        /// <summary>
        /// 难度★☆☆☆☆ 虽然leetcode分组在medium里，但我觉得比较简单，自己半小时做的
        /// </summary>
        private int Algorithm(int[] A)
        {
            int n = A.Length;//n>=3
            int sum = 0;//之前和
            int count = 2;//当前个数
            int sub = A[1] - A[0];//当前差值
            for (int i = 2; i < n; i++)
            {
                if (A[i] - A[i - 1] == sub)
                {
                    count++;
                }
                else
                {
                    sub = A[i] - A[i - 1];
                    if (count >= 3)
                    {
                        sum += GetValue(count);
                    }
                    count = 2;
                }
            }
            if (count >= 3)
            {
                sum += GetValue(count);
            }

            return sum;
        }

        private int GetValue(int count)
        {
            int s = count - 2;
            return s * (s + 1) / 2;
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
