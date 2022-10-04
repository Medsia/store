﻿using Store.Data;
using Xunit;

namespace Store.Tests
{
    public class OrderTests
    {

        [Fact]
        public void TotalCount_WithEmptyItems_ReturnsZero()
        {
            var order = CreateEmptyTestOrder();

            Assert.Equal(0, order.TotalCount);
        }

        private static Order CreateEmptyTestOrder()
        {
            return new Order(new OrderDto
            {
                Id = 1,
                Items = new OrderItemDto[0]
            });
        }
        [Fact]
        public void TotalPrice_WithEmptyItems_ReturnsZero()
        {
            var order = CreateEmptyTestOrder();

            Assert.Equal(0m, order.TotalPrice);
        }

        [Fact]
        public void TotalCount_WithNnonEmptyItems_CalculateTotalCount()
        {
            var order = CreateTestOrder();

            Assert.Equal(3 + 5, order.TotalCount);
        }

        private static Order CreateTestOrder()
        {

            return new Order(new OrderDto
            {

                Id = 1,
                Items = new[]
                {
                    new OrderItemDto { ProductId = 1, Price = 10m, Count = 3},
                    new OrderItemDto { ProductId = 2, Price = 100m, Count = 5},
                }
            });
        }

        [Fact]
        public void TotalPrice_WithNonEmptyItems_CalcualtesTotalPrice()
        {
            var order = CreateTestOrder();

            Assert.Equal(3 * 10m + 5 * 100m, order.TotalPrice);
        }

    }
}
