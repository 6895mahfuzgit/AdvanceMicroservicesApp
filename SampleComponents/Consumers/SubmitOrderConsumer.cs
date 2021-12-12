﻿using MassTransit;
using SampleContracts;
using System.Threading.Tasks;

namespace SampleComponents.Consumers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            await context.RespondAsync<OrderSubmissionAccepted>(new { InVar.Timestamp });
        }
    }
}
