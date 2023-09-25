using System.ComponentModel.DataAnnotations;

namespace QuanLyHangHoa.Models
{
    public class HangHoaModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ma { get; set; }
        public string Ten { get; set; }
        public double? GIa { get; set; }

    }
}
