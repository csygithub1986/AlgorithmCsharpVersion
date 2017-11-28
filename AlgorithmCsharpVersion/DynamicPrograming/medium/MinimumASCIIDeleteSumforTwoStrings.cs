/**
Given two strings s1, s2, find the lowest ASCII sum of deleted characters to make two strings equal.
Example 1:
Input: s1 = "sea", s2 = "eat"
Output: 231
Explanation: Deleting "s" from "sea" adds the ASCII value of "s" (115) to the sum.
Deleting "t" from "eat" adds 116 to the sum.
At the end, both strings are equal, and 115 + 116 = 231 is the minimum sum possible to achieve this.
Example 2:
Input: s1 = "delete", s2 = "leet"
Output: 403
Explanation: Deleting "dee" from "delete" to turn the string into "let",
adds 100[d]+101[e]+101[e] to the sum.  Deleting "e" from "leet" adds 101[e] to the sum.
At the end, both strings are equal to "let", and the answer is 100+101+101+101 = 403.
If instead we turned both strings into "lee" or "eet", we would get answers of 433 or 417, which are higher.
Note:

s1.length, s2.length 属于(0,100]
All elements of each string will have an ASCII value in [97, 122].
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming.medium
{
    class MinimumASCIIDeleteSumforTwoStrings : ITest
    {
        private int Algorithm(string s1, string s2)
        {
            int n1 = s1.Length;
            int n2 = s2.Length;
            int[] arr1 = new int[n1];
            int[] arr2 = new int[n2];
            for (int i = 0; i < n1; i++)
            {
                arr1[i] = s1.ElementAt(i);
            }
            for (int i = 0; i < n2; i++)
            {
                arr2[i] = s2.ElementAt(i);
            }
            //dp表示当前删除的最小值
            int dp = arr1[0] == arr2[0] ? 0 : arr1[0] + arr2[0];
            for (int i = 1; i < n1; i++)
            {
                for (int j = 1; j < n2; j++)
                {
                    for (int k = 0; k < n1; k++)
                    {

                    }
                }
            }
            return 0;
        }


        public void AlgorithmTest()
        {
        }

        public void BruteForceTest()
        {
        }
    }
}
