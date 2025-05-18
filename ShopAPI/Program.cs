using ShopAPI.Repository;
using ShopAPI.Database;
using System.Data.Common;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// string? papercutConnectionString = builder.Configuration.GetConnectionString("papercut");
// DbConnectionStringBuilder connectionBuilder = new()
// {
//     ConnectionString = papercutConnectionString
// };
//
// Uri endpoint = new(connectionBuilder["Endpoint"].ToString()!, UriKind.Absolute);
// builder.Services.AddScoped(_ => new SmtpClient(endpoint.Host, endpoint.Port));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();