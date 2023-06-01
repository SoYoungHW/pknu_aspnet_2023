using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace _01_myPortfolio.Models
{
    public class BoardModel
    {
        [Key]
        public int Id { get; set; } // 게시글번호

        [Required(ErrorMessage = "로그인이 필요합니다.")]
        [DisplayName("유저아이디")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "제목을 입력하세요.")]
        [DisplayName("제목")]
        public string Title { get; set; }

        [DisplayName("조회수")]
        public int ReadCount { get; set; }

        [DisplayName("작성일자")]
        public DateTime PostDate { get; set; }

        [Required(ErrorMessage = "게시글 내용을 입력하세요.")]
        [DisplayName("게시글")]
        public string Content { get; set; }    
    }
}
