using Microsoft.EntityFrameworkCore;
using CitizenAPI.Data;
using CitizenAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(opt => opt.AddPolicy("AllowReact", p =>
    p.WithOrigins("http://localhost:5173", "http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddScoped<ICitizenService, CitizenService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.UseCors("AllowReact");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
    
