using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//builder.Services.AddDbContext<LandShareDbContext>(options =>
//            options.UseInMemoryDatabase("InMem"));

builder.Services.AddDbContextPool<LandShareDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LandShareDbConnection")));

// Get the database context and apply the migrations
//builder.Services.BuildServiceProvider().GetService<LandShareDbContext>().
//    Database.Migrate();


builder.Services.AddIdentity<UserEntity, UserRoleEntity>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
        })
    .AddEntityFrameworkStores<LandShareDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<ISeekerListingRepository, SeekerListingRepository>();
builder.Services.AddScoped<ILandListingRepository, LandListingRepository>();
builder.Services.AddScoped<IUserListingRepository, UserListingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessagingService, MessagingService>();
builder.Services.AddScoped<IImageHandlerService, ImageHandlerService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
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
