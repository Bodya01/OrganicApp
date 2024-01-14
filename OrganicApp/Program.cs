using Microsoft.EntityFrameworkCore;
using OrganicApp.Context;
using OrganicApp.Data;
using OrganicApp.Services.Implementations;
using OrganicApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<OrganicContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddHttpClient();

builder.Services.AddScoped<IMonitoringService, MonitoringService>();

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<OrganicContext>();

    DataSeeder.Seed(dbContext);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();