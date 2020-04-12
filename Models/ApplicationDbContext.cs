using Microsoft.EntityFrameworkCore;

namespace Recruit300Podcast.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Podcasts> Podcasts { get; set; }
        public DbSet<PodcastEpisode> PodcastEpisodes { get; set; }
    }
}
