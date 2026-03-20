using Microsoft.AspNetCore.Mvc;
using WorkoutProgramManagementAPI.DTOs.WorkoutProgramDtos;
using WorkoutProgramManagementAPI.Services.WorkoutPrograms;
using WorkoutProgramManagementAPI.Shared.Result;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetWorkoutProgramDto>>> GetWorkoutPrograms()
    {
        var workoutProgramsResult = await _workoutProgramsService.GetWorkoutPrograms();
        if (workoutProgramsResult.IsFailure)
        {
            var errorDescription = workoutProgramsResult.Error.Description;
            if (workoutProgramsResult.Error == WorkoutProgramError.NotFound)
                return NotFound(errorDescription ?? "No workout programs were found.");
            return StatusCode(500);
        }
        return Ok(workoutProgramsResult.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkoutProgramDto>> GetWorkoutProgram(int id)
    {
        var workoutProgramResult = await _workoutProgramsService.GetWorkoutProgram(id);

        if(workoutProgramResult.IsFailure)
        {
            var errorDescription = workoutProgramResult.Error.Description;
            if (workoutProgramResult.Error == WorkoutProgramError.NotFound)
                return NotFound(errorDescription ?? "The workout program with the specified id was not found.");
            return StatusCode(500);
        }

        return Ok(workoutProgramResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<GetWorkoutProgramDto>> CreateWorkoutProgram(CreateWorkoutProgramDto workoutProgramDto)
    {
        var resultWorkoutProgramDto = await _workoutProgramsService.CreateWorkoutProgram(workoutProgramDto);

        return CreatedAtAction(nameof(GetWorkoutProgram),
                               new { id = resultWorkoutProgramDto.Id },
                               resultWorkoutProgramDto);
    }
}
