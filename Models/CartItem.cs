namespace QuanLyHangHoa.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public int quantity { set; get; }
        public HangHoaModel hangHoa { set; get; }
    }
}
