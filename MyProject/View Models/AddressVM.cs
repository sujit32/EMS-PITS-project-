using MyProject.Models;
using System.ComponentModel.DataAnnotations;
namespace MyProject.View_Models

{

    public class AddressVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter username")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Please enter email")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Please enter password")]
        public string? Password { get; set; }

        public string? Gender { get; set; }


        [Required(ErrorMessage = "Please enter address")]
        public string? HomeTown { get; set; }

        public long Phone { get; set; }
        public IFormFile? Image { get; set; }

    }
}
