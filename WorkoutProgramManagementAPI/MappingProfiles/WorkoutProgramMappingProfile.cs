using AutoMapper;
using WorkoutManagement.Domain.Models;
using WorkoutProgramManagementAPI.DTOs.WorkoutProgramDtos;
using WorkoutProgramManagementAPI.DTOs.WorkoutSessionDtos;

namespace WorkoutProgramManagementAPI.MappingProfiles
{
    public class WorkoutProgramMappingProfile : Profile
    {
        public WorkoutProgramMappingProfile()
        {
            // do for the reverse too
            CreateMap<WorkoutProgram, GetWorkoutProgramDto>();
            CreateMap<WorkoutProgram, CreateWorkoutProgramDto>();

            CreateMap<WorkoutSession, GetWorkoutSessionDto>();
        }
    }
}
