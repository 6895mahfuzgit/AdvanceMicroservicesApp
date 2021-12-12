using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleContracts;
using System;
using System.Threading.Tasks;

namespace SampleApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRequestClient<SubmitOrder> _submitOrderRequestClient;

        public OrderController(ILogger<OrderController> logger, IRequestClient<SubmitOrder> submitOrderRequestClient)
        {
            _logger = logger;
            _submitOrderRequestClient = submitOrderRequestClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid id, string customerNumber)
        {
            var response = await _submitOrderRequestClient.GetResponse<OrderSubmissionAccepted>(new
            {
                OrderId = id,
                CustomerNumber = customerNumber,
                Timestamp = InVar.Timestamp
            });


            return Ok(response.Message);
        }
    }
}
