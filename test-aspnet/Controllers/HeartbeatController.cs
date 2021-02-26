using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapr;
using Dapr.Client;

namespace test_aspnet.Controllers
{
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        private readonly ILogger<HeartbeatController> logger;

        public HeartbeatController(ILogger<HeartbeatController> logger)
        {
            this.logger = logger;
        }

        [Topic("pubsub", "heartbeat")]
        [HttpPost("heartbeat")]
        public ActionResult Heartbeat(Pulse pulse, [FromServices] DaprClient daprClient)
        {
            Console.WriteLine($"Pulse {{ {pulse.Beat}, {pulse.Time} }}");
            logger.LogDebug($"Pulse {{ {pulse.Beat}, {pulse.Time} }}");
            // var state = await daprClient.GetStateEntryAsync<Account>(StoreName, transaction.Id);
            // state.Value ??= new Account() { Id = transaction.Id, };
            // state.Value.Balance += transaction.Amount;
            // await state.SaveAsync();
            return Ok();
        }
    }
}