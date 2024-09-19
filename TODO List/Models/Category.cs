using System.ComponentModel.DataAnnotations.Schema;
using TODO_List.Models.Users;

namespace TODO_List.Models
{
	public class Category : ModelBase
	{
	 
		[ForeignKey(nameof(ToDo))]
		public int ToDoId { get; set; }
        public virtual IEnumerable<ToDo>? ToDos { get; set; }


        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }


    }
}
