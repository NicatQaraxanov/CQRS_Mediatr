using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Persistence.Contexts
{
    public class DataInitializer
    {
        private readonly CQRS_DbContext _dbContext;

        public DataInitializer(CQRS_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            List<Provider> providers = new List<Provider>()
                {
                    new() { Name = "Samsung" },
                    new() { Name = "Sony" },
                    new() { Name = "LG" }
                };

            List<Order> orders = new List<Order>()
                {
                    new() { Date = DateTime.Now.AddDays(-1), Number = "1", Provider = providers[0] },
                    new() { Date = DateTime.Now.AddDays(-25), Number = "2", Provider = providers[1] },
                    new() { Date = DateTime.Now.AddDays(-45), Number = "3", Provider = providers[2] }
                };

            List<OrderItem> orderItems = new List<OrderItem>()
                {
                    new() { Name = "Samsung s23 Ultra", Quantity = 20, Unit = "testUnit1", Orders = new() { orders[0] } },
                    new() { Name = "Xperia Z3", Quantity = 15, Unit = "testUnit2", Orders = new() { orders[1] } },
                    new() { Name = "Test TV", Quantity = 10, Unit = "testUnit3", Orders = new() { orders[2] } },
                };

            if (!_dbContext.Providers.Any())
            {
                _dbContext.Providers.AddRange(providers);
            }

            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.AddRange(orders);
            }

            if (!_dbContext.OrderItems.Any())
            {
                _dbContext.OrderItems.AddRange(orderItems);
            }

            _dbContext.SaveChanges();
        }
    }
}
