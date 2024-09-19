using TODO_List.Models;

namespace TODO_List
{
    public interface IToDoList
    {
        //CRUD OPeration:

        Task CreateToDoAsync(ToDo todo);

        IEnumerable<ToDo> GetAllToDo(ApplicationUser user);
        Task<ToDo> GetByIdAync(int id, ApplicationUser user);
        Task<IEnumerable<ToDo>> GetTodoWithCategory(ApplicationUser user);
        Task UpdateToDoAsync(int id,ToDo todo, ApplicationUser user);
        Task DeleteToDoAsync(int id, ApplicationUser user);    
    }
}
