using IRBSapi.Client.Interfaces;
using IRBSapi.Client.Services;
using IRBSapi.Entities.IRBSEntities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IRBS_dataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IRBSDatabase") ??
    throw new InvalidOperationException("Connection string not found")));

builder.Services.AddScoped<IResources, ResourceService>();
builder.Services.AddScoped<IBookings, BookingService>();
builder.Services.AddScoped<IResourceBooking, ResourceBookingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
 });

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
