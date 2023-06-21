using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Send_Emails_MVC.Models;
using SendGrid.Extensions.DependencyInjection;


var Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AuthMessageSenderOptions"));


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
