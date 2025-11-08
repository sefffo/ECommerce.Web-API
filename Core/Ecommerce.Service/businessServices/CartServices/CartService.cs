using AutoMapper;
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Models.Cart;
using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
using Ecommerce.Shared.DTOs.CartDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.businessServices.CartServices
{
    public class CartService(ICartRepo Repo, IMapper mapper) : ICartService
    {

        public async Task<CartDto> GetCartAsync(string key)
        {
            var cart = await Repo.GetCartAsync(key);
            if (cart != null)
            {
                var mappedCart = mapper.Map<UserCart, CartDto>(cart);
                return mappedCart;
            }
            else
            {
                throw new CartNotFound(key);

            }

        }
        public async Task<CartDto> CreateUpdateCartAsync(CartDto Cart)
        {
            var UserCartDto = mapper.Map<CartDto, UserCart>(Cart);
            var Savedcart = await Repo.CreateUpdateCartAsync(UserCartDto);
            if (Savedcart != null)
                return await GetCartAsync(Savedcart.Id);//3shan y3ml return ll cart lw hya tmam 
            else
                throw new Exception("Somthing Went Wrong");
        }
        public async Task<bool> DeleteCartAsync(string key)
        {
            var del = await Repo.DeleteCartAsync(key);
            return del ?? false;
        }
    }
}
