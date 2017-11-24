/**
Given a non negative integer number num. For every numbers i in the range 0 ≤ i ≤ num calculate the number of 1's in their binary representation and return them as an array.

Example:
For num = 5 you should return [0,1,1,2,1,2].

Follow up:
It is very easy to come up with a solution with run time O(n*sizeof(integer)). But can you do it in linear time O(n) /possibly in a single pass?
Space complexity should be O(n).
Can you do it like a boss? Do it without using any builtin function like __builtin_popcount in c++ or in any other language.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 难度★★☆☆☆
    /// 题目在顶端
    /// </summary>
    class 输出数字的二进制中1的个数 : ITest
    {
        private int[] Algorithm(int num)
        {
            int[] d = new int[num + 1];
            d[0] = 0;
            int powN = 1;//2的n次幂
            int nextPowN = 1;
            for (int i = 1; i <= num; i++)
            {
                if (i == nextPowN)
                {
                    d[i] = 1;
                    powN = nextPowN;
                    nextPowN *= 2;
                }
                else
                {
                    d[i] = 1 + d[i - powN];
                }
            }
            return d;
        }

        //leetcode上最简练的答案：
        private int[] CountBits(int num)
        {
            int[] res = new int[num + 1];
            for (int i = 1; i <= num; i++)
                res[i] = res[i / 2] + (i % 2);
            return res;
        }

        public void AlgorithmTest()
        {
            int[] answer = Algorithm(5);
            Console.Write("答案：(");
            for (int i = 0; i < answer.Length; i++)
            {
                Console.Write(answer[i] + ",");
            }
            Console.WriteLine(")");
        }

        public void BruteForceTest()
        {
        }
    }
}
