using AutoMapper;
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Models.Cart;
using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.DTOs.CartDto_s;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.businessServices.PaymentService
{
    public class PaymentService(IConfiguration configuration, ICartRepo cartRepo, IUnitOfWork unitOfWork ,IMapper mapper) : IPaymentService
    {
        public async Task<CartDto> CreateOrUpdatePaymentIntentAsync(string CartId)
        {
            //conigure stripe First :Package
            StripeConfiguration.ApiKey = configuration["StripeSettings:secretKey"];
            //get Cart by id 
            var Cart = await cartRepo.GetCartAsync(CartId) ?? throw new CartNotFound(CartId);
            //get amount of products + Delivery method cost 
            //we need the product repo just to get the items in the cart with their ids 
            var ProductRepo = unitOfWork.GetRepository<Domain.Models.Products.Product, int>();
            //collection of cart items 
            foreach (var item in Cart.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                item.Price = product.Price;
            }
            var DM = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Cart.DeliveryMethodId.Value) ?? throw new DeliveryMethodNotFound(Cart.DeliveryMethodId.Value);
            Cart.ShippingPrice = DM.Price;
            var CartAmount = (long)(Cart.Items.Sum(p => p.Quantity * p.Price) + DM.Price) * 100;
            //create intent [create - update]
            var paymentService = new PaymentIntentService();

            if (Cart.PaymentIntentId is null)
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = CartAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };

                var PaymentIntent = await paymentService.CreateAsync(options);

                Cart.PaymentIntentId = PaymentIntent.Id;
                Cart.ClientSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = CartAmount
                };
                await paymentService.UpdateAsync(Cart.PaymentIntentId, Options);
            }
            await cartRepo.CreateUpdateCartAsync(Cart);
            return mapper.Map<UserCart, CartDto>(Cart);

        }
    }
}
