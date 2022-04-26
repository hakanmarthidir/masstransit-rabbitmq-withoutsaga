using System;
using MassTransit;
using SharedKernel;

namespace newyorktimes_subscriber.Consumer
{
    public class NytNewsConsumer : IConsumer<INewsEvent>
    {

        public NytNewsConsumer()
        {

        }

        public async Task Consume(ConsumeContext<INewsEvent> context)
        {
            var message = context.Message;
            if (message != null)
            {
                await Console.Out.WriteLineAsync($"The news was taken by NYT : {message.NewsTitle}");
            }
        }
    }
}

