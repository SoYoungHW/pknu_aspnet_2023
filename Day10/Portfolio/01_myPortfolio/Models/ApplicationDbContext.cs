using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _01_myPortfolio.Models;

namespace _01_myPortfolio.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // 게시판 DB 모델
        public DbSet<BoardModel> Boards { get; set; }

        // 포트폴리오 DB 모델
        public DbSet<PortfolioModel> Portfolios { get; set; }

        //// 포트폴리오 DB 모델
        //public DbSet<_01_myPortfolio.Models.TempPortfoliomodel>? TempPortfoliomodel { get; set; }
        // 멋대로 만들어지니까 삭제해야함
    }
}
