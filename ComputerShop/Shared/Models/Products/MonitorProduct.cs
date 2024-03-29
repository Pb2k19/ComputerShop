﻿using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class MonitorProduct : Product, IMonitorProduct
    {
        public MonitorProduct()
        {
            Category = "Monitor";
        }
        public int PanelSizeInch {get; set;}
        public string AspectRatio { get; set; } = string.Empty;
        public string PanelType { get; set; } = string.Empty;
        public int ResolutionXpx { get; set; }
        public int ResolutionYpx { get; set; }
        public decimal SrgbColorSpacePerc { get; set; }
        public int BrightnessCdm { get; set; }
        public int Contrast { get; set; }
        public int RefreshRateHz { get; set; }
        public int ResponseTimems { get; set; }
        public List<StringValue> Ports { get; set; } = new();
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
    }
}
