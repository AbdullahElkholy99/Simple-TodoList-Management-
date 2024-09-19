using Microsoft.IdentityModel.Tokens;

namespace TODO_List.Controllers
{
    [Authorize]//Check Cookie
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IToDoList _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IToDoList service ,
            AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _service = service;
            _userManager = userManager;
        }
        public IActionResult Welcome()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var categories = await _context.Categories
                .Include(t => t.ToDos)
                .Where(x => x.UserId == user.Id).ToListAsync();

            if (categories.IsNullOrEmpty())
                return BadRequest("Ooooops");
            return View(categories);
        }

    }
}
