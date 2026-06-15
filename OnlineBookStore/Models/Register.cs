using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models
{
    public class Register
    {
        public int userId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string passwords { get; set; }

      

    }
}
