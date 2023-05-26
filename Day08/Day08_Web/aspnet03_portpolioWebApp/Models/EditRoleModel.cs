using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aspnet02_boardapp.Models
{
    public class EditRoleModel
    {
        [DisplayName("권한아이디")]
        public string Id { get; set; }

        [Required(ErrorMessage = "권한이름이 필요합니다")]
        [DisplayName("권한이름")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }

        public EditRoleModel() 
        {
            Users = new List<string>();
        }
    }
}
