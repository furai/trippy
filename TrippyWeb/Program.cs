using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Helpers;
using TrippyWeb.Model;
using TrippyWeb.Services;

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<ITripService, TripService>();

var connectionString = builder.Configuration.GetConnectionString("TrippyDatabase");
var serverVersion = new MariaDbServerVersion(new Version(10, 6, 7));

builder.Services.AddDbContext<TrippyWebDbContext>(options => options.UseMySql(
    connectionString, serverVersion
));

builder.Services.AddDefaultIdentity<TrippyUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
}
).AddEntityFrameworkStores<TrippyWebDbContext>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

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
        SeedData.Initialize(context);
    }
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();
