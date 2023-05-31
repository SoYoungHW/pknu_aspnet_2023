using Microsoft.EntityFrameworkCore;

namespace TodoAPIServer.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { // 생성자 마법사로 만들것
        }

        public DbSet<TodoItem> TodotItems { get; set; } // 없으면 db랑 연결안됨
    }
}
