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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BoardModel model)
        {
            try
            {
                model.PostDate = DateTime.Now;
                _db.Boards.Add(model);
                _db.SaveChanges();

                TempData["succeed"] = "새 게시글이 작성되었습니다";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"게시글 작성에 오류가 발생하였습니다. {ex.Message}";
            }

            return RedirectToAction("Index", "Board");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BoardModel model)
        {
            try
            {
                model.PostDate = DateTime.Now;
                _db.Boards.Update(model);
                _db.SaveChanges();

                TempData["succeed"] = "게시글을 수정하였습니다.";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"게시글 수정 중 오류가 발생하였습니다. {ex.Message}";
            }

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public IActionResult Delete(int? Id) 
        {
            ViewData["NoScroll"] = "true";

            if (Id == null || Id == 0) { return NotFound(); }

            var board = _db.Boards.Find(Id);
            if(board == null) { return NotFound(); }

            return View(board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var board = _db.Boards.Find(Id);
            if (board == null) { return NotFound(); }

            _db.Boards.Remove(board);
            _db.SaveChanges();

            TempData["succeed"] = "게시글을 삭제했습니다.";

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public IActionResult Details()
        {
            ViewData["NoScroll"] = "true";
            return View();
        }
    }
}
