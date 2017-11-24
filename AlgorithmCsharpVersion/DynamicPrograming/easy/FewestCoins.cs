using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 1元、3元和5元的硬币若干枚，如何用最少的硬币凑够N元
    /// </summary>
    public class FewestCoins
    {
        private static int[] GetFewestCoins(int value)
        {
            int[] coins = { 1, 3, 5 };
            int[] items = new int[value + 1];
            items[0] = 0;
            for (int i = 1; i <= value; i++)
            {
                items[i] = items[i - 1] + 1;//前提是有value=1的硬币，这里可以直接+1
                for (int j = 0; j < coins.Length; j++)
                {
                    if (i - coins[j] >= 0)
                    {
                        int tempValue = items[i - coins[j]] + 1;
                        if (tempValue < items[i])
                        {
                            items[i] = tempValue;
                        }
                    }
                }
            }
            return items;
        }

        public static void Test()
        {
            int value = 11;
            int[] items = GetFewestCoins(value);
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine(string.Format("Item[{0}]:{1}", i, items[i]));
            }
        }
    }
}
