using Microsoft.AspNetCore.Identity;
using TODO_List.Data;
using TODO_List.Models.Users;

namespace TODO_List.Extentions
{
	public static class ServiceExtensions
	{
		public static void ConfigureServiceTodo(this IServiceCollection services) =>
			services.AddScoped<IToDoList, ToDoList>();
		public static void ConfigureIdentityUserAndRole(this IServiceCollection services) =>
	   //For User       , For Role
	   services.AddIdentity<ApplicationUser, IdentityRole>(options =>
	   { 
		   options.Password.RequireDigit = false;
		   options.Password.RequireUppercase = false;
	   }).AddEntityFrameworkStores<AppDbContext>();
        public static void ConfigureSqlContext(this IServiceCollection services,
			IConfiguration configuration) =>
			services.AddSqlServer<AppDbContext>(configuration.GetConnectionString("sqlConnection"));

	}
}
