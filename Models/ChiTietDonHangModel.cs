using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace QuanLyHangHoa.Models
{
    public class ChiTietDonHangModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int orderId { get; set; } // Required foreign key property
        public DonHangModel order { get; set; } = null!;

        public int productId { get; set; } // Required foreign key property
        public HangHoaModel product { get; set; } = null!;
        public int soLuong { get; set; }
        public float gia {  get; set; }

    }
}
