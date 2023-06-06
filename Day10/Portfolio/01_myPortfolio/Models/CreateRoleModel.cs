using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _01_myPortfolio.Models
{
    public class CreateRoleModel
    {
        [Required(ErrorMessage = "권한명은 필수입니다.")]
        [DisplayName("권한명")]
        public string RoleName { get; set; }
    }
}
