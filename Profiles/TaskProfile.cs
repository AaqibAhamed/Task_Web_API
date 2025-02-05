using AutoMapper;
using Task_Web_API.Entities;
using Task_Web_API.Models;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<ToDoItem,ToDoItemDto>(); //Mapping for Get

        CreateMap<ToDoItemCreateDto,ToDoItem>(); // Mapping for CreateTaks- POST

        CreateMap<ToDoItemUpdateDto,ToDoItem>(); // Mapping for UpdateTask - PUT

        CreateMap<ToDoItem,ToDoItemUpdateDto>(); // Mapping for PartiallyUpdateTask - Patch

       
    }
}