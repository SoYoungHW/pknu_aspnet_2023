using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAPIServer.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Column("Title", TypeName = "Varchar(100)")]
        public string? Title { get; set; }

        // datetime -> string
        public string TodoDate { get; set; }
        
        // bool -> int
        public int IsComplete { get; set; }

    }
}
