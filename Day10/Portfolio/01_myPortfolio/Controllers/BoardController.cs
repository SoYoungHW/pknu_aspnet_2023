using _01_myPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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
            IEnumerable<BoardModel> list = _db.Boards.ToList();

            //var totalCount = _db.Boards.Count(); // 12
            //var pageSize = 10; // 게시판 한페이지에 10개씩 리스트
            //var totalPage = totalCount / pageSize; // 1

            //if (totalCount % pageSize > 0) // 데이터가 더있음 34 / 10 = 4가 남음(한페이지 더 추가)
            //{
            //    totalPage++; // 나머지 수가 있으면 전체페이지를 1증가
            //    // 12데이터 일때 2
            //}

            //// 제일 첫번째 페이지, 제일 마지막 페이지
            //var countPage = 10;
            //var startPage = ((page - 1) / countPage) * countPage + 1; // 12데이터 일때 1페이지 starPage = 1
            //var endPage = startPage + countPage - 1; // 10
            //if (totalPage < endPage) endPage = totalPage; // 10

            //int startCount = ((page - 1) * countPage) + 1; // 1
            //int endCount = startCount + (pageSize - 1); // 10

            //// HTML화면에서 사용하기 위해서 선언 == ViewData, TempData 동일한 역할
            //ViewBag.StartPage = startPage;
            //ViewBag.EndPage = endPage;
            //ViewBag.Page = page;
            //ViewBag.TotalPage = totalPage;
            //ViewBag.StartCount = startCount; // 230525 추가

            //var StartCount = new MySqlParameter("startCount", startCount);
            //var EndCount = new MySqlParameter("endCount", endCount);

            //var objBoardList = _db.Boards.FromSql($"CALL New_PagingBoard({StartCount}, {EndCount})").ToList();

            return View(list);
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
        public IActionResult Details(int? Id)
        {
            ViewData["NoScroll"] = "true";
            
            if(Id == null || Id == 0) { return NotFound(); }

            var board = _db.Boards.Find(Id);

            if(board == null) { return NotFound(); }

            board.ReadCount++;
            _db.Boards.Update(board);
            _db.SaveChanges();

            return View(board);
        }
    }
}
