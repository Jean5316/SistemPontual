using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestePontual.Context;
using TestePontual.Repositories;
using TestePontual.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Configure Services = registro de middlewares

//cria automaticamente os objetos das classes
//injeção dependencia string connection
builder.Services.AddDbContext<ClienteContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("ConexaoSqlite")));

//registrando dependencia do repository de cliente
builder.Services.AddTransient<IClienteRepository, ClienteRepositories>();

//configurando session e httpContext
//configurando httpcontext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ClienteContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

//configurando Session
builder.Services.AddMemoryCache();
builder.Services.AddSession();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Configure
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
