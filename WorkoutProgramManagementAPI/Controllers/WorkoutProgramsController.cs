using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WorkoutManagement.Domain.Models;
using WorkoutManagement.Infrastructure;

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
    public async Task<ActionResult<IEnumerable<WorkoutProgram>>> GetWorkoutPrograms()
    {
        return await _workoutManagementDbContext.WorkoutPrograms.ToListAsync();
    }

    // api/workoutprograms/1
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutProgram>> GetWorkoutProgram(int id)
    {
        var workoutProgram = await _workoutManagementDbContext.WorkoutPrograms
            .FindAsync(id);
        if(workoutProgram is null)
        {
            return NotFound("The workout program with the specified id was not found.");
        }

        return Ok(workoutProgram);
    }

    // api/workoutprograms
    [HttpPost]
    public async Task<ActionResult> CreateWorkoutProgram(WorkoutProgram workoutProgram)
    {
        _workoutManagementDbContext.WorkoutPrograms.Add(workoutProgram);
        await _workoutManagementDbContext.SaveChangesAsync();

        return CreatedAtAction("GetWorkoutProgram", new { id = workoutProgram.Id }, workoutProgram);
    }
}
