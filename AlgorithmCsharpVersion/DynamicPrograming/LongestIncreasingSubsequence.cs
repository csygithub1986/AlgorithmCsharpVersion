using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 求序列中最长的递增子序列，时间复杂度为O(n)，空间复杂度O(n)
    /// </summary>
    public class LongestIncreasingSubsequence : ITest
    {
        private int GetLongest(int[] sequence, out int[] subSequence)
        {
            if (sequence.Length == 1)
            {
                subSequence = sequence;
                return 1;
            }

            int[] d = new int[sequence.Length];//表示序列长度为i时，递增子序列的最大长度
            d[0] = 1;

            int[] s = new int[sequence.Length];//记录求解过程中前一个比自己小的数，前序
            s[0] = -1;//-1表示没有前序的值

            subSequence = null;

            int maxValue = 0;

            for (int i = 1; i < sequence.Length; i++)
            {
                d[i] = 1;
                s[i] = -1;
                for (int j = 0; j < i; j++)
                {
                    //当前i的值和之前的值循环比较
                    if (sequence[j] < sequence[i])
                    {
                        if (d[i] < d[j] + 1)
                        {
                            d[i] = d[j] + 1;
                            s[i] = j;
                        }
                    }
                }
                if (i == sequence.Length - 1)
                {
                    //求d中最大的
                    int lastIndex = 0;//序列最后一个的索引
                    for (int ii = 0; ii < d.Length; ii++)
                    {
                        if (d[ii] > maxValue)
                        {
                            maxValue = d[ii];
                            lastIndex = ii;
                        }
                    }
                    //求子序列
                    subSequence = new int[maxValue];
                    int nextIndex = lastIndex;
                    for (int k = subSequence.Length - 1; k >= 0; k--)
                    {
                        subSequence[k] = sequence[nextIndex];
                        nextIndex = s[nextIndex];
                    }
                }
            }
            return maxValue;
        }

        public void AlgorithmTest()
        {
            //循环测试
            int[] totalSequence = { 1, 3, 5, 7, 3, 4, 1, 2, 3, 4, 5 };

            for (int i = 1; i <= totalSequence.Length; i++)
            {
                int[] testSequence = new int[i];
                for (int j = 0; j < i; j++)
                {
                    testSequence[j] = totalSequence[j];
                }

                #region 单个测试
                Console.Write("序列：");
                foreach (var item in testSequence)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();

                int[] subSeq;
                int maxLen = GetLongest(testSequence, out subSeq);
                Console.WriteLine("最长递增子序列：" + maxLen);
                Console.Write("详细信息：");
                foreach (var item in subSeq)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                #endregion

                Console.WriteLine();
            }
        }

        public void BruteForceTest()
        {
        }
    }
}
