using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders(); // replaced with Serilog
//builder.Logging.AddConsole();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environment == Environments.Development)
{
    builder.Host.UseSerilog(
        (context, loggerConfiguration) => loggerConfiguration
            .MinimumLevel.Debug()
            .WriteTo.Console());
}
else
{
    builder.Host.UseSerilog(
        (context, loggerConfiguration) => loggerConfiguration
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/task_mgr.txt", rollingInterval: RollingInterval.Day));
            /* .WriteTo.ApplicationInsights(new TelemetryConfiguration
            {
                InstrumentationKey = builder.Configuration["ApplicationInsightsInstrumentationKey"]
            }, TelemetryConverter.Traces)); */
}

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

/* builder.Services.AddControllers(options => // if you need to change input/output format uncomment it 
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();  */

builder.Services.AddProblemDetails(); // descriptive error details


builder.Services.AddDbContext<TaskDbContext>(DbContextOptions  // Regsitered with Scoped lifetime 
    => DbContextOptions.UseSqlServer( 
        builder.Configuration["ConnectionStrings:TaskDbContext"]));  

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
