﻿using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class ComputerMouseProduct : Product, IComputerMouseProduct
    {
        public ComputerMouseProduct()
        {
            Category = "Mouse";
        }
        public int Weightg {get; set;}
        public bool IsWireless { get; set; }
        public string Sensor { get; set; } = string.Empty;
        public string Interface { get; set; } = string.Empty;
        public string SensorType { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int PollingRateHz { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
        public int MaxDpi { get; set; }
    }
}
