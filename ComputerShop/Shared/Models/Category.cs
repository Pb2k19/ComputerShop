using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop.Shared.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Uncategorized";
        public string? Icon { get; set; }
        public string Url { get; set; } = "/uncategorized";
        public List<Product>? Products { get; set; }

    }
}
