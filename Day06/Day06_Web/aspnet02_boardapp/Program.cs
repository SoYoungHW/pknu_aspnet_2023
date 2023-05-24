using aspnet02_boardapp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace aspnet02_boardapp
{
    // ASP.NET ������ ���� �����ʱ�ȭ
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Data���� ���� ApplicationContext�� ����ϰڴٴ� ���� �߰�
            builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseMySql(
                //appsettings.json ConnectionString ���� ���Ṯ�ڿ� �Ҵ�
                builder.Configuration.GetConnectionString("DefaultConnection"),
                // 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                ));

            // 2. ASP.NET ������ ���� ���� ����
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();

            // ��й�ȣ ��å ���� ����
            builder.Services.Configure<IdentityOptions>(Options =>
            {
                // Custom Password policy // �ǹ������� �Ʒ��� ���� �����ϸ� �ȵ�
                Options.Password.RequireDigit = false; // ������ �ʿ俩��
                Options.Password.RequireLowercase = false; // �ҹ��� �ʿ俩��
                Options.Password.RequireUppercase = false; // �빮�� �ʿ俩��
                Options.Password.RequireNonAlphanumeric = false; // Ư������ �ʿ俩��
                Options.Password.RequiredLength = 4; // �ּ� �н����� ���� ��
                Options.Password.RequiredUniqueChars = 0; // ��ȣ �������� ����
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

            app.UseAuthentication(); // 3. ASP.NET Identity �����߰�
            app.UseAuthorization(); // ����

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}