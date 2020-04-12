using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruit300Podcast.Models
{
    public class PodcastEpisode
    {        
        public string EpisodeType { get; set; }
        public int PodcastId { get; set; }
        [ForeignKey("PodcastId")]
        public Podcasts Podcasts { get; set; }
        public int Season { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Length { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }   
        [Key]
        public string Guide { get; set; }
        public string Publicationdate { get; set; }
        public string Duration { get; set; }
        public string ExplicitBool { get; set; }

       
    }
}