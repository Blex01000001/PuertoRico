using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico
{
    public class Bank
    {
        public int Worker { get; private set; }
        public int WorkerShip { get; private set; }
        public int Money { get; private set; }
        public int Score { get; private set; }
        public List<CargoAbstract> Cargos { get; private set; }
        public List<BuildingAbstract> QuarryFields  { get; private set; }
        public List<BuildingAbstract> TrushFarms { get; private set; }
        public List<BuildingAbstract> AvailableFarms  { get; internal set; }
        public List<BuildingAbstract> HideFarms  { get; internal set; }
        public List<BuildingAbstract> AvailableBuildings { get; private set; }
        public List<Ship> Ships { get; private set; }

        private void CreateCargos()
        {
            Cargos.Add(new Corn(10));
            Cargos.Add(new Sugar(11));
            Cargos.Add(new Coffee(9));
            Cargos.Add(new Tobacco(9));
            Cargos.Add(new Indigo(11));
        }
        private void CreateWorkerToBank(int playerNum)
        {
            //移民數量：55移民/3人  75移民/4人   95移民/5人
            switch (playerNum)
            {
                case 3:
                    AddWorkerToBank(55);
                    break;
                case 4:
                    AddWorkerToBank(75);
                    break;
                case 5:
                    AddWorkerToBank(95);
                    break;
            }
        }
        private void SetScore(int playerNum)
        {
            switch (playerNum)
            {
                case 3:
                    Score = 75;
                    break;
                case 4:
                    Score = 100;
                    break;
                case 5:
                    Score = 122;
                    break;
            }
        }
        private void CreateFarms()
        {
            for (int i = 0; i < 8; i++)
            {
                BuildingAbstract quarry = new QuarryFarm();
                QuarryFields.Add(quarry);
            }
            for (int i = 0; i < 8; i++)
            {
                BuildingAbstract coffee = new CoffeeFarm();
                HideFarms.Add(coffee);
            }
            for (int i = 0; i < 9; i++)
            {
                BuildingAbstract tobacco = new TobaccoFarm();
                HideFarms.Add(tobacco);
            }
            for (int i = 0; i < 10; i++)
            {
                BuildingAbstract corn = new CornFarm();
                HideFarms.Add(corn);
            }
            for (int i = 0; i < 11; i++)
            {
                BuildingAbstract sugar = new SugarFarm();
                HideFarms.Add(sugar);
            }
            for (int i = 0; i < 12; i++)
            {
                BuildingAbstract indigo = new IndigoFarm();
                HideFarms.Add(indigo);
            }
        }
        private void CreateBuildings()
        {
            //生廠廠房
            for (int i = 0; i < 4; i++)
            {
                IndigoPlant indigoPlant_S = new IndigoPlant(0);
                AvailableBuildings.Add(indigoPlant_S);
            }
            for (int i = 0; i < 3; i++)
            {
                IndigoPlant indigoPlant_B = new IndigoPlant(1);
                AvailableBuildings.Add(indigoPlant_B);
            }
            for (int i = 0; i < 4; i++)
            {
                SugarMill sugarMill_S = new SugarMill(0);
                AvailableBuildings.Add(sugarMill_S);
            }
            for (int i = 0; i < 3; i++)
            {
                SugarMill sugarMill_B = new SugarMill(1);
                AvailableBuildings.Add(sugarMill_B);
            }
            for (int i = 0; i < 3; i++)
            {
                TobaccoStorage tobaccoStorage = new TobaccoStorage();
                AvailableBuildings.Add(tobaccoStorage);
            }
            for (int i = 0; i < 3; i++)
            {
                CoffeeRoaster coffeeRoaster = new CoffeeRoaster();
                AvailableBuildings.Add(coffeeRoaster);
            }
            //小型特殊功能建築
            for (int i = 0; i < 2; i++)
            {
                Smallmarket smallmarket = new Smallmarket();
                AvailableBuildings.Add(smallmarket);
            }
            for (int i = 0; i < 2; i++)
            {
                Largemarket largemarket = new Largemarket();
                AvailableBuildings.Add(largemarket);
            }
            for (int i = 0; i < 2; i++)
            {
                Hacienda hacienda = new Hacienda();
                AvailableBuildings.Add(hacienda);
            }
            for (int i = 0; i < 2; i++)
            {
                Constructionhut constructionhut = new Constructionhut();
                AvailableBuildings.Add(constructionhut);
            }
            for (int i = 0; i < 2; i++)
            {
                Smallwarehouse smallwarehouse = new Smallwarehouse();
                AvailableBuildings.Add(smallwarehouse);
            }
            for (int i = 0; i < 2; i++)
            {
                Largewarehouse largewarehouse = new Largewarehouse();
                AvailableBuildings.Add(largewarehouse);
            }
            for (int i = 0; i < 2; i++)
            {
                Hospice hospice = new Hospice();
                AvailableBuildings.Add(hospice);
            }
            for (int i = 0; i < 2; i++)
            {
                Office office = new Office();
                AvailableBuildings.Add(office);
            }
            for (int i = 0; i < 2; i++)
            {
                Factory factory = new Factory();
                AvailableBuildings.Add(factory);
            }
            for (int i = 0; i < 2; i++)
            {
                University university = new University();
                AvailableBuildings.Add(university);
            }
            for (int i = 0; i < 2; i++)
            {
                Harbor harbor = new Harbor();
                AvailableBuildings.Add(harbor);
            }
            for (int i = 0; i < 2; i++)
            {
                Wharf wharf = new Wharf();
                AvailableBuildings.Add(wharf);
            }
            //大型特殊功能建築
            Guildhall guildhall = new Guildhall();
            AvailableBuildings.Add(guildhall);
            Residence residence = new Residence();
            AvailableBuildings.Add(residence);
            Fortress fortress = new Fortress();
            AvailableBuildings.Add(fortress);
            Customshouse customshouse = new Customshouse();
            AvailableBuildings.Add(customshouse);
            Cityhall cityhall = new Cityhall();
            AvailableBuildings.Add(cityhall);
            //空的建築物，當作PASS
            PassBuilding passBuilding = new PassBuilding();
            AvailableBuildings.Add(passBuilding);

            AvailableBuildings = AvailableBuildings.OrderBy(x => Utilities.RndNum()).ToList();
        }
        private void CreateShip(int playerNum)
        {
            Ships = new List<Ship>();
            if (playerNum == 3)
            {
                Ships.Add(new Ship(4, "Normal"));
                Ships.Add(new Ship(5, "Normal"));
                Ships.Add(new Ship(6, "Normal"));
            }
            else if (playerNum == 4)
            {
                Ships.Add(new Ship(5, "Normal"));
                Ships.Add(new Ship(6, "Normal"));
                Ships.Add(new Ship(7, "Normal"));
            }
            else
            {
                Ships.Add(new Ship(6, "Normal"));
                Ships.Add(new Ship(7, "Normal"));
                Ships.Add(new Ship(8, "Normal"));
            }

        }
        public void SetUp(int playerNum)
        {
            Cargos = new List<CargoAbstract>();
            QuarryFields = new List<BuildingAbstract>();
            TrushFarms = new List<BuildingAbstract>();
            AvailableFarms = new List<BuildingAbstract>();
            HideFarms = new List<BuildingAbstract>();
            AvailableBuildings = new List<BuildingAbstract>();

            CreateCargos();
            CreateWorkerToBank(playerNum);
            WorkerShip = playerNum;
            Money = 86;
            SetScore(playerNum);
            CreateFarms();
            CreateBuildings();
            CreateShip(playerNum);



        }
        public void AddCargo(Type cargo, int qty)
        {
            Cargos.Find(x => x.GetType() == cargo).Add(qty);
        }
        public int GetCargo(Type cargo, int qty)
        {
            return Cargos.Find(x => x.GetType() == cargo).Get(qty);
        }
        public void AddWorkerToBank(int qty)
        {
            Worker += qty;
        }
        public int GetWorkerFromBank(int request)
        {
            if(Worker < request)
            {
                int tempWorker = Worker;
                Worker -= request;
                return tempWorker;
            }
            Worker -= request;
            return request;
        }
        public void AddWorkerToWorkerShip(int qty)
        {
            WorkerShip += qty;
        }
        public int GetWorkerFromWorkerShip(int request)
        {
            if(WorkerShip <= 0)
            {
                Console.WriteLine($"NO Worker on WorkerShip");
                return 0;
            }
            else if (WorkerShip < request)
            {
                Console.WriteLine($"not enough Worker on WorkerShip");
                int tempWorker = WorkerShip;
                WorkerShip -= request;
                return tempWorker;
            }
            WorkerShip -= request;
            return request;
        }
        public void ResetWorkerShip(int workerNum)
        {
            WorkerShip = workerNum;
        }
        public void AddMoney(int qty)
        {
            Money += qty;
        }
        public int GetMoney(int request)
        {
            if (Money <= 0)
            {
                Console.WriteLine($"Bank NO MONEY, Bank: {Money}");
                return 0;
            }
            else if (Money < request)
            {
                int temp = Money;
                Money = 0;
                Console.WriteLine($"Bank not enough, Bank: {Money}");
                return temp;
            }
            Money -= request;
            return request;
        }
        public void RndAvailableBuildings()
        {
            AvailableBuildings = AvailableBuildings.OrderBy(x => Utilities.RndNum()).ToList();
        }
        public void CheckCargoShip()
        {
            foreach (Ship ship in Ships)
            {
                if (ship.Quantity >= ship.MaxCargoQuantity)
                {
                    ship.Reset();
                    Console.WriteLine($"***Ship({ship.GetHashCode()}) has been Reset***");
                }
            }
            Console.WriteLine("");
        }
        public int GetScore(int score)
        {
            Score -= score;
            return score;
        }
    }
}
