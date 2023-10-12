using Microsoft.AspNetCore.Mvc;
using QuanLyHangHoa.Models;
using static QuanLyHangHoa.Data.ApplicaitonDbContext;
using Newtonsoft.Json;

namespace QuanLyHangHoa.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            IEnumerable<HangHoaModel> objCatlist = _context.HangHoa;
            return View(objCatlist);
        }
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }
        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        [Route("addcart/{id:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int id)
        {
            var product = _context.HangHoa
                .Where(p => p.Id == id)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");
            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.hangHoa.Id == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, hangHoa = product });
            }
            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return  View("Cart",cart);
        }

        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.hangHoa.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return View("Cart", cart);
        }

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.hangHoa.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }
        [Route("/Cart", Name = "Cart")]
        public IActionResult Cart()
        {
            return View("Cart",GetCartItems());
        }


        [Route("Checkout", Name = "Checkout")]
        public IActionResult Checkout()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng
            var cart = GetCartItems();

            // Tính tổng tiền
            float total = (float)cart.Sum(item => item.quantity * item.hangHoa.GIa);

            // Tạo đơn hàng mới
            var donHang = new DonHangModel
            {
                ngayBan = DateTime.Now,
                tongTien =(float) total,
                trangThai = "Đang xử lý" // Hoặc giá trị trạng thái mặc định khác
            };

            // Lưu đơn hàng vào cơ sở dữ liệu
            _context.DonHang.Add(donHang);
            _context.SaveChanges();

            // Lưu chi tiết đơn hàng
            foreach (var cartItem in cart)
            {
                var chiTietDonHang = new ChiTietDonHangModel
                {
                    orderId = donHang.id,
                    productId = cartItem.hangHoa.Id,
                    soLuong = cartItem.quantity,
                    gia = (float)cartItem.hangHoa.GIa
                };

                _context.CTDonHang.Add(chiTietDonHang);
            }

            // Xóa giỏ hàng sau khi đã Checkout
            ClearCart();

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            // Chuyển đến trang cảm ơn hoặc trang xác nhận đơn hàng
            return View("Cart", cart);
        }

        [Route("Statistical", Name = "Statistical")]
        public IActionResult Statistical()
        {
            // Trả về view để người dùng nhập điều kiện thống kê
            return View();
        }
        [HttpPost]
        [Route("Statistical", Name = "Statistical")]

        public IActionResult Statistical(DateTime tuNgay, DateTime denNgay)
        {
            var thongKeData = _context.CTDonHang
                .Where(ct => ct.order.ngayBan >= tuNgay && ct.order.ngayBan <= denNgay)
                .GroupBy(ct => new { ct.productId, ct.product.Ten })
                .Select(group => new ThongKeModel
                {
                    MaHang = group.Key.productId,
                    TenHang = group.Key.Ten,
                    SoLuong = group.Sum(ct => ct.soLuong)
                })
                .ToList();

            return View("Statistical", thongKeData);
        }





    }
}
