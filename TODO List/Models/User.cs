using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO_List.Models
{
	public class User
	{
		public int Id { get; set; }

		[Length(2,25,ErrorMessage ="Name Must be in Range [2,25] Character")]
		public string? Name { get; set; }

		[EmailAddress]
		public string? Email { get; set; }

		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
		public virtual IEnumerable<Category>? Categories { get; set; }

	}
}
