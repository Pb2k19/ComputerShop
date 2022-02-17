﻿using ComputerShop.Shared.Models.Components;
using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopPcProduct : Product, IDesktopPcProduct
    {
        public DesktopPcProduct()
        {
            Category = new Category
            {
                Id = "1", //tmp
                Icon = "fas fa-desktop",
                Name = "PC",
                Url = "pc"
            };
        }
        public Cpu Cpu { get; set; }
        public Ram Ram { get; set; }
        public Gpu Gpu { get; set; }
        public Psu Psu { get; set; }
        public List<Drive> Drives{ get; set; }
        public int PowerConsumption { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
        public Motherboard Motherboard { get; set; }
        public DesktopCase DesktopCase { get; set; }
        public Cooler Cooler { get; set; }
    }
}
