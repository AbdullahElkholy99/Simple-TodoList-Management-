var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.ConfigureServiceTodo();
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.ConfigureIdentityUserAndRole();
builder.Services.ConfigureSqlContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Welcome}/{id?}");

app.Run();


/*
🚀 Exciting News!
I’m thrilled to share my latest project: an MVC-based To-Do List application! 🎉

Project Overview:
A "To-Do List" is such a simple yet powerful tool that helps boost productivity and keep tasks organized. My app makes it super easy to manage your tasks and time efficiently.

🌟 Features I'm Proud Of:

Add Tasks: Seamlessly create tasks that need to be done.
Set Start Time & Deadlines: Keep track of when tasks should begin and when they’re due. ⏳
Track Progress: Stay on top of your work with statuses like "Completed," "To Do," and "In Progress." ✅
Categorize Tasks: Organize tasks by project, type, or priority to stay laser-focused. 🔍
User Customization: Each user gets their own personalized categories and task lists! 🙌
What I Delivered:

Full CRUD functionality for managing tasks effortlessly.
Robust Authentication, Authorization, and Role Management for secure user access. 🔐

 
 */
