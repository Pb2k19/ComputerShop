﻿using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models
{
    public class Cpu : ICpu
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int FrequencyMHz { get; set; }
        public int Tdp { get; set; }
        public int ThreadsCount { get; set; }
        public int CoresCount { get; set; }
        public int L3CacheMB { get; set; }
        public string Architecture { get; set; }
    }
    public enum CpuManufacturers
    {
        Amd,
        Intel,
        Samsung,
        Apple,
        IBM,
        Qualcomm,
        MediaTek,
    }
    public enum CpuArchitecuters
    {
        x86,
        ARM
    }
}
