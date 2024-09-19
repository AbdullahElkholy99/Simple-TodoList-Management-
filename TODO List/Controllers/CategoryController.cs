using TODO_List.Pagination;

namespace TODO_List.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
  
        public CategoryController(AppDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index( )
        {
            return View( );
        }
        [HttpGet]
        public IActionResult Create()
        {
            Category cat = new ();
            return View(cat);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
           // var user = await _userManager.FindByEmailAsync("ezzat7802@gmail.com");
            Category cat = new();
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User); 
 
            cat.Name = category.Name;
            cat.Text = category.Text;
            // cat.UserId = "b1400f61-b398-4e2c-a15d-e4f5556b6f32";
            cat.UserId = user.Id;

            if (!ModelState.IsValid)
            {
                return BadRequest(category);
            }

            if (category is null)
            {
                return BadRequest("The Category is null");
            }
            _context.Categories.Add(cat);
            _context.SaveChanges();

          //  _service.CreateCategoryAsync(category);
            return RedirectToAction(nameof(Details));
        }

        /// /////////////////////////////////////////////
        public async Task<IActionResult> Details(int pg=1)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var Categories = await _context.Categories.Include(t => t.ToDos)
                .Where(x=>x.UserId == user.Id)
                .ToListAsync();
            if (Categories is null)
                return BadRequest();


            const int pageSize = 5;
            if (pg < 1) pg = 1;

            int recsCount = Categories.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = Categories.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);

         }
        public async Task<IActionResult> DetailsCategory(int id,int pg=1)
        {
            var category = _context.Categories
                .Include(t => t.ToDos)
                .SingleOrDefault(c => c.Id == id);

            // var category = await _service.GetByIdAync(id);
            if (category is null)
                return BadRequest();


            return View(category);
        }

        /// /////////////////////////////////////////////

        public async Task<IActionResult> Edit(int id )
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,Category category)
        {
            if (ModelState.IsValid)
            {

                var existingCategory = _context.Categories.Find(id);

                if (existingCategory is null)
                    return BadRequest("The Category is null");

                existingCategory.Name = category.Name;
                existingCategory.Text = category.Text;
                _context.Categories.Update(existingCategory);
                _context.SaveChanges();
              
                return RedirectToAction(nameof(Details));
            }
            else
            {
                return View(category);
            }
        }

        /// /////////////////////////////////////////////
		public async Task<IActionResult> Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
                return BadRequest();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details));
        }

    }
}
