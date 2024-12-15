using System;
using System.Collections.Generic;
using System.Linq;
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
        public static string ToHexHashCode(this object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            string hash;
            hash = Math.Abs(obj.GetHashCode() / 1024).ToString("X");
            return hash;
        }
    }
}
