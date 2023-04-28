using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.Models
{
    public class WeatherDbContext:DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options):base(options) 
        { }
        public DbSet<Weather> weather { get; set; }
    }
}
