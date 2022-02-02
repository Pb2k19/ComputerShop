using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop.Shared.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Location { get; set; } = "images/default_image.png";
        public string Name { get; set; } = "default_image";        
    }
}
