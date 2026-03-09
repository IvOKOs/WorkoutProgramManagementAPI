using Microsoft.AspNetCore.Mvc;
using WorkoutProgramManagementAPI.DTOs;
using WorkoutProgramManagementAPI.Services;

namespace WorkoutProgramManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutProgramsController : ControllerBase
{
    private readonly IWorkoutProgramsService _workoutProgramsService;

    public WorkoutProgramsController(IWorkoutProgramsService workoutProgramsService)
    {
        _workoutProgramsService = workoutProgramsService;
    }

    // api/workoutprograms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetWorkoutProgramDto>>> GetWorkoutPrograms()
    {
        var workoutProgramsDto = await _workoutProgramsService.GetWorkoutProgramsAsync();
        return Ok(workoutProgramsDto);
    }

    // api/workoutprograms/1
    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkoutProgramDto>> GetWorkoutProgram(int id)
    {
        var workoutProgramDto = await _workoutProgramsService.GetWorkoutProgramAsync(id);

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
        var resultWorkoutProgramDto = await _workoutProgramsService.CreateWorkoutProgram(workoutProgramDto);

        return CreatedAtAction(nameof(GetWorkoutProgram),
                               new { id = resultWorkoutProgramDto.Id },
                               resultWorkoutProgramDto);
    }
}
