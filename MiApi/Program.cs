using Microsoft.EntityFrameworkCore;
using MiApi.Context;
using Microsoft.AspNetCore.HttpOverrides;
using MiApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Crear variable para la cadena de conexi�n de la DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar el servicio para la conexi�n
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString)
);

// Registrar MercadoPagoService
builder.Services.AddScoped<MercadoPagoService>(); // Registrar el servicio

// Configuraci�n de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configuraci�n de encabezados reenviados
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Uso de encabezados reenviados antes de otros middlewares
app.UseForwardedHeaders();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();
