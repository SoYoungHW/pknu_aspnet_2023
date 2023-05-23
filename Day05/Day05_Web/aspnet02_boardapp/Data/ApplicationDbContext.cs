using aspnet02_boardapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace aspnet02_boardapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) // 생성자
        {
        }

        public DbSet<Board> Boards { get; set; }

    }
}
