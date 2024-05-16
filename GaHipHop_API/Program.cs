﻿using AutoMapper;
using GaHipHop_Model.Mapper;
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using GaHipHop_Repository;
using GaHipHop_Repository.Repository;
using GaHipHop_Service.Service.Interfaces;
using GaHipHop_Service.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Service

builder.Services.AddScoped<IAdminService, AdminService>();

//Mapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
builder.Services.AddSingleton<IMapper>(config.CreateMapper());

// Add services to the container.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 23)); // Replace with your actual MySQL server version
builder.Services.AddDbContext<MyDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MyDB");
    options.UseMySql(connectionString, serverVersion, options => options.MigrationsAssembly("GaHipHop_API"));
}
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Service add o day
//builder.Services.AddScoped<IUserService, UserService>();

//Build CORS
/*builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    // Dòng ở dưới là đường cứng
    //build.WithOrigins("https:localhost:3000", "https:localhost:7022");

    //Dòng dưới là nhận hết
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseAuthorization();

app.MapControllers();

app.Run();
