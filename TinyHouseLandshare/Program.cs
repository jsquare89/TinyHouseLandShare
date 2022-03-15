using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<LandShareDbContext>(options =>
            options.UseInMemoryDatabase("InMem"));


builder.Services.AddIdentity<UserEntity, UserRoleEntity>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
        })
    .AddEntityFrameworkStores<LandShareDbContext>();


builder.Services.AddControllersWithViews();




var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

InitializeDatabase.Initialize(app);
app.Run();
