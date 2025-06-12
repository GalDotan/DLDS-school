
namespace BlazorToDoList.Model
{
    public interface IToDoList
    {
        void AddTask(Task task);
        int RemoveTask(int taskid);
        void UpdateTask(Task task);
        List<Task> GetAllTasks();
        Task GetTaskById(int id);
        List<Subject> GetAllSubjects();    
    }
}
