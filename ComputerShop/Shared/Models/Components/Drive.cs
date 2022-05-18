﻿using ComputerShop.Shared.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace ComputerShop.Shared.Models.Components
{
    [BsonKnownTypes(typeof(Ssd), typeof(Hdd))]
    public class Drive : IDrive
    {
        public string Manufacturer { get; set; }
        public int ReadSpeedMBs { get; set; }
        public int WriteSpeedMBs { get; set; }
        public string Type { get; set; }
        public int CapacityGB { get; set; }
        public string Interface { get; set; }
    }
    public enum DriveManufacturers
    {
        Wd,
        Toshiba,
        Seagate,
        Kingston,
        Adata,
        Sandisk,
        Transcend,
        Lexar,
    }
    public enum DrivesTypes
    {
        HDD,
        SSD
    }
}
