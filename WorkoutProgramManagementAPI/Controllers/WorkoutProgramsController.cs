using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;
using WorkoutProgramManagementAPI.DTOs;

namespace WorkoutProgramManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutProgramsController : ControllerBase
{
    private readonly WorkoutManagementDbContext _workoutManagementDbContext;
    public WorkoutProgramsController(WorkoutManagementDbContext workoutManagementDbContext)
    {
        _workoutManagementDbContext = workoutManagementDbContext;
    }

    // api/workoutprograms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetWorkoutProgramsDto>>> GetWorkoutPrograms()
    {
        var workoutProgramsDto = await _workoutManagementDbContext.WorkoutPrograms
            .Select(w => new GetWorkoutProgramsDto()
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                Difficulty = w.Difficulty,
            })
            .ToListAsync();

        return Ok(workoutProgramsDto);
    }

    // api/workoutprograms/1
    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkoutProgramDto>> GetWorkoutProgram(int id)
    {
        var workoutProgramDto = await _workoutManagementDbContext.WorkoutPrograms
            .Where(w => w.Id == id)
            .Select(w => new GetWorkoutProgramDto()
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                Difficulty = w.Difficulty,
            })
            .FirstOrDefaultAsync();

        if(workoutProgramDto is null)
        {
            return NotFound("The workout program with the specified id was not found.");
        }

        return Ok(workoutProgramDto);
    }

    // api/workoutprograms
    [HttpPost]
    public async Task<ActionResult<GetWorkoutProgramDto>> CreateWorkoutProgram(CreateWorkoutProgramDto workoutProgramDto)
    {
        var workoutProgram = new WorkoutProgram()
        {
            Name = workoutProgramDto.Name,
            Description = workoutProgramDto.Description,
            Difficulty = workoutProgramDto.Difficulty,
        };
        _workoutManagementDbContext.WorkoutPrograms.Add(workoutProgram);
        await _workoutManagementDbContext.SaveChangesAsync();

        var resultWorkoutProgramDto = new GetWorkoutProgramDto()
        {
            Id = workoutProgram.Id,
            Name = workoutProgramDto.Name,
            Description = workoutProgramDto.Description,
            Difficulty = workoutProgramDto.Difficulty,
        };

        return CreatedAtAction("GetWorkoutProgram", new { id = resultWorkoutProgramDto.Id }, resultWorkoutProgramDto);
    }
}
