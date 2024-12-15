using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public abstract class CargoAbstract
    {
        public abstract string Name { get; }
        public int Qty { get; private set; } // 通用的屬性，由基類直接管理
        protected CargoAbstract(int qty)
        {
            Qty = qty; // 初始化商品數量
        }
        public void Add(int qty)
        {
            Qty += qty;
        }
        public int Get(int qty)
        {
            if (Qty <= 0)
            {
                Console.WriteLine($" there are no {GetType().Name} in the bank, Bank {GetType().Name}: {Qty}");
                return 0;
            }
            else if (Qty < qty)
            {
                int temp = Qty;
                Qty = 0;
                Console.WriteLine($" only {temp} {GetType().Name} in the bank, Bank {GetType().Name}: {Qty}");
                return temp;
            }
            Qty -= qty;
            return qty;
        }
    }

    public class Coffee : CargoAbstract
    {
        public override string Name => "Coffee";
        public Coffee(int qty) : base(qty) { }
    }
    public class Tobacco : CargoAbstract
    {
        public override string Name => "Tobacco";
        public Tobacco(int qty) : base(qty) { }
    }

    public class Corn : CargoAbstract
    {
        public override string Name => "Corn";
        public Corn(int qty) : base(qty) { }
    }

    public class Sugar : CargoAbstract
    {
        public override string Name => "Sugar";
        public Sugar(int qty) : base(qty) { }
    }

    public class Indigo : CargoAbstract
    {
        public override string Name => "Indigo";
        public Indigo(int qty) : base(qty) { }
    }

}
