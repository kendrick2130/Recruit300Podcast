using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Recruit300Podcast.Models
{
    public class Podcasts
    {
        [Key]
        public int PodcastId { get; set; }   
        public string Title { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string OwnerName { get; set; }     
        public string OwnerEmail { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string ExplicitBool { get; set; }

        public ICollection<PodcastEpisode> PodcastEpisodes { get; set; }

    }
}