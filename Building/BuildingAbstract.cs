using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public abstract class BuildingAbstract
    {
        //[JsonInclude]
        public string Name { get; protected set; }
        public string Industry { get; protected set; }
        public string Type { get; protected set; }
        public string Scale { get; protected set; }
        public int Worker { get; protected set; }
        public int MaxWorker { get; protected set; }
        public int Cost { get; protected set; }
        public int Score { get; protected set; }
        public int Level { get; protected set; }
        public int Priority { get; protected set; }
        protected BuildingAbstract()
        {
            Worker = 0; // 初始工作人數為 0
            Priority = 10;
        }
        public void SetJson
        (
            string name = null,
            string industry = null,
            string type = null,
            string scale = null,
            int worker = 0,
            int maxworker = 0,
            int cost = 0,
            int score = 0,
            int level = 0,
            int priority = 0
        )
        {
            Name = name;
            Industry = industry;
            Type = type;
            Scale = scale;
            Worker = worker;
            MaxWorker = maxworker;
            Cost = cost;
            Score = score;
            Level = level;
            Priority = priority;
        }
        public void SetPriority(int priority) => Priority = priority;
        public void ResetWorker() => Worker = 0;
        public void IncreaseWorker(int qty) => Worker += qty;
    }
    //5種大型特殊建築物（占2個建築物空間）各一
    //12種小型特殊建物（占1個建築物空間）各二
    //20個生產用途建築物

    public class QuarryFarm : BuildingAbstract
    {
        public QuarryFarm()
        {
            Name = "QuarryFarm";
            Industry = "Quarry";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 500;
        }
    }

    public class CoffeeFarm : BuildingAbstract
    {
        public CoffeeFarm()
        {
            Name = "CoffeeFarm";
            Industry = "Coffee";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }
    }

    public class TobaccoFarm : BuildingAbstract
    {
        public TobaccoFarm()
        {
            Name = "TobaccoFarm";
            Industry = "Tobacco";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }
    }

    public class CornFarm : BuildingAbstract
    {
        public CornFarm()
        {
            Name = "CornFarm";
            Industry = "Corn";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }

    }

    public class SugarFarm : BuildingAbstract
    {
        public SugarFarm()
        {
            Name = "SugarFarm";
            Industry = "Sugar";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }

    }

    public class IndigoFarm : BuildingAbstract
    {
        public IndigoFarm()
        {
            Name = "IndigoFarm";
            Industry = "Indigo";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }

    }
    //
    //生廠廠房
    //
    /// <summary>
    /// 小染料廠
    /// </summary>
    public class IndigoPlant_Small : BuildingAbstract
    {
        public IndigoPlant_Small()
        {
            Level = 1;
            Scale = "Small";
            Name = "IndigoPlant(Small)";
            Industry = "Indigo";
            Type = "Building";
            Score = 1;
            Cost = 1;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 大染料廠
    /// </summary>
    public class IndigoPlant_Large : BuildingAbstract
    {
        public IndigoPlant_Large()
        {
            Level = 2;
            Scale = "Large";
            Name = "IndigoPlant(Large)";
            Industry = "Indigo";
            Type = "Building";
            Score = 2;
            Cost = 3;
            MaxWorker = 3;
        }
    }
    /// <summary>
    /// 小蔗糖廠
    /// </summary>
    public class SugarMill_Small : BuildingAbstract
    {
        public SugarMill_Small()
        {
            Level = 1;
            Scale = "Small";
            Name = "SugarMill(Small)";
            Industry = "Sugar";
            Type = "Building";
            Score = 1;
            Cost = 2;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 大蔗糖廠
    /// </summary>
    public class SugarMill_Large : BuildingAbstract
    {
        public SugarMill_Large()
        {
            Level = 2;
            Scale = "Large";
            Name = "SugarMill(Large)";
            Industry = "Sugar";
            Type = "Building";
            Score = 2;
            Cost = 4;
            MaxWorker = 3;
        }
    }
    /// <summary>
    /// 雪茄廠，只有大型
    /// </summary>
    public class TobaccoStorage : BuildingAbstract
    {
        public TobaccoStorage()
        {
            Level = 3;
            Scale = "Large";
            Name = "TobaccoStorage";
            Industry = "Tobacco";
            Type = "Building";
            Score = 3;
            Cost = 5;
            MaxWorker = 3;
        }
    }
    /// <summary>
    /// 咖啡廠，只有大型
    /// </summary>
    public class CoffeeRoaster : BuildingAbstract
    {
        public CoffeeRoaster()
        {
            Level = 3;
            Scale = "Large";
            Name = "CoffeeRoaster";
            Industry = "Coffee";
            Type = "Building";
            Score = 3;
            Cost = 6;
            MaxWorker = 3;
        }
    }
    //
    //小型特殊功能建築
    //
    /// <summary>
    /// 小市場，小市場作用時，只要有賣商品進商店，就可多得一元。可以和大市場一起作用（一起作用時多得三元）。
    /// </summary>
    public class Smallmarket : BuildingAbstract
    {
        public Smallmarket()
        {
            Level = 1;
            Name = "Smallmarket";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 1;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 大市場，大市場作用時，只要有賣商品進商店，就可多得二元。可以和小市場一起作用（一起作用時多得三元）。
    /// </summary>
    public class Largemarket : BuildingAbstract
    {
        public Largemarket()
        {
            Level = 2;
            Name = "Largemarket";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 5;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 農莊，農莊作用時，當開拓者出現時，玩家可以（也可以不要）從背面朝上的農田方塊中抽取一張，放在自己的郊區空格上。然後才從正面朝上的農天方塊中選擇方塊（當自己是開拓者的時候，可以選擇採礦場）。
    /// </summary>
    public class Hacienda : BuildingAbstract
    {
        public Hacienda()
        {
            Level = 1;
            Name = "Hacienda";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 2;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 建築舍，建築舍作用時，當開拓者出現時，玩家猶如開拓者一般地可以選擇拿採礦場（當然要採礦場還有剩的時候）或者普通的農田空格。
    /// </summary>
    public class Constructionhut : BuildingAbstract
    {
        public Constructionhut()
        {
            Level = 1;
            Name = "Constructionhut";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 2;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 小倉庫，小倉庫作用時，當船長出現時，最後除了每個人有一份商品不會腐敗之外，玩家可以選擇一種商品（是一種不是一份），他所持有的該種商品全部不會腐敗。可以和大倉庫一起作用（一起作用時可以選三種商品存儲）。
    /// </summary>
    public class Smallwarehouse : BuildingAbstract
    {
        public Smallwarehouse()
        {
            Level = 1;
            Name = "Smallwarehouse";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 3;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 大倉庫，大倉庫作用時，當船長出現時，最後除了每個人有一份商品不會腐敗之外，玩家可以選擇二種商品（是二種不是二份），他所持有的該二種商品全部不會腐敗。可以和小倉庫一起作用（一起作用時可以選三種）。
    /// </summary>
    public class Largewarehouse : BuildingAbstract
    {
        public Largewarehouse()
        {
            Level = 2;
            Name = "Largewarehouse";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 6;
            MaxWorker = 1;
        }
    }

    /// <summary>
    /// 民宿，民宿作用時，當開拓者出現時，玩家可以在選擇正面朝上的農田方塊（或者採礦場--不論是開拓者本人或者有建築舍）的同時，從銀行獲得一個移民直接作用于該方塊上。倘若銀行沒有移民了，他可以直接從移民船上拿。倘若移民船上也沒了，則他就無法得到移民。
    /// </summary>
    public class Hospice : BuildingAbstract
    {
        public Hospice()
        {
            Level = 2;
            Name = "Hospice";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 4;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 辦公室，辦公室作用時，當商人出現時，玩家可以不受商店不收不同種類商品的限制，販賣商店內已經有的商品給商店。但玩家仍然只能每次賣一個商品。
    /// </summary>
    public class Office : BuildingAbstract
    {
        public Office()
        {
            Level = 2;
            Name = "Office";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 5;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 大工廠，大工廠作用時，當工匠出現時，最後統計收成時，倘若玩家收成的種類有一種，可從銀行得0元；有兩種，可從銀行得1元；三種得2元；四種得3元；五種都有得5元。
    /// </summary>
    public class Factory : BuildingAbstract
    {
        public Factory()
        {
            Level = 3;
            Name = "Factory";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 7;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 大學，大學作用時，當建築師出現時，玩家可以在興建建築的同時，從銀行獲得一個移民直接作用于該建築上。倘若銀行沒有移民了，他可以直接從移民船上拿。倘若移民船上也沒了，則他就無法得到移民。不論建築物容納移民的數量為何，他都只能獲得最多一個移民。
    /// </summary>
    public class University : BuildingAbstract
    {
        public University()
        {
            Level = 3;
            Name = "University";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 8;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 港口，港口作用時，當船長出現時，每次輪到他運物資上船，只要他有物資可以運上船，他就可以多獲得一分。在同一次船長出現時，港口的作用可以進行很多次。可以和碼頭一起作用。
    /// </summary>
    public class Harbor : BuildingAbstract
    {
        public Harbor()
        {
            Level = 3;
            Name = "Harbor";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 8;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 碼頭，碼頭作用時，當船長出現時，輪到該玩家運物資上船時，他可以選擇將物資運到一艘私人的，虛擬的，沒有容量上限的船上，且該虛擬的船運輸的種類可以與船長的三艘船上的種類重複，但該虛擬的船在同一個船長出現的時候仍然只能運送一種物資。該虛擬的船最後一定會開船，也就是船上的物資必定會繳回銀行。可以和港口一起作用。
    /// </summary>
    public class Wharf : BuildingAbstract
    {
        public Wharf()
        {
            Level = 3;
            Name = "Wharf";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 9;
            MaxWorker = 1;
        }
    }
    //
    //大型特殊功能建築
    //
    /// <summary>
    /// 商會，若商會作用，最後計算點數時，大型生產廠房多計兩分，小型生產廠房多計一分。
    /// </summary>
    public class Guildhall : BuildingAbstract
    {
        public Guildhall()
        {
            Level = 4;
            Name = "Guildhall";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 府邸，若居民區作用，最後計算點數時，若郊區空格占滿九格以下，多計四分；占滿十格，多計五分；十一格六分；十二格都占滿多計七分。
    /// </summary>
    public class Residence : BuildingAbstract
    {
        public Residence()
        {
            Level = 4;
            Name = "Residence";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 堡壘，若堡壘作用，最後計算點數時，統計遊戲盤上所有移民總數，無條件舍去每三移民多計一分。
    /// </summary>
    public class Fortress : BuildingAbstract
    {
        public Fortress()//堡壘
        {
            Level = 4;
            Name = "Fortress";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 海關，若海關作用，最後計算點數時，統計（運物資上船得到的）得分方塊總分，無條件舍去每四分多計一分。
    /// </summary>
    public class Customshouse : BuildingAbstract
    {
        public Customshouse()//海關
        {
            Level = 4;
            Name = "Customshouse";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 市政廳，若市政廳作用，最後計算點數時，每座紫色的特殊功能建築（不論大小）多計一分。
    /// </summary>
    public class Cityhall : BuildingAbstract
    {
        public Cityhall()//市政廳
        {
            Level = 4;
            Name = "Cityhall";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
        }
    }
    /// <summary>
    /// 空建築，當作PASS
    /// </summary>
    public class PassBuilding : BuildingAbstract
    {
        public PassBuilding()
        {
            Name = "PassBuilding";
        }
    }

}
