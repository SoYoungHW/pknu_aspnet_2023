using aspnet02_boardapp.Data;
using Microsoft.AspNetCore.Identity;
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

            // 2. ASP.NET 계정을 위한 서비스 설정
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();

            // 비밀번호 정책 변경 설정
            builder.Services.Configure<IdentityOptions>(Options =>
            {
                // Custom Password policy // 실무에서는 아래와 같이 변경하면 안됨
                Options.Password.RequireDigit = false; // 영문자 필요여부
                Options.Password.RequireLowercase = false; // 소문자 필요여부
                Options.Password.RequireUppercase = false; // 대문자 필요여부
                Options.Password.RequireNonAlphanumeric = false; // 특수문자 필요여부
                Options.Password.RequiredLength = 4; // 최소 패스워드 길이 수
                Options.Password.RequiredUniqueChars = 0; // 암호 고유문자 갯수
            });

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

            app.UseAuthentication(); // 3. ASP.NET Identity 계정추가
            app.UseAuthorization(); // 권한

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}