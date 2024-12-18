﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
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
        public void UseStealthShip()
        {
            UsedStealthShip = false;
        }
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
            UsedStealthShip = true;
            Cargos.Add(new Corn(0));
            Cargos.Add(new Sugar(0));
            Cargos.Add(new Coffee(0));
            Cargos.Add(new Tobacco(0));
            Cargos.Add(new Indigo(0));
            this.Name = name;
        }
        public void IncreaseScore(int qty)
        {
            Score += qty;
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
            List<BuildingAbstract> allBuildings = new List<BuildingAbstract>();
            allBuildings.AddRange(BuildingList);
            allBuildings.AddRange(FarmList);
            return allBuildings;
        }
        public void IncreaseWorker(int qty)
        {
            Worker += qty;
        }
        public List<BuildingAbstract> GetEmptyCircleList()
        {
            List<BuildingAbstract> PlayerBuildings = new List<BuildingAbstract>();
            //Console.WriteLine($"FarmList.count: {FarmList.Count}");
            foreach (BuildingAbstract farm in FarmList)
            {
                for (int i = 0; i < farm.MaxWorker; i++)
                {
                    PlayerBuildings.Add(farm);
                }
            }

            foreach (BuildingAbstract Building in BuildingList)
            {
                for (int i = 0; i < Building.MaxWorker; i++)
                {
                    PlayerBuildings.Add(Building);
                }
            }
            //Console.WriteLine($"PlayerBuildings.count: {PlayerBuildings.Count}");

            PlayerBuildings = PlayerBuildings.OrderBy(x => Utilities.RndNum()).ToList();
            return PlayerBuildings;
        }
        public void ClearFarmWorker()
        {
            foreach (BuildingAbstract farm in FarmList)
            {
                farm.ResetWorker();
            }
        }
        public void ClearFactoryWorker()
        {
            foreach (BuildingAbstract Building in BuildingList)
            {
                Building.ResetWorker();
            }
        }
        public void IncreaseCargo(string CargoName, int qty)
        {
            Cargos.Find(x => x.Name == CargoName).Add(qty);
        }
        public int DecreaseCargo(string CargoName, int qty)
        {
            return Cargos.Find(x => x.Name == CargoName).Get(qty);
        }

        public int GetFarmWorker(string IndustryType)
        {
            return FarmList.Where(x => x.Industry == IndustryType).Where(x => x.Worker > 0).Sum(x => x.Worker);
        }
        public int GetBuildingWorker(string IndustryType)
        {
            return BuildingList.Where(x => x.Industry == IndustryType).Where(x => x.Worker > 0).Sum(x => x.Worker);
        }
        public void ShowCargo()
        {
            Console.Write($"\t\t{Name} cargos: ");
            foreach (CargoAbstract cargo in Cargos)
            {
                Console.Write($"{cargo.Name}:{cargo.Qty} ");
            }
            Console.Write($"\n");

        }

    }
}
