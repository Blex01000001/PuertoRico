using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public static class Utilities
    {
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        public static int RndNum()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next();
        }
        public static int RndNum(int minValue, int maxValue)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next(minValue, maxValue);
        }
        public static bool CheckBuildingWithWorker(Player player, Type buildingType)
        {
            return player.GetAllBuildings().Where(x => x.GetType() == buildingType && x.Worker > 0).ToList().Count > 0 ? true : false;
        }
        public static void shift<T>(T item, List<T> AddToList, List<T> RemoveFromList)
        {
            AddToList.Add(item);
            RemoveFromList.Remove(item);
        }
        public static string GetHexHash(this object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            // 1. 使用 GetHashCode 生成整數哈希值
            int hash = obj.GetHashCode();

            // 2. 對哈希值取模，縮小範圍到 4 位元
            int reducedHash = Math.Abs(hash % 65536); // 65536 = 16^4, 保證結果為 4 位

            // 3. 將結果轉為 16 進制字串
            return reducedHash.ToString("X4"); // "X4" 保證固定長度為 4
        }

        /// <summary>
        /// 以Priority作為機率基準，隨機選取List中的一個物件返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="weights"></param>
        /// <returns>返回List中的一個物件</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static T RandomGetOne<T>(List<T> list)
        {
            if (list == null)
                throw new ArgumentException("Cargos and weights must have the same number of elements.");
            List<int> weights = new List<int>();
            foreach (T item in list)
            {
                item.GetType().GetProperty("Priority").GetValue(item, null);
                weights.Add((int)item.GetType().GetProperty("Priority").GetValue(item, null));
                //weights.Add(item.Priority);
            }
            //Random random = new Random(Guid.NewGuid().GetHashCode());
            int totalWeight = weights.Sum();
            int randomValue = Utilities.RndNum(1, totalWeight + 1);
            int currentSum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                currentSum += weights[i];
                if (randomValue <= currentSum)
                {
                    T selected = list[i];
                    //list.Remove(selected);
                    return selected;
                }
            }
            throw new InvalidOperationException("***未能選取***");
        }
        /// <summary>
        /// 以Priority作為機率基準重新排列list裡的物件，但不是完全依照Priority做排序，高Priority只是比較高機率出現在前面
        /// </summary>
        /// <param name="list"></param>
        /// <returns>返回一個新的list</returns>
        public static List<T> RandomOrderByPriority<T>(List<T> list)
        {

            List<T> newList = list.ToList();
            List<T> Ordered = new List<T>();
            int count = newList.Count;
            for (int i = 0; i < count; i++)
            {
                T temp = RandomGetOne(newList);
                Ordered.Add(temp);
                newList.Remove(temp);
            }
            return Ordered;
        }

    }
}
