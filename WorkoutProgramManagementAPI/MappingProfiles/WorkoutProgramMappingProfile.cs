using AutoMapper;
using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.WorkoutProgramDtos;

namespace WorkoutProgramManagementAPI.MappingProfiles
{
    public class WorkoutProgramMappingProfile : Profile
    {
        public WorkoutProgramMappingProfile()
        {
            CreateMap<WorkoutProgram, GetWorkoutProgramDto>();
            // do the reverse
            CreateMap<WorkoutProgram, CreateWorkoutProgramDto>();
        }
    }
}
