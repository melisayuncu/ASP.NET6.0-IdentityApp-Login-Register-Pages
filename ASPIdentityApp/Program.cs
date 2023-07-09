using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASPIdentityApp.Areas.Identity.Data;


//we will use the builder to add more services into this application 
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ASPIdentityDBContextConnection") ?? throw new InvalidOperationException("Connection string 'ASPIdentityDBContextConnection' not found.");

builder.Services.AddDbContext<ASPIdentityDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ASPIdentityDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();


//to view pages(login,register) we have to add the support for Razor pages inside the app
builder.Services.AddRazorPages();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});



//for configuration of this project
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
