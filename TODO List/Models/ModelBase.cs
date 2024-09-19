using System.ComponentModel.DataAnnotations;

namespace TODO_List.Models
{
    public class ModelBase
    {
        public int Id { get; set; }
        [Required]
        [Length(minimumLength: 2, 250, ErrorMessage = "Title Must be in Range [2,25] Character")]
        public string? Name { get; set; }
        [Required]
        [MaxLength(2500, ErrorMessage = "Max Length Must Be 2500")]
        public string? Text { get; set; }
    }
}
