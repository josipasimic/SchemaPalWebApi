using System.ComponentModel.DataAnnotations;

namespace SchemaPalWebApi.DataTransferObjects
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Potrebno je unijeti korisničko ime.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Potrebno je unijeti lozinku.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne podudaraju.")]
        public string PasswordConfirmation { get; set; }
    }
}
