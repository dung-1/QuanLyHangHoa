using Microsoft.AspNetCore.Mvc;
using QuanLyHangHoa.Models;
using static QuanLyHangHoa.Data.ApplicaitonDbContext;

namespace QuanLyHangHoa.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.HangHoa.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HangHoaModel empobj)
        {
            if (ModelState.IsValid)
            {
                _context.HangHoa.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Thêm Hàng hóa Thành công !!!";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.HangHoa.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HangHoaModel empobj)
        {
            if (ModelState.IsValid)
            {
                _context.HangHoa.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Sửa Hàng hóa Thành công !!! ";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.HangHoa.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? id)
        {
            var deleterecord = _context.HangHoa.Find(id);
            if (deleterecord == null)
            {
                return NotFound();
            }
            _context.HangHoa.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Xóa Hàng hóa Thành công !!!";
            return RedirectToAction("Index");
        }


    }
}
