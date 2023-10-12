using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace QuanLyHangHoa.Models
{
    public class DonHangModel
    {
        [Key]
        [Required]
        public int id { get; set; }
        public DateTime ngayBan { get; set; }
        public float tongTien { get; set; }
        public string? trangThai{ get; set; }
        public ICollection<ChiTietDonHangModel> ctdh { get; } = new List<ChiTietDonHangModel>();

    }
}
