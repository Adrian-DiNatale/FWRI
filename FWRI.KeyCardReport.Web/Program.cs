using FWRI.KeyCardReport.Data;
using FWRI.KeyCardReport.Library.Interfaces;
using FWRI.KeyCardReport.Library.Utilities;
using FWRI.KeyCardReport.Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("JsonData"));
builder.Services.AddSingleton<IAppSettings, ConfigurationUtility>();
builder.Services.AddSingleton<IFacilityAccessReportService, FacilityAccessReportService>();


// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


var app = builder.Build();


// Seed the in-memory database once at startup
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    var db = scopedProvider.GetRequiredService<AppDbContext>();
    JSONDataService.LoadJsonDataToInMemoryDatabase(db);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
