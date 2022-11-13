using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using Store.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class OrderService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        protected ISession Session => httpContextAccessor.HttpContext.Session;

 
        public OrderService(IProductRepository productRepository, 
                            IOrderRepository orderRepository, 
                            INotificationService notificationService, 
                            IHttpContextAccessor httpContextAccessor)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<(bool hasValue, OrderModel model)> TryGetModelAsync()
        {
            var (hasValue, order) = await TryGetOrderAsync();
            if (hasValue)
                return (true, await MapAsync(order));

            return (false, null);
        }

        internal async Task<(bool hasValue, Order order)> TryGetOrderAsync()
        {
            if (Session.TryGetCart(out Cart cart))
            {
                var order = await orderRepository.GetByIdAsync(cart.OrderId);
                return (true, order);
            }

            return (false, null);
        }

        //internal async Task<(bool hasValue, ShippingDetails shippingDetails)> TryGetshippingDetailsAsync()
        //{
        //    if (Session.TryGetCart(out Cart cart))
        //    {
        //        var shippingDetails = await orderRepository.GetByIdAsync(cart.OrderId);

        //    }

        //    return (false, null);
        //}

        internal async Task<OrderModel> MapAsync(Order order)
        {
            var products = await GetProductsAsync(order);
            var items = from item in order.Items
                        join product in products on item.ProductId equals product.Id
                        select new OrderItemModel
                        {
                            ProductId = product.Id,
                            Title = product.Title,
                            Count = item.Count,
                            Price = item.Price,
                        };

            return new OrderModel
            {
                Id = order.Id,
                Items = items.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
                CellPhone = order.CellPhone,
                DeliveryDescription = order.Delivery?.Description,
                PaymentDescription = order.Payment?.Description,
            };
        }

        internal async  Task<IEnumerable<Product>> GetProductsAsync(Order order)
        {
            var productIds = order.Items.Select(item => item.ProductId);

            return await productRepository.GetAllByIdsAsync(productIds);
        }

        public async Task<OrderModel> AddProductAsync(int productId, int count)
        {
            if (count < 1)
                throw new InvalidOperationException("Too few Products to add");

            var (hasValue, order) = await TryGetOrderAsync();

            if (!hasValue)
                order = await orderRepository.CreateAsync();

            await AddOrUpdateProductAsync(order, productId, count);
            UpdateSession(order);

            return await MapAsync(order);
        }

        internal async Task AddOrUpdateProductAsync(Order order, int productId, int count)
        {
            var product = await productRepository.GetByIdAsync(productId);

            if(order.Items.TryGet(productId, out OrderItem orderItem))
                orderItem.Count += count;
            else
                order.Items.Add(product.Id, product.Price, count);

            await orderRepository.UpdateAsync(order);
        }

        internal void UpdateSession(Order order)
        {
            var cart = new Cart(order.Id, order.TotalCount, order.TotalPrice);
            Session.Set(cart);
        }

        public async Task<OrderModel> UpdateProductAsync(int productId, int count)
        {
            var order = await GetOrderAsync();
            order.Items.Get(productId).Count = count;

            await orderRepository.UpdateAsync(order);
            UpdateSession(order);

            return await MapAsync(order);
        }

        public async Task<OrderModel> RemoveProductAsync(int productId)
        {
            var order = await GetOrderAsync();
            order.Items.Remove(productId);

            await orderRepository.UpdateAsync(order);
            UpdateSession(order);

            return await MapAsync(order);
        }

        public async Task<Order> GetOrderAsync()
        {
            var (hasValue, order) = await TryGetOrderAsync();

            if (hasValue)
                return order;

            throw new InvalidOperationException("Empty session.");
        }

        public async Task<OrderModel> SendConfirmationAsync(string cellPhone)
        {
            var order = await GetOrderAsync();
            var model = await MapAsync(order);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                int confirmationCode = 1111; // todo: random.Next(1000,10000);
                model.CellPhone = cellPhone;
                Session.SetInt32(cellPhone, confirmationCode);
                Session.SetString(confirmationCode.ToString(), cellPhone);
                await notificationService.SendConfirmationCodeAsync(cellPhone, confirmationCode);
            }
            else
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +ххх(хх)ххх-хх-хх.";

            return model;
        }

        private readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        internal bool TryFormatPhone(string cellPhone, out string formattedPhone)
        {
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(cellPhone, "ru");
                formattedPhone = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
                return true;
            }
            catch (NumberParseException)
            {
                formattedPhone = null;
                return false;
            }
        }

        public async Task<OrderModel> ConfirmCellPhoneAsync(string cellPhone, int confirmationCode)
        {
            cellPhone = Session.GetString(confirmationCode.ToString());
            var model = new OrderModel();
            int? storedCode;
            try
            {
                storedCode = Session.GetInt32(cellPhone);
            }
            catch
            {
                model.Errors["confirmationCode"] = "Неверный код. Проверьте и попробуйте еще раз.";
                return model;
            }


            if (storedCode == null)
            {
                model.Errors["cellPhone"] = "Что-то случилось. Попробуйте получить код еще раз.";
                return model;
            }

            if(storedCode != confirmationCode)
            {
                model.Errors["confirmationCode"] = "Неверный код. Проверьте и попробуйте еще раз.";
                return model;
            }
            else 
            {
                var order = await GetOrderAsync();
                order.CellPhone = cellPhone;
                await orderRepository.UpdateAsync(order);

                Session.Remove(confirmationCode.ToString());
                Session.Remove(cellPhone);

                return await MapAsync(order);
            }
            
        }

        public async Task <OrderModel> SetShippingDetailsAsync(ShippingDetails shippingDetails)
        {
            var order = await GetOrderAsync();

            order.ShippingDetails = shippingDetails;

            await orderRepository.UpdateAsync(order);

            return await MapAsync(order);
        }

        public async Task<OrderModel> SetDeliveryAsync(OrderDelivery delivery)
        {
            var order =  await GetOrderAsync();
            order.Delivery = delivery;
            await orderRepository.UpdateAsync(order);

            return  await MapAsync(order);
        }

        public async Task<OrderModel> SetPaymentAsync(OrderPayment payment)
        {
            var order = await GetOrderAsync();
            order.Payment = payment;
           // var shippingInfo = ShippingDetails.;
            await orderRepository.UpdateAsync(order);
            Session.RemoveCart();

            //await notificationService.StartProcessAsync(order);
            //await notificationService.StartProcessAsync(order, shippingInfo);

            return await MapAsync(order);
        }


        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            var orders = orderRepository.GetAll();

            List<OrderModel> orderModels = new List<OrderModel>();

            foreach (var order in orders)
            {
                orderModels.Add( await MapAsync(order) );
            }

            return orderModels.ToArray();
        }
    }
}
