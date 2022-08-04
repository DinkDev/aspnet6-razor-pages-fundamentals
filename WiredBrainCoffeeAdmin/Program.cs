using WiredBrainCoffeeAdmin.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WiredBrain") ?? "Data Source=WiredBrain.db";

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<WiredContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WiredBrain"));
});

// add the same instance per the lifetime of the HTTP request (scope)
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();