using System.ComponentModel.DataAnnotations;

namespace WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.AddRequest
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }

    }
}
