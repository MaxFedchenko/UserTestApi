using System.ComponentModel.DataAnnotations;

namespace UserTestApi.DTOs
{
    public class LoginDTO
    {
        [Required]
        public required string UserName { get; set; }
    }
}
