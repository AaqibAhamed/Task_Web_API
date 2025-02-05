namespace Task_Web_API.Middlewares
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(Guid taskId) : base($"Task with id {taskId} not found.")
        {

        }
    }
}