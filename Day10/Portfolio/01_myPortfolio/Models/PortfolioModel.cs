using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_myPortfolio.Models
{
    public class PortfolioModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "구분은 필수입니다.")]
        [DisplayName("구분")]
        [Column("Division", TypeName = "Varchar(20)")]
        public string Division { get; set; }

        [Required(ErrorMessage = "제목은 필수입니다.")]
        [DisplayName("제목")]
        [Column("Title", TypeName = "Varchar(100)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "설명은 필수입니다.")]
        [DisplayName("설명")]
        [Column("Description", TypeName = "Varchar(250)")]
        public string Description { get; set; }

        [DisplayName("URL")]
        [Column("Url", TypeName = "Varchar(250)")]
        public string? Url { get; set; }

        [DisplayName("파일명")]
        [FileExtensions(Extensions = ".png, .jpg, .jpeg", ErrorMessage = "이미지 파일을 선택하세요.")]
        [Column("FileName", TypeName = "Varchar(300)")]
        public string? FileName { get; set; }   
    }

    // 파일업로드를 위한 중간단계 모델
    public class TempPortfoliomodel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "구분은 필수입니다.")]
        [DisplayName("구분")]
        [Column("Division", TypeName = "Varchar(20)")]
        public string Division { get; set; }

        [Required(ErrorMessage = "제목은 필수입니다.")]
        [DisplayName("제목")]
        [Column("Title", TypeName = "Varchar(100)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "설명은 필수입니다.")]
        [DisplayName("설명")]
        [Column("Description", TypeName = "Varchar(250)")]
        public string Description { get; set; }

        [DisplayName("URL")]
        [Column("Url", TypeName = "Varchar(250)")]
        public string? Url { get; set; }

        // 실제 이미지를 받아서 저장하기 위한 중간단계 객체
        // [NotMapped]
        [DisplayName("파일명 *() 권장")]
        public IFormFile? PortfolioImage { get; set; }

        [DisplayName("파일명")]
        [FileExtensions(Extensions = ".png, .jpg, .jpeg", ErrorMessage = "이미지 파일을 선택하세요.")]
        [Column("FileName", TypeName = "Varchar(300)")]
        public string? FileName { get; set; }
    }

}
