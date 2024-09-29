using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; // Newtonsoft.Json for JSON formatting
using Newtonsoft.Json.Linq;
using WalletScanner.Data;
using WalletScanner.Repositories;
using WalletScanner.Services;

var builder = WebApplication.CreateBuilder(args);

// **1. Configuration**
// Load configuration from appsettings.json and environment variables
builder
    .Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// **2. Services Registration**

// **a. Database Context**
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// **b. Repositories**
builder.Services.AddScoped<WalletRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<WhaleActivityRepository>();
builder.Services.AddScoped<AlertRepository>();
builder.Services.AddScoped<DumpEventRepository>();
builder.Services.AddScoped<TokenRepository>();
builder.Services.AddScoped<WalletHoldingRepository>();

// **c. Services**
builder.Services.AddScoped<BirdseyeApiService>();
builder.Services.AddScoped<TokenDataService>();

// **d. HttpClient Registration**
builder.Services.AddHttpClient(
    "BirdseyeApi",
    client =>
    {
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
        );
        // Additional configuration if needed, e.g., BaseAddress
        // client.BaseAddress = new Uri("https://public-api.birdeye.so/");
    }
);

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
app.MapGet(
    "/tokens",
    async (BirdseyeApiService birdseyeApiService) =>
    {
        var walletAddresses = new List<string>
        {
            "CTFJEcxBjbx8yP8siAqiyQ9QSg7bS3kPH43oRobjsWXw",
            "55NQkFDwwW8noThkL9Rd5ngbgUU36fYZeos1k5ZwjGdn",
            // Add more wallet addresses as needed
        };

        var result = await birdseyeApiService.GetTokenListForWalletsAsync(walletAddresses);
        return Results.Json(result);
    }
);
app.MapGet(
    "/trending-tokens",
    async (BirdseyeApiService birdseyeApiService) =>
    {
        var result = await birdseyeApiService.GetTrendingTokensAsync();
        return result != null ? Results.Json(result) : Results.StatusCode(500);
    }
);

app.MapGet(
    "/token-list",
    async (BirdseyeApiService birdseyeApiService) =>
    {
        var result = await birdseyeApiService.GetTokenListAsync();
        return result != null ? Results.Json(result) : Results.StatusCode(500);
    }
);

app.MapGet("/", () => "You made it to the server. 🏆");

// **5. Run the App**
app.Run();
