using Microsoft.EntityFrameworkCore;
using PriceTracker.Gateway.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<GatewayDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.Run();

