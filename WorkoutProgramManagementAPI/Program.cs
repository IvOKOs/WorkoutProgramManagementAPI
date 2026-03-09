using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.MappingProfiles;
using WorkoutProgramManagementAPI.Services.WorkoutPrograms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WorkoutManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IWorkoutProgramsService, WorkoutProgramsService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<WorkoutProgramMappingProfile>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
