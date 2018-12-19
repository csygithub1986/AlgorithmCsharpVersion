/**
Given two words word1 and word2, find the minimum number of steps required to convert word1 to word2. (each operation is counted as 1 step.)

You have the following 3 operations permitted on a word:

a) Insert a character
b) Delete a character
c) Replace a character
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// based on LCS
    /// </summary>
    class EditDistance : ITest
    {
        public int MinDistance(string word1, string word2)
        {
            int n1 = word1.Length;
            int n2 = word2.Length;
            int[,] dp = new int[n1 + 1, n2 + 1];//dp[i,j]表示i长度的转到j长度的词
            for (int i = 0; i <= n1; i++)
            {
                dp[i, 0] = i;
            }
            for (int i = 0; i <= n2; i++)
            {
                dp[0, i] = i;
            }
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    if (word1[i] == word2[j])
                    {
                        dp[i + 1, j + 1] = dp[i, j];
                    }
                    else
                    {
                        dp[i + 1, j + 1] = Math.Min(Math.Min(dp[i, j] + 1, dp[i + 1, j] + 1), dp[i, j + 1] + 1);//分别为：换、加、删
                    }
                }
            }
            return dp[n1, n2];
        }


        public void AlgorithmTest()
        {
            string a = "sea";
            string b = "eat";
            int r = MinDistance(a, b);
            Console.WriteLine(r);
        }

        public void BruteForceTest()
        {
        }
    }
}
