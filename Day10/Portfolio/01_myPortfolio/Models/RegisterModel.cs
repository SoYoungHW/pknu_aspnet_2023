using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _01_myPortfolio.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "아이디는 필수입니다.")]
        [DisplayName("아이디")]
        public string UserId { get; set; }

        [EmailAddress]
        [DisplayName("이메일주소")]
        public string? Email { get; set; }

        [DisplayName("휴대폰번호")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "패스워드는 필수입니다.")]
        [DataType(DataType.Password)]
        [DisplayName("패스워드")]
        public string Password { get; set; }

        [Required(ErrorMessage = "패스워드 확인은 필수입니다.")]
        [DataType(DataType.Password)]
        [DisplayName("패스워드 확인")]
        [Compare("Password", ErrorMessage = "패스워드가 일치하지 않습니다. 다시 입력해주세요.")]
        public string ConfirmPassword { get; set; }
    }
}
