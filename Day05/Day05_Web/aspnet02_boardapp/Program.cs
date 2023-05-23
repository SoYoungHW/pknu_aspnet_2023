using aspnet02_boardapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace aspnet02_boardapp
{
    // ASP.NET 실행을 위한 구성초기화
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Data에서 만든 ApplicationContext를 사용하겠다는 설정 추가
            builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseMySql(
                //appsettings.json ConnectionString 내의 연결문자열 할당
                builder.Configuration.GetConnectionString("DefaultConnection"),
                // 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                ));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}