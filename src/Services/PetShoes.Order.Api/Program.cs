using Marraia.Notifications.Configurations;
using MassTransit;
using PetShoes.Order.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddSmartNotification();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        cfg.Host(configuration.GetSection("RabbitMq:HostName").Value, h =>
        {
            h.Username(configuration.GetSection("RabbitMq:UserName").Value);
            h.Password(configuration.GetSection("RabbitMq:Password").Value);
        });
    });
});

new RootBootstrapper().BootstrapperRegisterServices(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
           options.SwaggerEndpoint("/openapi/v1.json", "PetShoes Api"));
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.MapControllers();
app.Run();
