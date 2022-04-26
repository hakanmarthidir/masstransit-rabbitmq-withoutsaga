using System;
using MassTransit;
using SharedKernel;

namespace thewallstreet_subscriber.Consumer
{
    public class WsjNewsConsumer : IConsumer<INewsEvent>
    {

        public WsjNewsConsumer()
        {

        }

        public async Task Consume(ConsumeContext<INewsEvent> context)
        {
            var message = context.Message;
            if (message != null)
            {
                await Console.Out.WriteLineAsync($"The news was taken by WSJ : {message.NewsTitle}");
            }
        }
    }
}

