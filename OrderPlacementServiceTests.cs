using Xunit;

namespace OrderPlacement
{
    public class OrderPlacementServiceTests
    {
        [Fact]
        public void PlaceOrder_ValidParameters_ReturnsTrue()
        {
            // Arrange
            var service = new OrderPlacementService();
            service.KitVariants.Add(new KitVariant("DNA", 98.99m));
            var customerId = 1;
            var quantity = 5;
            var deliveryDate = DateTime.Now.AddDays(7);
            var kitVariantName = "DNA";

            // Act
            bool result = service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Assert
            Assert.True(result);
            Assert.Single(service.Orders);
        }

        [Fact]
        public void PlaceOrder_InvalidKitVariant_ReturnsFalse()
        {
            // Arrange
            var service = new OrderPlacementService();
            var customerId = 1;
            var quantity = 5;
            var deliveryDate = DateTime.Now.AddDays(7);
            var kitVariantName = "InvalidKitVariant";

            // Act
            bool result = service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Assert
            Assert.False(result);
            Assert.Empty(service.Orders);
        }

        [Fact]
        public void PlaceOrder_QuantityAboveDiscountThreshold1_ReturnsDiscountedPrice1()
        {
            // Arrange
            var service = new OrderPlacementService();
            service.KitVariants.Add(new KitVariant("DNA", 98.99m));

            var customerId = 1;
            var quantity = 15;
            var deliveryDate = DateTime.Now.AddDays(7);
            var kitVariantName = "DNA";

            // Act
            bool result = service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Assert
            Assert.True(result);
            Assert.Single(service.Orders);

            Order order = service.Orders.First();
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(quantity, order.Quantity);
            Assert.Equal(deliveryDate, order.DeliveryDate);
            Assert.Equal(kitVariantName, order.KitVariant.Name);

            decimal expectedBasePrice = 98.99m;
            decimal expectedDiscountedPrice = expectedBasePrice * (1 - service.DiscountPercentage1);
            Assert.Equal(expectedDiscountedPrice, order.Price);
        }

        [Fact]
        public void PlaceOrder_QuantityAboveDiscountThreshold2_ReturnsDiscountedPrice2()
        {
            // Arrange
            var service = new OrderPlacementService();
            service.KitVariants.Add(new KitVariant("DNA", 98.99m));

            var customerId = 1;
            var quantity = 60;
            var deliveryDate = DateTime.Now.AddDays(7);
            var kitVariantName = "DNA";

            // Act
            bool result = service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Assert
            Assert.True(result);
            Assert.Single(service.Orders);

            Order order = service.Orders.First();
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(quantity, order.Quantity);
            Assert.Equal(deliveryDate, order.DeliveryDate);
            Assert.Equal(kitVariantName, order.KitVariant.Name);

            decimal expectedBasePrice = 98.99m;
            decimal expectedDiscountedPrice = expectedBasePrice * (1 - service.DiscountPercentage2);
            Assert.Equal(expectedDiscountedPrice, order.Price);
        }

        [Fact]
        public void PlaceOrder_InvalidDeliveryDate_ReturnsFalse()
        {
            // Arrange
            var service = new OrderPlacementService();
            service.KitVariants.Add(new KitVariant("DNA", 98.99m));
            var customerId = 1;
            var quantity = 5;
            var deliveryDate = DateTime.Now.AddDays(-1); // Invalid delivery date (in the past)
            var kitVariantName = "DNA";

            // Act
            bool result = service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Assert
            Assert.False(result);
            Assert.Empty(service.Orders);
        }

        [Fact]
        public void PlaceOrder_InvalidQuantity_ReturnsFalse()
        {
            // Arrange
            var service = new OrderPlacementService();
            service.KitVariants.Add(new KitVariant("DNA", 98.99m));
            var customerId = 1;
            var quantity = -5; // Invalid quantity (negative)
            var deliveryDate = DateTime.Now.AddDays(7);
            var kitVariantName = "DNA";

            // Act
            bool result = service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Assert
            Assert.False(result);
            Assert.Empty(service.Orders);
        }

        [Fact]
        public void PlaceOrder_InvalidKitVariantName_ReturnsFalse()
        {
            // Arrange
            var service = new OrderPlacementService();
            service.KitVariants.Add(new KitVariant("DNA", 98.99m));
            var customerId = 1;
            var quantity = 5;
            var deliveryDate = DateTime.Now.AddDays(7);
            var kitVariantName = "InvalidKitVariant"; // Invalid kit variant name

            // Act
            bool result = service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Assert
            Assert.False(result);
            Assert.Empty(service.Orders);
        }

        [Fact]
        public void ListOrders_EmptyOrders_ReturnsEmptyList()
        {
            // Arrange
            var service = new OrderPlacementService();

            // Act
            List<Order> orders = service.Orders;

            // Assert
            Assert.Empty(orders);
        }

        [Fact]
        public void ListOrders_ReturnsAllOrders()
        {
            // Arrange
            var service = new OrderPlacementService();
            service.KitVariants.Add(new KitVariant("DNA", 98.99m));
            var customerId = 1;
            var quantity = 5;
            var deliveryDate = DateTime.Now.AddDays(7);
            var kitVariantName = "DNA";
            service.PlaceOrder(customerId, quantity, deliveryDate, kitVariantName);

            // Act
            List<Order> orders = service.Orders;

            // Assert
            Assert.Single(orders);
            Assert.Equal(customerId, orders[0].CustomerId);
            Assert.Equal(quantity, orders[0].Quantity);
            Assert.Equal(deliveryDate, orders[0].DeliveryDate);
            Assert.Equal(kitVariantName, orders[0].KitVariant.Name);
        }
    }
}
