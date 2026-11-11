using Integradora4.Repositories.Implementations;
using Integradora4.Repositories.Interfaces;
using Integradora4.Services.Implementations;
using Integradora4.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Integradora4;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Integradora4Context>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString(
            "ABMProductosDBConnectionString"
        )
    )
);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

