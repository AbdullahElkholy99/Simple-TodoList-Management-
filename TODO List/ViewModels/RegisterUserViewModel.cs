using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO_List.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }
 
        [DataType(DataType.Password)]
        [Length(8,25)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Length(8, 25)]
        public string ConfirmPassword { get; set; }
 
        [EmailAddress]
        public string? Email { get; set; }
      

    }
}
