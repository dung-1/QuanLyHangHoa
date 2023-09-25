using Microsoft.EntityFrameworkCore;
using QuanLyHangHoa.Models;

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
        }
    }
}
