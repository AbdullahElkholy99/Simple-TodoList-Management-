using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO_List.Models
{
	public class ToDo:ModelBase
	{
        [Required]
        public DateTime? CreatedAt { get; set; }
		[Required]
        public DateTime? CreatedEnd { get; set; }
		[Required]
		public string? StatusTask { get; set; } = "ToDo";

        [Required]
		[NotMapped]
		public List<string>? Status { get; set; } =
			new List<string>(){ "Completed", "ToDo", "In Progress" };


		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; } // Assuming you're using string UserId
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
