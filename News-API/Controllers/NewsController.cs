using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        ISendEndpointProvider _sendEndpointProvider;
        public NewsController(ISendEndpointProvider sendEndpointProvider)
        {
            this._sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNews([FromBody] NewsRegisterDto newsRegisterDto)
        {

            // Scenario 1
            // this API project might have a PersistenceLayer and you can persist the data in related database.
            // afterwards you can send your message to subscribers. of course message sending type will be  changed.
            // if i would choose this scenario, i would implemente DDD approach and fire message with domain events.

            // Scenario 2 :
            // This API does not have any persistence layer.
            // you might check your data status and afterwards are able to send data into persistence-queue.
            // data-consumer will be consuming data which you sent and persisting the data.
            // then you can send your message to subscribers.

            // of course you can increase the scenario count.

            // Let s continue with Scenario 2
            // API will send the message to specific end point
            // Data-Consumer will publish the message to all subscriber. Methodology is different.

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:news-register-queue")).ConfigureAwait(false);
            await endpoint.Send<INewsCommand>(newsRegisterDto);

            return Ok();
        }
    }
}

