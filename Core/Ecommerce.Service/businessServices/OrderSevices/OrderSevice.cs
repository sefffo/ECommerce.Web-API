using AutoMapper;
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Service.Specifications;
using Ecommerce.Shared.DTOs.IdentityDto_s;
using Ecommerce.Shared.DTOs.OrderDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.businessServices.OrderSevices
{
    public class OrderSevice(IMapper mapper, ICartRepo cartRepo, IUnitOfWork unitOfWork) : IOrderService
    {
        //create w b3dha ngeeb el order b3dha ytdaf fe el DB 
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            //map address to orderAddress
            var OdrderAddress = mapper.Map<AddressDto, OrderAddress>(orderDto.Address);
            //get Cart
            var Cart = await cartRepo.GetCartAsync(orderDto.CartId) ?? throw new CartNotFound(orderDto.CartId);
            //create order item list 
            List<OrderItem> orderItems = [];
            //get product repo 3shna a7na hndifm 3ntrik el id mn gwa 3ndi 
            var productRepo = unitOfWork.GetRepository<Product, int>();
            //h3ml check if all items in the cart itemthe user added is found inside the product repo 
            foreach (var item in Cart.Items)
            {
                var Product = await productRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                //lw mwgood f3ln emla el order item bta3k 
                var orderItem = new OrderItem()
                {
                    Product = new ItemTobeOrderd()
                    {
                        ProductId = item.Id,
                        ProductImgUrl = Product.PictureUrl,
                        ProductName = item.Name,
                    },
                    Quantity = item.Quantity,
                    Price = Product.Price, //3shan mytl3bsh feeh
                };
                orderItems.Add(orderItem);
            }
            //get delivery method
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId) ?? throw new DeliveryMethodNotFound(orderDto.DeliveryMethodId);
            //sub total
            var SubTotal = orderItems.Sum(p => p.Price * p.Quantity);
            //gm3 el Order bta3k b2a 
            var order = new Order(Email, OdrderAddress, DeliveryMethod, orderItems, SubTotal);
            unitOfWork.GetRepository<Order,Guid>().add(order);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Order,OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var orders = await unitOfWork.GetRepository<Order,Guid>().GetAllWithSpecificatonsAsync(Spec);
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderById(Guid OrderId)
        {
            var Spec = new OrderSpecifications(OrderId);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdWithSpecificationsAync(Spec);
            return mapper.Map<Order,OrderToReturnDto>(order);

        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        
    }
}
