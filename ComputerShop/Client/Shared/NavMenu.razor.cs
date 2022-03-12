namespace ComputerShop.Client.Shared
{
    public partial class NavMenu
    {
        List<string> PeripheryList = new List<string> 
        { 
            "Myszy", 
            "Klawiatury", 
            "Słuchawki", 
            "Monitory", 
            "Drukarki",
        };

        List<string> ComponentsList = new() 
        { 
            "Procesory", 
            "Karty graficzne", 
            "Płyty główne", 
            "Pamięci RAM", 
            "Zasilacze", 
            "Dyski HDD",
            "Dyski SSD",
            "Obudowy", 
            "Chłodzenie",
            "Przewody" 
        };


        bool collapseNavMenu = true;
        bool isLogIn = true;
        string username = "test";

        string NavMenuCssClass => collapseNavMenu ? " collapse" : "";

        void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        void CollapseNavMenu()
        {
            collapseNavMenu = true;
        }
        protected override async Task OnInitializedAsync()
        {
            await CategoryService.LoadAsync();
            base.OnInitialized();
        }
        protected void OnPartsClicked(int id)
        {
            collapseNavMenu = true;
            switch (id)
            {
                case 0:
                    NavigationManager.NavigateTo("cpu");
                    break;
                case 1:
                    NavigationManager.NavigateTo("gpu");
                    break;
                case 2:
                    NavigationManager.NavigateTo("motherboard");
                    break;
                case 3:
                    NavigationManager.NavigateTo("ram");
                    break;
                case 4:
                    NavigationManager.NavigateTo("psu");
                    break;
                case 5:
                    NavigationManager.NavigateTo("hdd");
                    break;
                case 6:
                    NavigationManager.NavigateTo("ssd");
                    break;
                case 7:
                    NavigationManager.NavigateTo("case");
                    break;
                case 8:
                    NavigationManager.NavigateTo("cooler");
                    break;
                case 9:
                    NavigationManager.NavigateTo("cabel");
                    break;
                default:
                    break;
            }
        }

        protected void OnPeripheriesClicked(int id)
        {
            switch (id)
            {
                case 0:
                    NavigationManager.NavigateTo("mouse");
                    break;
                case 1:
                    NavigationManager.NavigateTo("keyboard");
                    break;
                case 2:
                    NavigationManager.NavigateTo("headphones");
                    break;
                case 3:
                    NavigationManager.NavigateTo("montior");
                    break;
                case 4:
                    NavigationManager.NavigateTo("printer");
                    break;
                default:
                    break;
            }
        }
    }
}
