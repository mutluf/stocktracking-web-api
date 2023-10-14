using Autofac.Core;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockTracking.Domain.Entities;
using StockTracking.Infrastructure;
using StockTracking.Infrastructure.Hubs;
using StockTracking.Infrastructure.SqlTableDependency;
using StockTracking.Infrastructure.SqlTableDependency.Middleware;
using StockTracking.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var assembly = AppDomain.CurrentDomain.GetAssemblies();
// Add services to the container.
IConfiguration configuration = builder.Configuration;

builder.Services.Configure<EventBusSettings>(options => configuration.GetSection(nameof(EventBusSettings)).Bind(options));
//builder.Services.AddEventBusService(configuration);

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddGoogle(options =>
//    {
//        options.ClientId = configuration["Authentication:Google:ClientId"];
//        options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//    });


builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
    .AddCookie(x =>
    {
        x.Cookie.Name = "access_token";

    })
    .AddJwtBearer("User", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };


    });
    

builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 102400000;
    e.EnableDetailedErrors = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
app.UseSwagger();
app.UseDatabaseSubscription<DatabaseSubscription<Product>>("Products");
app.UseRouting();
app.UseAuthorization();

app.UseHangfireDashboard();
app.MapHub<ProductHub>("/productshub");

app.Run();
