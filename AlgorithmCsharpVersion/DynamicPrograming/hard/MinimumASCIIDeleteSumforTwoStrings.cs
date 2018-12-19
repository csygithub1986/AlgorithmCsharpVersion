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

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 做过LCS，这个就好做了。但要注意初始条件
    /// </summary>
    class MinimumASCIIDeleteSumforTwoStrings : ITest
    {
        private int Algorithm(string s1, string s2)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            int[,] dp = new int[len1 + 1, len2 + 1];
            for (int i = 1; i <= len1; i++)
            {
                dp[i, 0] = dp[i - 1, 0] + s1[i - 1];
            }
            for (int j = 1; j <= len2; j++)
            {
                dp[0, j] = dp[0, j - 1] + s2[j - 1];
            }
            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    if (s1[i] == s2[j])
                    {
                        dp[i + 1, j + 1] = dp[i, j];
                    }
                    else
                    {
                        dp[i + 1, j + 1] = Math.Min(dp[i + 1, j] + s2[j], dp[i, j + 1] + s1[i]);
                    }
                }
            }
            return dp[len1, len2];
        }

        public void AlgorithmTest()
        {
            string s1 = "delete";
            string s2 = "leet";
            //string s1 = "sea";
            //string s2 = "eat";
            int r = Algorithm(s1, s2);
            Console.WriteLine(r);
        }

        public void BruteForceTest()
        {
        }
    }
}
