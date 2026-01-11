using Clientes.Application.Interfaces.Repositories;
using Clientes.Application.Interfaces.Services;
using Clientes.Application.Services;
using Clientes.Infrastructure.Data;
using Clientes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(ClienteProfile).Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ITipoTelefoneRepository, TipoTelefoneRepository>();
builder.Services.AddScoped<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddScoped<ITelefoneService, TelefoneService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cliente}/{action=Index}/{id?}");

app.Run();
