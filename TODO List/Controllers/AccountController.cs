namespace TODO_List.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // UserManager is services for user
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var userModel = await _userManager.FindByNameAsync(userVM.UserName);

                if (userModel != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found)
                    {
                        //cookie 
                        await _signInManager.SignInAsync(userModel, userVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User Name Or Password Is Wrong");
            }

            return View(userVM);


        }
        #endregion

        #region Register 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                //Mapping from RegisterUserViewModel To ApplicationUser
                ApplicationUser userModel = new();
                userModel.UserName = newUserVM.UserName;
                userModel.Email = newUserVM.Email;
                userModel.PasswordHash = newUserVM.Password;

                //Create New User
                var result = await _userManager.CreateAsync(userModel, newUserVM.Password);
                if (result.Succeeded)
                {
                    //Create Cookie
                    await _signInManager.SignInAsync(userModel, false);//store : ID , Name , Role
                    //Create Role
                    await _userManager.AddToRoleAsync(userModel, "User");
                    return RedirectToAction("Welcome", "Home");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }
            }
            return View(newUserVM);
        }

        #endregion

        #region LogOut
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Register));
        }
        #endregion

        #region Admin Add a new Admin

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel newAdminVM)
        {
            if (ModelState.IsValid)
            {
                //Mapping from RegisterUserViewModel To ApplicationUser
                ApplicationUser userModel = new();
                userModel.UserName = newAdminVM.UserName;
                userModel.Email = newAdminVM.Email;
                userModel.PasswordHash = newAdminVM.Password;

                //Create New User
                var result = await _userManager.CreateAsync(userModel, newAdminVM.Password);
                if (result.Succeeded)
                {
                    //Create Cookie
                    await _signInManager.SignInAsync(userModel, false);//store : ID , Name , Role
                    //Create Role
                     await _userManager.AddToRoleAsync(userModel, "Admin");
                    return NoContent();
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }
            }
            return View(newAdminVM);
        }
        #endregion


    }
}
