using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using aspnet02_boardapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using aspnet03_portfolioWebApp.Models;

namespace aspnet02_boardapp.Data
{
    public class ApplicationDbContext : IdentityDbContext // 1번 Dbcontext에서 IdentityDbContext로 변경 /결국DbContext를 쓰는것하고 동일
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) // 생성자
        {
        }

        public DbSet<Board> Boards { get; set; }

        // 포트폴리오를 DB로 관리하기 위한 모델
        public DbSet<portfolioModel> Portfolios { get; set; }

        }
}
