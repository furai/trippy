using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("TrippyDatabase");
builder.Services.AddDbContext<TrippWebDbContext>(options => options.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
