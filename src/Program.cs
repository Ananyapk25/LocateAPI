using FluentValidation;
using LocationNinja.Common;
using LocationNinja.Common.Persistence;
using LocationNinja.Features.IpLocation;
using LocationNinja.Features.IpLocation.Endpoints;
using LocationNinja.Features.IpLocation.Providers.IpApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIpLocationService, IpLocationService>();

builder.Services.Configure<AppSettings>(builder.Configuration);

var settings = builder.Configuration.Get<AppSettings>();
ArgumentNullException.ThrowIfNull(settings, nameof(settings));

builder.Services.AddDbContext<LocationNinjaDbContext>(options =>
{
    options.UseMongoDB(settings.MongoDatabase.Host, settings.MongoDatabase.DatabaseName);
});

builder.Services.AddHttpClient<ILocationAPI, IpApiProvider>(options =>
{
    options.BaseAddress = new Uri(settings.Features.IpLocation.IpApiBaseUrl);
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

builder.Services.AddAutoMapper(domainAssemblies);

builder.Services.AddValidatorsFromAssemblies(domainAssemblies);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIpLocationFeatureEndpoints();

app.Run();

public partial class Program { }