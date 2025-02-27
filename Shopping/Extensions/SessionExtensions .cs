using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Shopping.Models;

namespace Shopping.Extensions
{
    public static class SessionExtensions
    {
        private const string CartSessionKey = "CartSessionKey";

        public static void SetCart(this ISession session, List<CartItem> cart)
        {
            session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
        }

        public static List<CartItem> GetCart(this ISession session)
        {
            var cartData = session.GetString(CartSessionKey);
            return cartData != null ? JsonConvert.DeserializeObject<List<CartItem>>(cartData) : new List<CartItem>();
        }
    }
}
