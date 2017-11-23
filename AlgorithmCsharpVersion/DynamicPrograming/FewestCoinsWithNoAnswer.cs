using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 任意的硬币若干枚，如何用最少的硬币凑够N元，如果不能凑出来，请注明无解
    /// m种硬币，n元，时间复杂度O(m*n)，空间复杂度O(m+n)
    /// </summary>
    public class FewestCoinsWithNoAnswer
    {
        private static int[] GetFewestCoins(int[] coins, int value)
        {
            int[] items = new int[value + 1];
            items[0] = 0;
            for (int i = 1; i <= value; i++)
            {
                items[i] = -1;//-1代表无解
                int minValue = int.MaxValue;
                for (int j = 0; j < coins.Length; j++)
                {
                    if (i - coins[j] >= 0)
                    {
                        if (items[i - coins[j]] != -1)
                        {
                            int tempValue = items[i - coins[j]] + 1;
                            if (tempValue < minValue)
                            {
                                minValue = tempValue;
                            }
                        }
                    }
                }
                if (minValue != int.MaxValue)
                {
                    items[i] = minValue;
                }
            }
            return items;
        }

        public static void Test()
        {
            int value = 30;
            int[] coins = { 4, 5, 9 };

            int[] items = GetFewestCoins(coins, value);
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine(string.Format("Item[{0}]:{1}", i, items[i] == -1 ? "无解" : ("" + items[i])));
            }
        }
    }
}
