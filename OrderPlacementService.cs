namespace OrderPlacement
{
    public class OrderPlacementService
    {
        public List<KitVariant> KitVariants { get; } = new List<KitVariant>();
        public List<Order> Orders { get; } = new List<Order>();

        public decimal DiscountPercentage1 { get; } = 0.05m; // 5% discount
        public decimal DiscountPercentage2 { get; } = 0.15m; // 15% discount

        public int discountThreshold1 = 10;

        public int discountThreshold2 = 50;

        public bool PlaceOrder(int customerId, int quantity, DateTime deliveryDate, string kitVariantName)
        {
            if (deliveryDate <= DateTime.Now || quantity <= 0 || quantity > 999)
            {
                return false;
            }

            KitVariant? kitVariant = KitVariants.FirstOrDefault(kv => kv.Name == kitVariantName);

            if (kitVariant == null)
            {
                return false;
            }

            decimal price = kitVariant.BasePrice;

            if (quantity >= discountThreshold1 && quantity < discountThreshold2)
            {
                price *= (1 - DiscountPercentage1);
            }
            else if (quantity >= discountThreshold2)
            {
                price *= (1 - DiscountPercentage2);
            }

            Order order = new Order(customerId, quantity, deliveryDate, kitVariant, price);
            Orders.Add(order);

            return true;
        }
    }
}