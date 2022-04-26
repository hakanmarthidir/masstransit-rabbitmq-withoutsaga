using MassTransit;
using Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NewsConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("news-register-queue", c =>
        {
            c.ConfigureConsumer<NewsConsumer>(context);
        });

        cfg.Host("localhost", "news", h =>
        {
            h.Username("tadev");
            h.Password("tadev");
        });

        cfg.UseCircuitBreaker(c =>
        {
            c.TrackingPeriod = TimeSpan.FromMinutes(1);
            c.ResetInterval = TimeSpan.FromMinutes(5);
            c.TripThreshold = 15;
            c.ActiveThreshold = 10;
        });

        cfg.UseRetry(r => r.Interval(10, 1000));
        cfg.UseMessageRetry(r => r.Immediate(20));
        cfg.UseRateLimit(100, TimeSpan.FromSeconds(1));

        cfg.ConfigureEndpoints(context);
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseAuthorization();

app.MapControllers();

app.Run();

