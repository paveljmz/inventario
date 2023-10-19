using Microsoft.EntityFrameworkCore;
using Inventario;
using Inventario.Datos;
using Inventario.Repositorio;
using Inventario.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductosRepositorio, ProductosRepositorio>();
builder.Services.AddScoped<ITipoProductoRepositorio, TipoProductoRepositorio>();


builder.Services.AddCors(options =>
{
    //NUEVA POLITICA
    options.AddPolicy("NuevaPolitica", app =>
    {
        //PERMITE CUALQUIER ORIGEN
        app.AllowAnyOrigin()
        //PERMIE CUALQUIER CABECERA
        .AllowAnyHeader()
        //PERMITE CUALQUIER METODO
        .AllowAnyMethod();
    });
});
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();
