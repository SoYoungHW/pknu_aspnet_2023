using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using aspnet02_boardapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace aspnet02_boardapp.Data
{
    public class ApplicationDbContext : IdentityDbContext // 1번 Dbcontext에서 IdentityDbContext로 변경 /결국DbContext를 쓰는것하고 동일
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) // 생성자
        {
        }

        public DbSet<Board> Boards { get; set; }

    }
}
