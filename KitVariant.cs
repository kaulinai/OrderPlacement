namespace OrderPlacement
{
    public class KitVariant
    {
        public string Name { get; }
        public decimal BasePrice { get; }

        public KitVariant(string name, decimal basePrice)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Name = name;
            BasePrice = basePrice;
        }
    }
}