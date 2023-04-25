using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain
{
    public class Customer : BaseEntity
    {
        [Required(ErrorMessage = "First name is required")]
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public Int64 phonenumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? email { get; set; }
    }
}
