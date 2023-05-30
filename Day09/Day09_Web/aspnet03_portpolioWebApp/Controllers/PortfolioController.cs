using aspnet02_boardapp.Data;
using aspnet03_portfolioWebApp.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace aspnet03_portfolioWebApp.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ApplicationDbContext _db;

        // 파일업로드 웹환경을 위한 객체(필수!)
        private readonly IWebHostEnvironment _environment;

        public PortfolioController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.Portfolios.ToList(); // SELECT *
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // portfolioModel(선택X) -> TempPortfolioMedel(선택O)
            // Create 할때만
            return View();
        }

        [HttpPost]
        public IActionResult Create(TempPortfolioMedel temp)
        {
            // 파일업로드, Temp -> Model db저장
            if (ModelState.IsValid) 
            {
                // 파일업로드되면 새로운 이미지 파일명을 받음
                string upFileName = UploadImageFile(temp);

                // TempPortFolioModel -> portfolioModel 변경
                var portfolio = new portfolioModel
                {
                    Division = temp.Division,
                    Title = temp.Title,
                    Description = temp.Description,
                    Url = temp.Url,
                    FileName = upFileName // 핵심!
                };

                _db.Portfolios.Add(portfolio);
                _db.SaveChanges();

                TempData["succeed"] = "포트폴리오 저장완료";

                return RedirectToAction("Index", "Portfolio");
            }

            return View(temp);
        }

        // Rounting이나 GET/POST랑 관계없는 그냥 메서드
        private string UploadImageFile(TempPortfolioMedel temp)
        {
            var resultFileName = "";
            try
            {
                if (temp.PortfolioImage != null)
                {
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads"); // wwwroot 밑에 uploads라는 폴더존재해야함
                    resultFileName = Guid.NewGuid() + "_" + temp.PortfolioImage.FileName; // 중복된 이미지파일명 제거
                    string filePath = Path.Combine(uploadFolder, resultFileName); // 파일확장자

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        temp.PortfolioImage.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            

            return resultFileName;
        }
    }
}
