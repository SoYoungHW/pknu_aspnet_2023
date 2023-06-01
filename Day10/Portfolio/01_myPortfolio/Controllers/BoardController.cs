using _01_myPortfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace _01_myPortfolio.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BoardController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["NoScroll"] = "true";
            IEnumerable<BoardModel> objBoardList = _db.Boards.ToList();

            return View(objBoardList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["NoScroll"] = "true";
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            ViewData["NoScroll"] = "true";

            if (Id == null || Id == 0) { return NotFound(); }
            
            var board = _db.Boards.Find(Id);
            if (board == null) { return NotFound(); }   

            return View(board);
        }

        [HttpGet]
        public IActionResult Delete() 
        {
            ViewData["NoScroll"] = "true";
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            ViewData["NoScroll"] = "true";
            return View();
        }
    }
}
