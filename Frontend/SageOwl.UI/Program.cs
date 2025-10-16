using SageOwl.UI.Models;
using SageOwl.UI.Services.Implementations;
using SageOwl.UI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<ITeamService,TeamService>();
builder.Services.AddScoped<IAnnouncementService,AnnouncementService>();
builder.Services.AddScoped<IFormService,FormService>();
builder.Services.AddScoped<IQualificationService,QualificationService>();
builder.Services.AddSingleton<CurrentTeam>();
builder.Services.AddSingleton<CurrentUser>();

builder.Services.AddHttpClient("Backend", client =>
    client.BaseAddress = new Uri("https://localhost:7027/api/"));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseMiddleware<TokenRefreshMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
