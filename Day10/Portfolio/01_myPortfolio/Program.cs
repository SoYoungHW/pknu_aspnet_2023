using _01_myPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _01_myPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // * ApplicationContext ����ϱ� ���� ���� �߰� / ���Ṯ�ڿ� �Ҵ�
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                ));

            // asp.net �������
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // ��й�ȣ ��å����
            builder.Services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireDigit = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequiredLength = 4;
                Options.Password.RequiredUniqueChars = 0;
            });

            // ���� ���� �߰�
            builder.Services.AddAuthorization();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}