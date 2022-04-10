namespace ComputerShop.Shared.Models
{
    public class DropdownNavigationItem
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }

    public class DropdownNavigationItems
    {
        public List<DropdownNavigationItem> Items = new();
        public DropdownNavigationItems() { }
        public DropdownNavigationItems(List<DropdownNavigationItem> items)
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
