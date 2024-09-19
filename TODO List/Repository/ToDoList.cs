namespace TODO_List
{
    public class ToDoList : IToDoList
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoList(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;  
            _userManager = userManager;
        }

        public ToDoList()
        {
        }

        public async Task CreateToDoAsync(ToDo todo )
        {
            if (todo is null)
                throw new Exception("This Todo Is Null!!!");

            _context.Add(todo);
            await _context.SaveChangesAsync();
        }

        //-----------------------------
        public IEnumerable<ToDo> GetAllToDo(ApplicationUser user)
        {
            var exsitingTodo = _context.ToDos
                .Where(x=>x.UserId == user.Id)
                .ToList();
            if (exsitingTodo is null)
                throw new Exception("This Todo Is Null!!!");

            return exsitingTodo;
        }

        public async Task<ToDo> GetByIdAync(int id, ApplicationUser user)
        {
            var exsitingTodo = _context.ToDos
                .Where(x => x.UserId == user.Id)
                .FirstOrDefault(e => e.Id == id);
            if(exsitingTodo is null)
                throw new Exception("This Todo Is Null!!!");

            return exsitingTodo;
        }

        public async Task UpdateToDoAsync(int id, ToDo newTodo, ApplicationUser user)
        {
            var exsitingTodo = await GetByIdAync(id,user);

            if (exsitingTodo is null)
                throw new Exception("This Todo Is Null!!!");


            newTodo.Name = newTodo.Name;
            newTodo.CreatedAt = newTodo.CreatedAt;
            newTodo.CreatedEnd = newTodo.CreatedEnd;
            newTodo.CategoryId = newTodo.CategoryId;
            newTodo.StatusTask = newTodo.StatusTask;


            _context.Update(exsitingTodo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteToDoAsync(int id, ApplicationUser user)
        {
            var exsitingTodo = await GetByIdAync(id, user);
            if(exsitingTodo is null)
                throw new Exception("This Todo Is Null!!!");

            _context.Remove(exsitingTodo);
            await _context.SaveChangesAsync();  
        }

        public  Task<IEnumerable<ToDo>> GetTodoWithCategory(ApplicationUser user)
        {

            var todos = _context.ToDos.Include(c => c.Category)
                .Where(x=>x.UserId == user.Id)
                .ToList();
            return Task.FromResult(todos.AsEnumerable());
        }
    }
}
