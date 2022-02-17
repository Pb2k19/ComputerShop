namespace ComputerShop.Client.Shared
{
    public partial class NavMenu
    {
        List<string> list = new List<string> { "Myszy", "Klawiatury", "Słuchawki", "Monitory", "Drukarki", "Wszystko" };

        List<string> ComponentsList = new() { "Procesory", "Karty graficzne", "Płyty główne", "Pamięci RAM", "Zasilacze", "Dyski", "Obudowy", "Chłodzenie", "Wszystko" };


        bool collapseNavMenu = true;
        bool isLogIn = false;
        string username = "test";

        string NavMenuCssClass => collapseNavMenu ? " collapse" : "";

        void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnInitializedAsync()
        {
            await CategoryService.LoadAsync();
            base.OnInitialized();
        }

        protected void OnPartsClicked(int id)
        {
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
                    NavigationManager.NavigateTo("drive");
                    break;
                case 6:
                    NavigationManager.NavigateTo("case");
                    break;
                case 7:
                    NavigationManager.NavigateTo("cooling");
                    break;
                default:
                    NavigationManager.NavigateTo("part");
                    break;
            }
        }

        protected void OnPeripheriesClicked(int id)
        {
            switch (id)
            {
                case 0:
                    NavigationManager.NavigateTo("mice");
                    break;
                case 1:
                    NavigationManager.NavigateTo("keyboards");
                    break;
                case 2:
                    NavigationManager.NavigateTo("headphones");
                    break;
                case 3:
                    NavigationManager.NavigateTo("monitors");
                    break;
                case 4:
                    NavigationManager.NavigateTo("printers");
                    break;
                default:
                    NavigationManager.NavigateTo("periphery");
                    break;
            }
        }
    }
}
