namespace ComputerShop.Shared.Models.Products
{
    public class StringValue
    {
        public string Value { get; set; }

        public static List<StringValue> GetStringValues(List<string> strings)
        {
            List<StringValue> values = new();
            strings.ForEach(s => values.Add(new StringValue { Value = s }));
            return values;
        }
    }
}
