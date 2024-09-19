using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TODO_List.ViewModels;
namespace TODO_List.Data
{
	public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RegisterUserViewModel>().HasNoKey();
            builder.Entity<LoginUserViewModel>().HasNoKey();
            builder.Entity<RoleViewModel>().HasNoKey();
            base.OnModelCreating(builder);
        }

        // public DbSet<User>? Users { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<ToDo>? ToDos { get; set; }
	    public DbSet<TODO_List.ViewModels.RegisterUserViewModel> RegisterUserViewModel { get; set; } = default!;
	    public DbSet<TODO_List.ViewModels.LoginUserViewModel> LoginUserViewModel { get; set; } = default!;
	    public DbSet<TODO_List.ViewModels.RoleViewModel> RoleViewModel { get; set; } = default!;

    }
}
