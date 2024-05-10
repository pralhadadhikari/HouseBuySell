using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HouseBuySell.Web.Data;
using HouseBuySell.Infrastructure;
using Microsoft.AspNetCore.Identity.UI.Services;
using HouseBuySell.Infrastructure.Services;
using HouseBuySell.Infrastructure.IRepository;
using HouseBuySell.Infrastructure.Repository.CRUD;
using HouseBuySell.Infrastructure.Repository;
using System.Configuration;
using HouseBuySell.Web.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDbContext<HouseLandContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
 e => e.MigrationsAssembly("HouseBuySell.Web")));


//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient(typeof(ICrudService<>), typeof(CrudService<>));
builder.Services.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
builder.Services.AddTransient<IPropertyRepository, PropertyRepository>();
builder.Services.AddTransient<IRawSqlRepository, RawSqlRepository>();
builder.Services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsBroker", policy =>
    {
        policy.RequireClaim("IsBroker", "True");
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedIntialData.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
//void SeedDatabase(IApplicationBuilder app, IWebHostEnvironment env)
//{
//    // Other configuration

//    // Seed initial data
//    using (var scope = app.ApplicationServices.CreateScope())
//    {
//        var serviceProvider = scope.ServiceProvider;
//        var roleManager = serviceProvider.GetRequiredService < RoleManager<IdentityRole>>();
//        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//        SeedIntialData.Initialize(roleManager, userManager).Wait();
//    }
//}





if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
//SeedDatabase();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
