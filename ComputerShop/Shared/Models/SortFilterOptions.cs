namespace ComputerShop.Shared.Models
{
    public class ProductSortFilterOptions
    {
        int priceMin = 0, priceMax = 99999;

        public int SortOption { get; set; }
        public int PriceMin { 
            get => priceMin; 
            set
            {
                if(value >= 0 && value < int.MaxValue && value < priceMax)
                    priceMin = value;
            }
        }
        public int PriceMax
        {
            get => priceMax; 
            set
            {
                if (value >= 0 && value < int.MaxValue && value > priceMin)
                    priceMax = value;
            }
        }
        public List<string> Manufacturers { get; set; } = new();
        public bool ShowAvaliableOnly { get; set; } = false;
        public bool ShowDiscountOnly { get; set; } = false;
    }

    public class SortOption
    {
        public string DisplayName { get; set; }
        public int Option { get; set; }
    }

    public static class SortOptions
    {
        public readonly static SortOption OrderByPriceDescending = new() { DisplayName = "Cena: najtańsze", Option = 0 };
        public readonly static SortOption OrderByPriceAscending = new() { DisplayName = "Cena: najdroższe", Option = 1 };
        public readonly static SortOption OrderByName= new() { DisplayName = "Nazwa: od A do Z", Option = 2 };
        public readonly static SortOption OrderByNameInverted = new() { DisplayName = "Nazwa: od Z do A", Option = 3 };

        public readonly static List<SortOption> SortOptionsList = new()
        {
            OrderByPriceDescending,
            OrderByPriceAscending,
            OrderByName,
            OrderByNameInverted,
        };

        public static List<string> GetDisplayNames()
        {
            return SortOptionsList.Select(x => x.DisplayName).ToList();
        }

        public static int GetOptionByDisplayName(string name)
        {
            name = name.ToLower();
            return SortOptionsList.FirstOrDefault(x => x.DisplayName.ToLower().Equals(name))?.Option ?? -1;
        }

        public static string GetDisplayNameByOption(int option)
        {
            return SortOptionsList.FirstOrDefault(x => x.Option == option)?.DisplayName ?? string.Empty;
        }
    }
}
