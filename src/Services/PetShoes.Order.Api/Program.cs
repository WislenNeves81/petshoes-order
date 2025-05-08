using PetShoes.Order.Infrastructure.IoC;
using Scalar.AspNetCore;

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

new RootBootstrapper().BootstrapperRegisterServices(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("PetShoes Api")
            .WithSidebar(false);
        //options
        //    .WithPreferredScheme("Bearer")
        //    .WithHttpBearerAuthentication(bearer =>
        //    {
        //        bearer.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyJ3aXNsZW4ubmV2ZXNAZ21haWwuY29tLmJyIiwid2lzbGVuLm5ldmVzQGdtYWlsLmNvbS5iciJdLCJqdGkiOiI1ODExOGY5Ni1lZjY0LTQ2YWUtYjA3NC1kZWI3M2MyOGM2ZjkiLCJ1c2VyTmFtZSI6Ildpc2xlbiBOZXZlcyIsImlzTWFzdGVyQWNjb3VudCI6IlRydWUiLCJyb2xlIjoidXNlciIsIm5iZiI6MTc0NjQ4OTgwMSwiZXhwIjoxNzQ5MDgxODAxLCJpYXQiOjE3NDY0ODk4MDEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxOTQiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTk0In0.Ctck9cx0EPEco9CZyLdJsNAHsPVJEdSNEex-jwcdvlM";
        //    });
    });
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.MapControllers();
app.Run();
