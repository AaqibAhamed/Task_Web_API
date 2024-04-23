using AutoMapper;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task,TaskDto>(); //Mapping for Get

        CreateMap<TaskCreateDto,Task>(); // Mapping for CreateTaks- POST

        CreateMap<TaskUpdateDto,Task>(); // Mapping for UpdateTask - PUT

        CreateMap<Task,TaskUpdateDto>(); // Mapping for PartiallyUpdateTask - Patch

       
    }
}