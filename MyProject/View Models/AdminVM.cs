using System.ComponentModel.DataAnnotations;

namespace MyProject.View_Models
{
    public class AdminVM
    {
        [Required(ErrorMessage = "Please enter email")]
        public string? Email { get; set; }


        [Required(ErrorMessage="Please enter password")]
        public  string? Password { get; set; }
       
    }
}
