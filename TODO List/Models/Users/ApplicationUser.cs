using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO_List.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        [EmailAddress]
        public string? Email { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual IEnumerable<Category>? Categories { get; set; }
    }
}
