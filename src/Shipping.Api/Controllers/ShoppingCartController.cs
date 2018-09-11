﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shipping.Data;

namespace Shipping.Api.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        [HttpGet]
        [Route("products/{ids}")]
        public IEnumerable<dynamic> GetCart(string ids)
        {
            using (var db = ShippingContext.Create())
            {
                var productIds = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                var cartItems = db.ShoppingCartItems
                    .Where(item => productIds.Any(id => id == item.ProductId))
                    .ToArray()
                    .GroupBy(cartItem => cartItem.ProductId)
                    .Select(group => new
                    {
                        ProductId = group.Key,
                        group.First().DeliveryEstimate
                    })
                    .ToArray();

                return cartItems;
            }
        }
    }
}