using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyHangHoa.Controllers
{
    public class HangHoaController : Controller
    {
        // GET: HangHoaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HangHoaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HangHoaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HangHoaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HangHoaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HangHoaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HangHoaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HangHoaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
