﻿using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models
{
    public class Ram : IRam
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int ModulesNumber { get; set; }
        public int FrequencyMHz { get; set; }
        public int LatencyCL { get; set; }
        public string RamTechnology { get; set; }
    }
    public enum RamTechnologies
    {
        DDR,
        DDR2,
        DDR3,
        DDR4,
        DDR5,
    }
    public enum RamManufacturers
    {
        ADATA,
        Asus,
        Corsair,
        HyperX,
        IBM,
        Kingston,
        Lenovo,
        Curcial,
        Mushkin,
        PNY,
        Hynix,
        Toshiba
    }
}