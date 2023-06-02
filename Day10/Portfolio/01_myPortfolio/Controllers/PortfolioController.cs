using _01_myPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace _01_myPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ApplicationDbContext _db;

        // 파일업로드 웹환경 객체
        private readonly IWebHostEnvironment _environment;

        public PortfolioController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.Portfolios.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TempPortfoliomodel temp)
        {
            if (ModelState.IsValid) 
            {
                string upFileName = UploadImageFile(temp);

                var portfolio = new PortfolioModel
                {
                    Division = temp.Division,
                    Title = temp.Title,
                    Description = temp.Description,
                    Url = temp.Url,
                    FileName = upFileName
                };

                _db.Portfolios.Add(portfolio);
                _db.SaveChanges();

                TempData["succeed"] = "포트폴리오 저장성공";

                return RedirectToAction("Index", "portfolio");
            }

            return View(temp);
        }

        // 업로드를 위한 메서드 
        private string UploadImageFile(TempPortfoliomodel temp)
        {
            var resultFileName = "";
            try
            {
                if (temp.PortfolioImage != null)
                {
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads"); // 파일모아두는 폴더
                    resultFileName = Guid.NewGuid() + "-" + temp.PortfolioImage.FileName; // 파일명 중복 안되게
                    string filePath = Path.Combine(uploadFolder, resultFileName); // 파일확장자

                    using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                    {
                        temp.PortfolioImage.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return resultFileName;
        }

        
    }
}
