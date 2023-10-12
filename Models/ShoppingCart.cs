namespace QuanLyHangHoa.Models
{
    public class ShoppingCart
    {
        public List<HangHoaModel> Items { get; set; }

        public ShoppingCart()
        {
            Items = new List<HangHoaModel>();
        }

        // Phương thức để thêm sản phẩm vào giỏ hàng
        public void AddItem(HangHoaModel product)
        {
            Items.Add(product);
        }
    }

}
