using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop.Shared.Models
{
    internal abstract class Computer : Product
    {
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string Gpu { get; set; }
        public int PowerSupplyWattage { get; set; }
        public List<string> DrivesName { get; set; }
    }
}
