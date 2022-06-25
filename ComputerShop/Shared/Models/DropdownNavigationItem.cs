namespace ComputerShop.Shared.Models
{
    public class NavigationItem
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }

    public class NavigationItems
    {
        public List<NavigationItem> Items = new();
        public NavigationItems() { }
        public NavigationItems(List<NavigationItem> items)
        {
            if(items != null)
                Items.AddRange(items);
        }
        public List<string> GetItemsNames()
        {
            return Items.Select(x => x?.Name)?.ToList();
        }
        public string GetPathByIndex(int index)
        {
            if (index < 0 || index >= Items.Count)
                return null;
            return Items[index]?.Path;
        }
    }
}
