using ComputerShop.Shared.Models.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class ProductDetails
    {
        [Parameter] public IProduct? Product { get; set; }
    }
}
