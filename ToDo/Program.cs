using Microsoft.Extensions.Options;
using System.Configuration;
using ToDo.Repository;
using Volo.Abp.Data;






var builder = WebApplication.CreateBuilder(args);
var builder2 = new ConfigurationBuilder().AddJsonFile("/appsettings.json");

// Add services to the container.
builder.Services.AddRazorPages().
    Services.AddSingleton<ITaskRepo, TaskRepo>().
    AddSingleton<IUserRepo, UserRepo>().
    AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(30); }).
    AddMemoryCache().
    AddMvc().
    AddRazorPagesOptions(option => { option.Conventions.AddPageRoute("/Login", ""); });
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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
