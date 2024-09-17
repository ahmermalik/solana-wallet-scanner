using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WalletScanner.Data;
using Microsoft.EntityFrameworkCore;
using WalletScanner.Services;
using WalletScanner.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
builder.Services.AddScoped<WhaleMonitorService>();
builder.Services.AddScoped<CoinStatsService>();
builder.Services.AddScoped<MetricsService>();
// builder.Services.AddScoped<NotificationService>();


// **d. HttpClient Registration**
builder.Services.AddHttpClient("BirdseyeApi", client =>
{
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// **5. Run the App**
app.Run();
