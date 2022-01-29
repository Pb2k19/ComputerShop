using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop.Shared.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Default Name";
        public string? Description { get; set; }        
        public decimal Price { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public bool IsPublic { get; set; } = false;
        public bool IsRemoved { get; set; } = false;
        public DateTime CareationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public List<Image>? Images { get; set; }
        public List<Parameter>? Parameters { get; set; }
    } 
}
