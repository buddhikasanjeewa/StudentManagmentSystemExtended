using Business_Logic_Layer;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoftoneStudentManagmentSystem;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add dependancy injection for DAL and BLL layers
builder.Services.RegisterDALDependencies(builder.Configuration); 
builder.Services.RegisterBLLDependencies(builder.Configuration);


//var conString = builder.Configuration.GetConnectionString("StuConnStr") ??
//     throw new InvalidOperationException("Connection string 'StuConnStr'" +
//    " not found.");
//builder.Services.AddDbContext<SofttOneSmsnewContext>(options =>
//    options.UseSqlServer(conString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS to allow requests from any origin, header, and method
//Allow 3rd party frond end to connect this Api
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.MapControllers();

app.Run();
