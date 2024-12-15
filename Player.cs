using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico
{
    public class Player
    {
        public string Name { get; private set; }
        public int Money { get; private set; }
        public int Worker { get; private set; }
        public int Score { get; private set; }
        public string Role { get; private set; }
        public List<CargoAbstract> Cargos { get; private set; }
        public List<BuildingAbstract> FarmList { get; private set; }
        public List<BuildingAbstract> BuildingList { get; private set; }
        public bool UsedStealthShip { get; private set; }

        public void SetRole(string role)
        {
            Role = role;
        }
        public void RemoveRole()
        {
            Role = null;
        }

        public Player(string name)
        {
            Cargos = new List<CargoAbstract>();
            FarmList = new List<BuildingAbstract>();
            BuildingList = new List<BuildingAbstract>();
            Cargos.Add(new Corn(0));
            Cargos.Add(new Sugar(0));
            Cargos.Add(new Coffee(0));
            Cargos.Add(new Tobacco(0));
            Cargos.Add(new Indigo(0));
            this.Name = name;
        }
        public void IncreaseMoney(int qty)
        {
            Money += qty;
        }
        public int DecreaseMoney(int qty)
        {
            if(Money <= 0)
            {
                return 0;
            }else if(Money < qty)
            {
                int tempMoney = Money;
                Money = 0;
                return tempMoney;
            }
            else
            {
                Money -= qty;
                return qty;
            }
        }
        public List<BuildingAbstract> GetAllBuildings()
        {
            List<BuildingAbstract> BuildingList = new List<BuildingAbstract>();
            BuildingList.AddRange(BuildingList);
            BuildingList.AddRange(FarmList);
            return BuildingList;

        }
    }
}
