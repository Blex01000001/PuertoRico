using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public class BuildingAbstract
    {
        public string Name { get; protected set; }
        public string Industry { get; protected set; }
        public string Type { get; protected set; }
        public int Worker { get; protected set; }
        public int MaxWorker { get; protected set; }
        public int Cost { get; protected set; }
        public int Score { get; protected set; }
        public int Level { get; protected set; }
        protected BuildingAbstract()
        {
            Worker = 0; // 初始工作人數為 0
        }
        public void ResetWorker()
        {
            Worker = 0;
        }
        public void IncreaseWorker(int qty)
        {
            Worker += qty;
        }
    }
    //5種大型特殊建築物（占2個建築物空間）各一
    //12種小型特殊建物（占1個建築物空間）各二
    //20個生產用途建築物

    public class QuarryFarm : BuildingAbstract
    {
        public QuarryFarm() : base()
        {
            Name = "Quarry  ";
            Industry = "Quarry";
            Type = "Farm";
            MaxWorker = 1;

        }
    }

    public class CoffeeFarm : BuildingAbstract
    {
        public CoffeeFarm()
        {
            Name = "Coffee  ";
            Industry = "Coffee";
            Type = "Farm";
            MaxWorker = 1;

        }
    }

    public class TobaccoFarm : BuildingAbstract
    {
        public TobaccoFarm()
        {
            Name = "Tobacco ";
            Industry = "Tobacco";
            Type = "Farm";
            MaxWorker = 1;

        }
    }

    public class CornFarm : BuildingAbstract
    {
        public CornFarm()
        {
            Name = "Corn    ";
            Industry = "Corn";
            Type = "Farm";
            MaxWorker = 1;
        }

    }

    public class SugarFarm : BuildingAbstract
    {
        public SugarFarm()
        {
            Name = "Sugar   ";
            Industry = "Sugar";
            Type = "Farm";
            MaxWorker = 1;
        }

    }

    public class IndigoFarm : BuildingAbstract
    {
        public IndigoFarm()
        {
            Name = "Indigo  ";
            Industry = "Indigo";
            Type = "Farm";
            MaxWorker = 1;
        }

    }
    //
    //生廠廠房
    //
    internal class IndigoPlant : BuildingAbstract
    {
        public IndigoPlant(int smallLarge)
        {
            if (smallLarge == 0)
            {
                Level = 1;
                Name = "IndigoPlant_Small";
                Industry = "Indigo";
                Type = "Building";
                Cost = 1;
                MaxWorker = 1;
                Score = 1;
            }
            else if (smallLarge == 1)
            {
                Level = 2;
                Name = "IndigoPlant_Big";
                Industry = "Indigo";
                Type = "Building";
                Cost = 3;
                MaxWorker = 3;
                Score = 2;
            }
        }
    }
    internal class SugarMill : BuildingAbstract
    {
        public SugarMill(int smallLarge)
        {
            if (smallLarge == 0)
            {
                Level = 1;
                Name = "SugarMill_Small";
                Industry = "Sugar";
                Type = "Building";
                Cost = 2;
                MaxWorker = 1;
                Score = 1;

            }
            else if (smallLarge == 1)
            {
                Level = 2;
                Name = "SugarMill_Big";
                Industry = "Sugar";
                Type = "Building";
                Cost = 4;
                MaxWorker = 3;
                Score = 2;
            }
        }
    }
    internal class TobaccoStorage : BuildingAbstract
    {
        public TobaccoStorage()
        {
            Level = 3;
            Name = "TobaccoStorage";
            Industry = "Tobacco";
            Type = "Building";
            Cost = 5;
            MaxWorker = 3;
            Score = 3;
        }
    }
    internal class CoffeeRoaster : BuildingAbstract
    {
        public CoffeeRoaster()
        {
            Level = 3;
            Name = "CoffeeRoaster";
            Industry = "Coffee";
            Type = "Building";
            Cost = 6;
            MaxWorker = 2;
            Score = 3;
        }
    }
    //
    //小型特殊功能建築
    //
    internal class Smallmarket : BuildingAbstract
    {
        public Smallmarket()
        {
            Level = 1;
            Name = "Smallmarket";
            Industry = "";
            Type = "Building";
            Cost = 1;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Largemarket : BuildingAbstract
    {
        public Largemarket()
        {
            Level = 2;
            Name = "Largemarket";
            Industry = "";
            Type = "Building";
            Cost = 5;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Hacienda : BuildingAbstract
    {
        public Hacienda()
        {
            Level = 1;
            Name = "Hacienda";
            Industry = "";
            Type = "Building";
            Cost = 2;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Constructionhut : BuildingAbstract
    {
        public Constructionhut()
        {
            Level = 1;
            Name = "Constructionhut";
            Industry = "";
            Type = "Building";
            Cost = 2;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Smallwarehouse : BuildingAbstract
    {
        public Smallwarehouse()
        {
            Level = 1;
            Name = "Smallwarehouse";
            Industry = "";
            Type = "Building";
            Cost = 3;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Largewarehouse : BuildingAbstract
    {
        public Largewarehouse()
        {
            Level = 2;
            Name = "Largewarehouse";
            Industry = "";
            Type = "Building";
            Cost = 6;
            MaxWorker = 1;
            //Score = 0;
        }
    }

    internal class Hospice : BuildingAbstract
    {
        public Hospice()
        {
            Level = 2;
            Name = "Hospice";
            Industry = "";
            Type = "Building";
            Cost = 4;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Office : BuildingAbstract
    {
        public Office()
        {
            Level = 2;
            Name = "Office";
            Industry = "";
            Type = "Building";
            Cost = 5;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Factory : BuildingAbstract
    {
        public Factory()
        {
            Level = 3;
            Name = "Factory";
            Industry = "";
            Type = "Building";
            Cost = 7;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class University : BuildingAbstract
    {
        public University()
        {
            Level = 3;
            Name = "University";
            Industry = "";
            Type = "Building";
            Cost = 8;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Harbor : BuildingAbstract
    {
        public Harbor()
        {
            Level = 3;
            Name = "Harbor";
            Industry = "";
            Type = "Building";
            Cost = 8;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Wharf : BuildingAbstract
    {
        public Wharf()
        {
            Level = 3;
            Name = "Wharf";
            Industry = "";
            Type = "Building";
            Cost = 9;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    //
    //大型特殊功能建築
    //
    internal class Guildhall : BuildingAbstract
    {
        public Guildhall()
        {
            Level = 4;
            Name = "Guildhall";
            Industry = "";
            Type = "Building";
            Cost = 10;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Residence : BuildingAbstract
    {
        public Residence()
        {
            Level = 4;
            Name = "Residence";
            Industry = "";
            Type = "Building";
            Cost = 10;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Fortress : BuildingAbstract
    {
        public Fortress()//要塞
        {
            Level = 4;
            Name = "Fortress";
            Industry = "";
            Type = "Building";
            Cost = 10;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Customshouse : BuildingAbstract
    {
        public Customshouse()//海關
        {
            Level = 4;
            Name = "Customshouse";
            Industry = "";
            Type = "Building";
            Cost = 10;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Cityhall : BuildingAbstract
    {
        public Cityhall()//市政廳
        {
            Level = 4;
            Name = "Cityhall";
            Industry = "";
            Type = "Building";
            Cost = 10;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    //
    //空建築，當作PASS
    //
    internal class PassBuilding : BuildingAbstract
    {
        public PassBuilding()
        {
            Name = "PassBuilding";
        }
    }

}
