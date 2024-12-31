using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderDataSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderDataSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded!!!");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new()
                {
                    UserName = "chula",
                    FirstName = "Chula",
                    LastName = "Fernando",
                    EmailAddress = "chulafernando@eCommerce.net",
                    AddressLine = "Oslo",
                    Country = "Sri Lanka",
                    TotalPrice = 750,
                    State = "Oslo",
                    ZipCode = "0860",

                    CardName = "Visa",
                    CardNumber = "1234567890123456",
                    CreatedBy = "chula",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "chula",
                    LastModifiedDate = new DateTime(),
                }
            };
        }
    }
}
