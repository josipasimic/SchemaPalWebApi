using System.ComponentModel.DataAnnotations;

namespace SchemaPalWebApi.DataTransferObjects
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Potrebno je unijeti korisničko ime.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Potrebno je unijeti lozinku.")]
        public string Password { get; set; }
    }
}
