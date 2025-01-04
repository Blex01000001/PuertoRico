//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public abstract class CargoAbstract
    {
        [JsonInclude]
        public abstract string Name { get; }
        [JsonInclude]
        public int Qty { get; private set; } // 通用的屬性，由基類直接管理
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public StreamWriter _writer;
        protected CargoAbstract()
        {
            Qty = 0; // 初始化商品數量
        }
        public void SetWriter(StreamWriter writer)
        {
            this._writer = writer;
        }
        public void Add(int qty)
        {
            Qty += qty;
        }
        public int Get(int qty)
        {
            if (Qty <= 0)
            {
                _writer.WriteLine($" there are no {GetType().Name} in the bank, Bank {GetType().Name}: {Qty}");
                return 0;
            }
            else if (Qty < qty)
            {
                int temp = Qty;
                Qty = 0;
                _writer.WriteLine($" only {temp} {GetType().Name} in the bank, Bank {GetType().Name}: {Qty}");
                return temp;
            }
            Qty -= qty;
            return qty;
        }
        public int TryGet(int qty)
        {
            if (Qty <= 0)
            {
                _writer.WriteLine($" there are no {GetType().Name} in the bank, Bank {GetType().Name}: {Qty}");
                return 0;
            }
            else if (Qty < qty)
            {
                int temp = Qty;
                Qty = 0;
                _writer.WriteLine($" only {temp} {GetType().Name} in the bank, Bank {GetType().Name}: {Qty}");
                return temp;
            }
            return qty;
        }

    }
    public class Coffee : CargoAbstract
    {
        public override string Name => "Coffee";
        public Coffee() { }
    }
    public class Tobacco : CargoAbstract
    {
        public override string Name => "Tobacco";
        public Tobacco() { }
    }

    public class Corn : CargoAbstract
    {
        public override string Name => "Corn";
        public Corn() { }
    }

    public class Sugar : CargoAbstract
    {
        public override string Name => "Sugar";
        public Sugar() { }
    }

    public class Indigo : CargoAbstract
    {
        public override string Name => "Indigo";
        public Indigo() { }
    }

}
