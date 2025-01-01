using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public class Trader : RoleAbstract
    {
        public override string Name => "Trader    ";

        public override void Action(Player player, PuertoRico game)
        {//玩家可以不要買貨品嗎? 可以
            game._writer.WriteLine($"\t{Name} Action");
            foreach (Player player1 in game.GetPlayerListFromRole(player))
            {
                //商店只有四格商品的空間。在任何一圈當中，只要商店的空間滿了，任何人在該圈將無法販賣商品
                if (game.Shop.Count >= 4)//商店滿了不能賣
                {
                    game._writer.WriteLine("***商店已滿，無法販售***");
                    continue;
                }
                List<CargoAbstract> tempGoodList = player1.Cargos.OrderBy(x => Utilities.RndNum()).ToList();
                foreach (CargoAbstract good in tempGoodList)
                {//辦公室作用時，當商人出現時，玩家可以不受商店不收不同種類商品的限制，販賣商店內已經有的商品給商店。但玩家仍然只能每次賣一個商品
                    bool ignoreSalesRules = Utilities.CheckBuildingWithWorker(player1, typeof(Office));
                    int playerGoodQTY = player1.Cargos.Find(x => x.Name == good.Name).Qty;
                    int containGoodIndex = game.Shop.FindIndex(x => x.Name == good.Name);
                    game.Shop.FindIndex(x => x.Name == good.Name);
                    if (playerGoodQTY <= 0)//玩家該貨物為零不能賣
                        continue;
                    if (!ignoreSalesRules && containGoodIndex >= 0)//沒有辦公室且重複的商品不能賣
                    {
                        //game._writer.WriteLine($"***沒有辦公室且重複的商品***");
                        continue;
                    }
                    int buildingDiscount = 0;
                    //小市場作用時，只要有賣商品進商店，就可多得一元。可以和大市場一起作用（一起作用時多得三元）。
                    if (Utilities.CheckBuildingWithWorker(player1, typeof(Smallmarket)))
                        buildingDiscount += 1;
                    //大市場作用時，只要有賣商品進商店，就可多得二元。可以和小市場一起作用（一起作用時多得三元）
                    if (Utilities.CheckBuildingWithWorker(player1, typeof(Largemarket)))
                        buildingDiscount += 2;
                    //商人本身將商品賣給商店可多得1元（特權），但商人本身不做買賣時無法獲得這1元。
                    if (player1.Role == "Trader ")
                        buildingDiscount += 1;
                    int goodPrice = 0;
                    //玉米值0元，染料值1元，蔗糖值2元，煙草值3元，咖啡值4元
                    switch (good.Name)
                    {
                        case "Indigo":
                            goodPrice = 1; break;
                        case "Sugar":
                            goodPrice = 2; break;
                        case "Tobacco":
                            goodPrice = 3; break;
                        case "Coffee":
                            goodPrice = 4; break;
                    }
                    int totalPrice = buildingDiscount + goodPrice;
                    if (totalPrice == 0)//商品總賣價是零元就不賣
                    {
                        continue;
                    }
                    //SalesGoods
                    int getMoneyFromBank = game.Bank.GetMoney(totalPrice);
                    player1.AddMoney(getMoneyFromBank);
                    player1.DecreaseCargo(good.Name, 1);
                    game.Shop.Add(good);
                    game._writer.Write($"\t\t{player1.Name} Sales {good.Name} to shop, get {getMoneyFromBank} dollar from bank, ");
                    game.ShowShopGoods();
                    break;
                }
            }
            game.CheckShop();
            game._writer.Flush();
        }
    }
}
