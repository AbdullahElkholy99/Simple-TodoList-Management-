using Microsoft.AspNetCore.Mvc.Rendering;
using TODO_List.Pagination;

namespace TODO_List.Controllers
{
    [Authorize]
    public class TODOController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IToDoList _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public TODOController(AppDbContext context,IToDoList service,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _service = service;
            _userManager = userManager;
        }
        public IActionResult Index(int pg = 1)
        {
            return View( );
        }

        //--------------------------------------------------------------
        public async Task<IActionResult> Details(int pg = 1)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var todos = await _service.GetTodoWithCategory(user);

            const int pageSize = 2;
            if (pg < 1) pg = 1;

            int recsCount = todos.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;


            var data = todos.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }
        public async Task<IActionResult> DetailsTask(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var todo = await _service.GetByIdAync(id,user);
            if (todo is null)
                return NotFound();

            return View(todo);
        }
        //------------------------------------------------------

        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var categories = _context.Categories
                .Where(u => u.UserId == user.Id)
                .ToList();
            categories.Insert(0, new Category { Id = 0, Name = "-- Select Department --" });
            ViewData["CategoriesList"] = categories;

            ToDo todo = new();
           
            return View(todo);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ToDo todo)
        {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (!ModelState.IsValid)
            {
                var categories = _context.Categories
                    .Where(u => u.UserId == user.Id)
                    .ToList();
                categories.Insert(0, new Category { Id = 0, Name = "-- Select Department --" });
                ViewData["CategoriesList"] = categories;
                return View(todo);
            }

            if(todo is null)
            {
                return BadRequest("The Task is null");
            }
            todo.UserId = user.Id;
            await _service.CreateToDoAsync(todo);

            return RedirectToAction(nameof(Details));
        }

        //------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new ToDo();
            // Logic to get the current selected status, for example:
            model.StatusTask = "ToDo"; // This is just an example. Retrieve the current status for the specific item.
    
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var categories = _context.Categories
                .Where(u => u.UserId == user.Id)
                .ToList();
            if (categories == null || !categories.Any())
            {
                categories = new List<Category> {
                    new Category { Id = 0, Name = "-- Select Department --" }
                };
            }
            ViewData["CategoriesList"] = new SelectList(categories, "Id", "Name");

            var todo = await _service.GetByIdAync(id, user);
            if (todo is null)
                return NotFound();

            return View(todo);
        }
        [HttpPost()]
        public async Task<IActionResult> Edit(int id,ToDo newTodo)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var categories = _context.Categories
                .Where(u => u.UserId == user.Id)
                .ToList();
            if (categories == null || !categories.Any())
            {
                categories = new List<Category> {
                    new Category { Id = 0, Name = "-- Select Department --" }
                };
            }
            ViewData["CategoriesList"] = new SelectList(categories, "Id", "Name");

            if (ModelState.IsValid)
            {
                var todo = _context.ToDos!.Find(id);
                if (todo is null)
                    return NotFound();

                if (newTodo is null)
                    return NotFound();

                todo.Name = newTodo.Name;
                todo.CreatedAt = newTodo.CreatedAt;
                todo.CreatedEnd = newTodo.CreatedEnd;
                todo.CategoryId = newTodo.CategoryId;
                todo.StatusTask = newTodo.StatusTask;

                _context.Update(todo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details));
            }
            return View(newTodo);
        }

        //------------------------------------------------------
        public async Task<IActionResult> Delete(int id)
        {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            await _service.DeleteToDoAsync(id, user);

            return RedirectToAction(nameof(Details));
        }
        //------------------------------------------------------


    }
}
