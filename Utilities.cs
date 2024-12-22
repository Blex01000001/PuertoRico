using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public static class Utilities
    {
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
        public static RoleAbstract RandomGetOne(List<RoleAbstract> list)
        {
            if (list == null)
                throw new ArgumentException("Cargos and weights must have the same number of elements.");
            List<int> weights = new List<int>();
            foreach (RoleAbstract item in list)
            {
                weights.Add(item.Priority);
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
                    RoleAbstract selected = list[i];
                    //list.Remove(selected);
                    return selected;
                }
            }
            throw new InvalidOperationException("***未能選取貨物***");
        }
        /// <summary>
        /// 以Priority作為機率基準重新排列list裡的物件，但不是完全依照Priority做排序，高Priority只是比較高機率出現在前面
        /// </summary>
        /// <param name="list"></param>
        /// <returns>返回一個新的list</returns>
        public static List<RoleAbstract> RandomOrderByPriority(List<RoleAbstract> list)
        {

            List<RoleAbstract> newList = list.ToList();
            List<RoleAbstract> Ordered = new List<RoleAbstract>();
            //Console.WriteLine($"before Ori List Count: {list.Count}");
            //Console.WriteLine($"before newList Count: {newList.Count}");
            //Console.WriteLine($"before Ordered Count: {Ordered.Count}");
            int count = newList.Count;
            for (int i = 0; i < count; i++)
            {
                RoleAbstract temp = RandomGetOne(newList);
                Ordered.Add(temp);
                newList.Remove(temp);
            }
            //Console.WriteLine($"after Ori List Count: {list.Count}");
            //Console.WriteLine($"after newList Count: {newList.Count}");
            //Console.WriteLine($"after Ordered Count: {Ordered.Count}");
            return Ordered;
        }

    }
}
