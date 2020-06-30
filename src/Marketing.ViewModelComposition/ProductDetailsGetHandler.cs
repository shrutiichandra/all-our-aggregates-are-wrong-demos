﻿using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sales.ViewModelComposition
{
    class ProductDetailsGetHandler : ICompositionRequestsHandler
    {
        [HttpGet("products/details/{id}")]
        public async Task Handle(HttpRequest request)
        {
            var id = (string)request.HttpContext.GetRouteData().Values["id"];

            var url = $"http://localhost:5002/api/product-details/product/{id}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic details = await response.Content.AsExpando();
            var vm = request.GetComposedResponseModel();
            vm.ProductName = details.Name;
            vm.ProductDescription = details.Description;
        }
    }
}
