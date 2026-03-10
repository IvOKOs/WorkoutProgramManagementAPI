using Microsoft.EntityFrameworkCore;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.MappingProfiles;
using WorkoutProgramManagementAPI.Services.Users;
using WorkoutProgramManagementAPI.Services.WorkoutPrograms;
using WorkoutProgramManagementAPI.Services.Workouts;
using WorkoutProgramManagementAPI.Services.WorkoutSessions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WorkoutManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<WorkoutProgramMappingProfile>();
});

builder.Services.AddScoped<IWorkoutProgramsService, WorkoutProgramsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IWorkoutsService, WorkoutsService>();
builder.Services.AddScoped<IWorkoutSessionsService, WorkoutSessionsService>();

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
