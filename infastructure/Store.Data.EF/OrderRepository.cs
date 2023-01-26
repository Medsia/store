using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    class OrderRepository : IOrderRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public OrderRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<Order> CreateAsync()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = Order.DtoFactory.Create();
            dto.OrderState = TemporaryData.OrderStates.Keys.First();

            dbContext.Orders.Add(dto);
            await dbContext.SaveChangesAsync();

            return Order.Mapper.Map(dto);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = await dbContext.Orders
                                     .Include(order => order.Items)
                                     .SingleAsync(order => order.Id == id);

            return Order.Mapper.Map(dto);
        }

        public async Task UpdateAsync(Order order)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<Order> GetAll()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = dbContext.Orders
                               .Include(order => order.Items)
                               .AsEnumerable()
                               .Select(Order.Mapper.Map)
                               .ToArray();

            return dto;
        }
        public async Task DeleteNotFullOrdersAsync()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var orders = dbContext.Orders.Where(x => x.FullOrder == false);

            foreach(var order in orders)
            {
                if(order != null)
                    dbContext.Orders.Remove(order);
            }
            

            await dbContext.SaveChangesAsync();
        }
    }
}
