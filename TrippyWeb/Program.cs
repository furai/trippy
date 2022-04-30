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
