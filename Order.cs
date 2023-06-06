namespace OrderPlacement
{
    public class Order
    {
        public int CustomerId { get; }
        public int Quantity { get; }
        public DateTime DeliveryDate { get; }
        public KitVariant KitVariant { get; }
        public decimal Price { get; }

        public Order(int customerId, int quantity, DateTime deliveryDate, KitVariant kitVariant, decimal price)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("CustomerId must be greater than 0.", nameof(customerId));
            }

            if (quantity <= 0 || quantity > 999)
            {
                throw new ArgumentException("Quantity must be a positive round number less than or equal to 999.", nameof(quantity));
            }

            if (deliveryDate <= DateTime.Now)
            {
                throw new ArgumentException("DeliveryDate must be in the future.", nameof(deliveryDate));
            }

            if (kitVariant == null)
            {
                throw new ArgumentNullException(nameof(kitVariant));
            }

            CustomerId = customerId;
            Quantity = quantity;
            DeliveryDate = deliveryDate;
            KitVariant = kitVariant;
            Price = price;
        }
    }
}