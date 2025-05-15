using Microsoft.EntityFrameworkCore;
using TenguiNoTengui.Models; // Asegúrate de usar el namespace correcto

namespace TenguiNoTengui.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
