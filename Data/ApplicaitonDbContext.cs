using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using QuanLyHangHoa.Models;
using System.Reflection.Emit;

namespace QuanLyHangHoa.Data
{
    public class ApplicaitonDbContext
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<HangHoaModel> HangHoa { get; set; }
            public DbSet<ChiTietDonHangModel> CTDonHang { get; set; }
            public DbSet<DonHangModel> DonHang { get; set; }


        }
    }
}
