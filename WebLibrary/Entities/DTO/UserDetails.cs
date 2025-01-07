using System.ComponentModel.DataAnnotations;

namespace WebLibrary.Entities.DTO {
    public record UserDetails(
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1}")]
        string? Name,

        [EmailAddress(ErrorMessage = "The {0} field must be a valid email address.")]
        string? Email,

        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 16 characters.")]
        string? Password
        ) { }
}
