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
        static public int RndNum()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next();
        }

        static public bool CheckBuildingWithWorker(Player player, Type buildingType)
        {
            return player.GetAllBuildings().Where(x => x.GetType() == buildingType && x.Worker > 0).ToList().Count > 0 ? true : false;
        }
        static public void shift<T>(T item, List<T> AddToList, List<T> RemoveFromList)
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
    }
}
