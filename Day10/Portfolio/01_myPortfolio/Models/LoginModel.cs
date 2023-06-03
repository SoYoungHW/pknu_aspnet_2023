using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _01_myPortfolio.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        [DisplayName("아이디")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "패스워드를 입력해주세요.")]
        [DataType(DataType.Password)]
        [DisplayName("패스워드")]
        public string Password { get; set; }

        [DisplayName("로그인 유지")]
        public bool RememberMe { get; set; }
    }
}
