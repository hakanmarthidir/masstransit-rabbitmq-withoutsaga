using System;
using MassTransit;
using SharedKernel;

namespace Consumer
{
    public class NewsConsumer : IConsumer<INewsCommand>
    {
        //you can add Logger or dependencies if you have
        // this time we will not use the specific queue name. we will implement the pub/sub pattern

        private readonly IPublishEndpoint _publishEndpoint;

        public NewsConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<INewsCommand> context)
        {
            var message = context.Message;
            if (message != null)
            {
                // You can persist the data here..look at the scenarios in NewsController - UI
                //persistence is ok and now publish the news for other subscribers. they might be 3rd party companies.
                //nytimes, wallstreet, bild ...

                Console.WriteLine($"The news was inserted : {message.NewsTitle}");

                await this._publishEndpoint.Publish<INewsEvent>(
                    new NewsCreated()
                    {
                        NewsId = Guid.NewGuid(),
                        NewsAuthor = message.NewsAuthor,
                        NewsContent = message.NewsContent,
                        NewsTitle = message.NewsTitle

                    }).ConfigureAwait(false);

            }
        }
    }
}

