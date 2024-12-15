using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico
{
    public class Ship
    {
        public string Type { get; private set; }
        public string Cargo { get; private set; }
        public int Quantity { get; private set; }
        public int MaxCargoQuantity { get; private set; }


        public Ship(int maxCargoQuantity, string shipType)
        {
            Type = shipType;
            Cargo = null;
            Quantity = 0;
            MaxCargoQuantity = maxCargoQuantity;
        }
        public void AddQuantity(int qty)
        {
            Quantity += qty;
        }
        public void Reset()
        {
            Cargo = null;
            Quantity = 0;
        }

    }
}
