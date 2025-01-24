using AutoMapper;
using Task_Web_API.Models;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<ToDoItem,ToDoItemDto>(); //Mapping for Get

        CreateMap<TaskCreateDto,ToDoItem>(); // Mapping for CreateTaks- POST

        CreateMap<TaskUpdateDto,ToDoItem>(); // Mapping for UpdateTask - PUT

        CreateMap<ToDoItem,TaskUpdateDto>(); // Mapping for PartiallyUpdateTask - Patch

       
    }
}