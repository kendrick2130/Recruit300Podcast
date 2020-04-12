using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recruit300Podcast.Models;

namespace Recruit300Podcast.Controllers
{
    public class PodcastEpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PodcastEpisodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PodcastEpisodes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PodcastEpisodes.Include(p => p.Podcasts);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PodcastEpisodes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcastEpisode = await _context.PodcastEpisodes
                .Include(p => p.Podcasts)
                .FirstOrDefaultAsync(m => m.Guide == id);
            if (podcastEpisode == null)
            {
                return NotFound();
            }

            return View(podcastEpisode);
        }

        // GET: PodcastEpisodes/Create
        public IActionResult Create()
        {
            ViewData["PodcastId"] = new SelectList(_context.Podcasts, "PodcastId", "PodcastId");
            return View();
        }

        // POST: PodcastEpisodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EpisodeType,PodcastId,Season,EpisodeNumber,Title,Description,ImageUrl,Link,Length,Type,Url,Guide,Publicationdate,Duration,ExplicitBool")] PodcastEpisode podcastEpisode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(podcastEpisode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PodcastId"] = new SelectList(_context.Podcasts, "PodcastId", "PodcastId", podcastEpisode.PodcastId);
            return View(podcastEpisode);
        }

        // GET: PodcastEpisodes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcastEpisode = await _context.PodcastEpisodes.FindAsync(id);
            if (podcastEpisode == null)
            {
                return NotFound();
            }
            ViewData["PodcastId"] = new SelectList(_context.Podcasts, "PodcastId", "PodcastId", podcastEpisode.PodcastId);
            return View(podcastEpisode);
        }

        // POST: PodcastEpisodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EpisodeType,PodcastId,Season,EpisodeNumber,Title,Description,ImageUrl,Link,Length,Type,Url,Guide,Publicationdate,Duration,ExplicitBool")] PodcastEpisode podcastEpisode)
        {
            if (id != podcastEpisode.Guide)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podcastEpisode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodcastEpisodeExists(podcastEpisode.Guide))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PodcastId"] = new SelectList(_context.Podcasts, "PodcastId", "PodcastId", podcastEpisode.PodcastId);
            return View(podcastEpisode);
        }

        // GET: PodcastEpisodes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcastEpisode = await _context.PodcastEpisodes
                .Include(p => p.Podcasts)
                .FirstOrDefaultAsync(m => m.Guide == id);
            if (podcastEpisode == null)
            {
                return NotFound();
            }

            return View(podcastEpisode);
        }

        // POST: PodcastEpisodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var podcastEpisode = await _context.PodcastEpisodes.FindAsync(id);
            _context.PodcastEpisodes.Remove(podcastEpisode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodcastEpisodeExists(string id)
        {
            return _context.PodcastEpisodes.Any(e => e.Guide == id);
        }
        public IActionResult RSSFeed()
        {
            IEnumerable<PodcastEpisode> episodes = _context.PodcastEpisodes.Include(e => e.Podcasts).ToList();

            List<string> returneString = new List<string>();
            returneString.Add("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>");
            returneString.Add("<rss version=\"2.0\" xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\">");
            foreach (PodcastEpisode e in episodes)
            {
                returneString.Add("<channel>");
                returneString.Add($"<item>");
                returneString.Add($"<itunes:episodeType>{e.EpisodeType}</itunes:episodeType>");
                if (e.EpisodeNumber != 0) returneString.Add($"<itunes:episode>{e.EpisodeNumber}</itunes:episode>");
                if (e.Season != 0) returneString.Add($"<itunes:season>{e.Season}</itunes:season>");
                returneString.Add($"<itunes:title>{e.Title}</itunes:title>");
                returneString.Add($"<description>{e.Description}</description>");
                if (e.ImageUrl != null) returneString.Add($"<itunes:image href =\"{e.ImageUrl}\"/>");
                if (e.Link != null) returneString.Add($"<link>{e.Link}</link>");
                returneString.Add($"<enclosure length =\"{ e.Length}\" type=\"{e.EpisodeType}\" url=\"{e.Url}\">");
                returneString.Add($"<guid>{e.Guide}</guid>");
                returneString.Add($"<pubDate>{e.Publicationdate}</pubDate>");
                returneString.Add($"<itunes:duration>{e.Duration}</itunes:duration>");
                returneString.Add($"<itunes:explicit>{e.ExplicitBool.ToString().ToLower()}</itunes:explicit>");
                returneString.Add($"</item>");
            }
            returneString.Add($"</channell>");
            returneString.Add($"</rss>");
            return View(returneString);
        }
    }
}
