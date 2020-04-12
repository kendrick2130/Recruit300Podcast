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
    public class PodcastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PodcastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Podcasts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Podcasts.ToListAsync());
        }

        // GET: Podcasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcasts = await _context.Podcasts
                .FirstOrDefaultAsync(m => m.PodcastId == id);
            if (podcasts == null)
            {
                return NotFound();
            }

            return View(podcasts);
        }

        // GET: Podcasts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Podcasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PodcastId,Title,Link,Language,Copyright,Author,Description,Type,OwnerName,OwnerEmail,Image,Category,EmplicitBool")] Podcasts podcasts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(podcasts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(podcasts);
        }

        // GET: Podcasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcasts = await _context.Podcasts.FindAsync(id);
            if (podcasts == null)
            {
                return NotFound();
            }
            return View(podcasts);
        }

        // POST: Podcasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PodcastId,Title,Link,Language,Copyright,Author,Description,Type,OwnerName,OwnerEmail,Image,Category,EmplicitBool")] Podcasts podcasts)
        {
            if (id != podcasts.PodcastId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podcasts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodcastsExists(podcasts.PodcastId))
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
            return View(podcasts);
        }

        // GET: Podcasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcasts = await _context.Podcasts
                .FirstOrDefaultAsync(m => m.PodcastId == id);
            if (podcasts == null)
            {
                return NotFound();
            }

            return View(podcasts);
        }

        // POST: Podcasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podcasts = await _context.Podcasts.FindAsync(id);
            _context.Podcasts.Remove(podcasts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodcastsExists(int id)
        {
            return _context.Podcasts.Any(e => e.PodcastId == id);
        }
        public IActionResult PodcastRSSFeed()
        {
            IEnumerable<Podcasts> podcasts = _context.Podcasts.Include(p => p.PodcastEpisodes).ToList();
            List<string> returnString = new List<string>();
            returnString.Add("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>");
            returnString.Add("<rss version=\"2.0\" xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\">");

            foreach (Podcasts p in podcasts)
            {
                IEnumerable<PodcastEpisode> episodes = p.PodcastEpisodes;
                returnString.Add("<channel>");
                returnString.Add($"<title>{p.Title}</title>");
                returnString.Add($"<link>{p.Link}</link>");
                returnString.Add($"<language>{p.Language}</language>");
                returnString.Add($"<copyright>{p.Copyright}</copyright>");
                returnString.Add($"<itunes:author>{p.Author}</itunes:author>");
                returnString.Add($"<description>{p.Description}</description>");
                returnString.Add($"<itunes:owner>");
                returnString.Add($"<itunes:name>{p.OwnerName}</itunes:name>");
                returnString.Add($"<itunes:email>{p.OwnerEmail}</itunes:email>");
                returnString.Add($"</itunes:owner>");
                returnString.Add($"<itunes:image href =\"{p.Image}\"/>");
                string[] category = p.Category.Split(", ");
                for (int i = 0; i < category.Length; i++)
                {
                    returnString.Add($" <itunes:category text=\"{category[i]}\"" + ((i > 0) ? " /" : "") + ">");
                }
                returnString.Add($"</itunes:category>");
                returnString.Add($"<itunes:explicit>{p.ExplicitBool.ToString().ToLower()}</itunes:explicit>");
                returnString.Add($"</channell>");
            }
            returnString.Add($"</rss>");
            return View(returnString);
        }
    }
}
