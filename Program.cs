using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WalletScanner.Data;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Services;
using WalletScanner.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json; // Newtonsoft.Json for JSON formatting
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

// **1. Configuration**
// Load configuration from appsettings.json and environment variables
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

// **2. Services Registration**

// **a. Database Context**
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// **b. Repositories**
builder.Services.AddScoped<WalletRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<WhaleActivityRepository>();
builder.Services.AddScoped<AlertRepository>();
builder.Services.AddScoped<DumpEventRepository>();

// **c. Services**
builder.Services.AddScoped<BirdseyeApiService>();
// Temporarily comment out services that might cause errors
// builder.Services.AddScoped<WhaleMonitorService>();
// builder.Services.AddScoped<CoinStatsService>();
// builder.Services.AddScoped<MetricsService>();
// builder.Services.AddScoped<NotificationService>();

// **d. HttpClient Registration**
builder.Services.AddHttpClient("BirdseyeApi", client =>
{
    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    // Additional configuration if needed, e.g., BaseAddress
    // client.BaseAddress = new Uri("https://public-api.birdeye.so/");
});

// **e. Controllers**
builder.Services.AddControllers();

// **f. Swagger/OpenAPI (for API documentation and testing)**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// **g. Background Services**
// Temporarily disable the hosted service as it's not implemented yet
// builder.Services.AddHostedService<WhaleMonitorBackgroundService>();

// **3. Build the App**
var app = builder.Build();

// **4. Middleware Configuration**
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// **Authentication & Authorization** (Optional - to be implemented)
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

// **Add API Endpoint for GetTokenListForWalletsAsync**
app.MapGet("/tokens", async (BirdseyeApiService birdseyeApiService) =>
{
    var walletAddresses = new List<string>
    {
        "CTFJEcxBjbx8yP8siAqiyQ9QSg7bS3kPH43oRobjsWXw",
        // "55NQkFDwwW8noThkL9Rd5ngbgUU36fYZeos1k5ZwjGdn"
        // Add more wallet addresses as needed
    };

    var result = await birdseyeApiService.GetTokenListForWalletsAsync(walletAddresses);
    return Results.Json(result);
});

// **5. Run the App**
app.Run();
