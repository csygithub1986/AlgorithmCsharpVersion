/**
 A message containing letters from A-Z is being encoded to numbers using the following mapping:
'A' -> 1
'B' -> 2
...
'Z' -> 26
Given an encoded message containing digits, determine the total number of ways to decode it.

For example,
Given encoded message "12", it could be decoded as "AB" (1 2) or "L" (12).
The number of ways decoding "12" is 2.
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
    /// 各种条件判断，很容易错，主要要分联合和不联合两种分别记录
    /// </summary>
    class DecodeWays : ITest
    {
        public int NumDecodings(string s)
        {
            if (s.Length < 1)
            {
                return 0;
            }
            int[] d = new int[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                d[i] = int.Parse(s[i].ToString());
            }
            if (d[0] == 0)
            {
                return 0;
            }
            int dpSingle = 1;
            int dpUnion = 0;
            for (int i = 1; i < d.Length; i++)
            {
                if (d[i - 1] > 2 || d[i - 1] < 1 || (d[i - 1] == 2 && d[i] > 6))//不能联合
                {
                    if (d[i] == 0)
                    {
                        return 0;
                    }
                    dpSingle = dpSingle + dpUnion;
                    dpUnion = 0;
                }
                else//可以联合
                {
                    if (d[i] == 0)//必须联合
                    {
                        dpUnion = dpSingle;
                        dpSingle = 0;
                    }
                    else
                    {
                        int temp = dpSingle;
                        dpSingle = dpSingle + dpUnion;
                        dpUnion = temp;
                    }
                }
            }
            return dpSingle + dpUnion;
        }


        public void AlgorithmTest()
        {
            string s = "15465213578912";
            int r = NumDecodings(s);
            Console.WriteLine(r);
        }

        public void BruteForceTest()
        {
        }
    }
}
