using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("TrippyDatabase");
var serverVersion = new MariaDbServerVersion(new Version(10, 6, 7));

builder.Services.AddDbContext<TrippyWebDbContext>(options => options.UseMySql(
    connectionString, serverVersion
));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#if DEBUG
builder.Services.AddSassCompiler();
#endif

var app = builder.Build();

switch (app.Environment.EnvironmentName.ToLower())
{
    case "production":
        Console.WriteLine("Running in production mode.");
        break;
    case "staging":
        Console.WriteLine("Running in staging mode.");
        break;
    case "development":
        Console.WriteLine("Running in development mode.");
        break;
    default:
        Console.WriteLine("Running in unknown mode.");
        break;
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHttpsRedirection();
}

bool useMigrations = builder.Configuration.GetValue<bool>("UseMigrations", false);
Console.WriteLine("UseMigrations: " + useMigrations.ToString());

if (!useMigrations)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<TrippyWebDbContext>();
        context.Database.EnsureCreated();
        // DbInitializer.Initialize(context);
    }
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
