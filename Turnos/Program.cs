using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

var builder = WebApplication.CreateBuilder(args);


//Inyeccion 

builder.Services.AddDbContext<TurnosContext>(options => options.UseSqlServer("name=ConnectionStrings:Turnoscontext"));

builder.Services.AddSession(option => option.IdleTimeout = TimeSpan.FromSeconds(300));
builder.Services.AddSession(option => option.Cookie.HttpOnly = true);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
