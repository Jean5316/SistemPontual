using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestePontual.Context;
using TestePontual.Repositories;
using TestePontual.Repository;
using TestePontual.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Configure Services = registro de middlewares

//cria automaticamente os objetos das classes
//registrando dependencia string connection
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("ConexaoSqlite")));

//registrando dependencia do repository de cliente
builder.Services.AddTransient<IClienteRepository, ClienteRepositories>();

//configurando session e httpContext
//registrando dependencia httpcontext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//registrando dependencia Session
builder.Services.AddMemoryCache();
builder.Services.AddSession();

//registando dependencia identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//alteração nas politicas de senha do identity
builder.Services.Configure<IdentityOptions>(options =>
{
    //Configuração padrao de senha
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 1;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
    options.User.RequireUniqueEmail = false;
});

//registando dependencia UserRoles
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

//inluindo nova politica e informando necessidade de perfil
builder.Services.AddAuthorization(options =>
    options.AddPolicy("Admin",
        policy =>
        {
            policy.RequireRole("Admin");
        }
    )
);

builder.Services.AddControllersWithViews();





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

CriarPerfisUsuarios(app);

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});
#pragma warning restore ASP0014 // Suggest using top level route registrations


app.Run();

void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedUsers();
        service.SeedRoles();
    }
}
