using System.ComponentModel.DataAnnotations;

namespace InfoPortalWeb.Models
{
    public class Admin
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public static string Role = "Admin";
    }
}
