using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Application.Commands;
using Ordering.API.Application.Queries;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductQueries _productQueries;

        public OrdersController(IMediator mediator, IProductQueries productQueries)
        {
            _mediator = mediator;
            _productQueries = productQueries;
        }

        // POST api/orders
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] CreateOrderCommand createOrderCommand)
        {
            foreach (var item in createOrderCommand.OrderItems)
            {
                var product = await _productQueries.GetProductBySKUAsync(item.SKU);

                if (product != null)
                {
                    item.ProductId = product.Id;
                    item.UnitPrice = product.UnitPrice;
                }
            }

            return await _mediator.Send(createOrderCommand);
        }
    }
}