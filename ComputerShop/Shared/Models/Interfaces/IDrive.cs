﻿namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IDrive
    {
        public string Manufacturer { get; set; }
        public int ReadSpeedMBs { get; set; }
        public int WriteSpeedMBs { get; set; }
    }

    public interface ISsd : IDrive
    {
        public int Tbw { get; set; }
        public string Interface { get; set; }
    }
    public interface IHdd : IDrive
    {
        public int Rpm { get; set; }
    }
}
