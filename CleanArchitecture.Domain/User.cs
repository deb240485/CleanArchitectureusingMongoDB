using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain
{
    public class User : BaseEntity
    {
       [Required(ErrorMessage = "User name is required")]
       public string? userName { get; set; }
       [Required(ErrorMessage = "Password is required")]
       public byte[]? password { get; set; }
       public byte[]? passwordKey { get; set; }
    }
}
