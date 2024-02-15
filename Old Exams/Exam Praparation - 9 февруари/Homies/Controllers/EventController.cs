using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using Type = Homies.Data.Type;
using static Homies.Data.DataConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext data;
        public EventController(HomiesDbContext context)
        {
            data = context;
        }
        public async Task<IActionResult> All()
        {
            var events = await data.Events
                .AsNoTracking()
                .Select(e => new EventInfoViewModel(
                    e.Id,
                    e.Name,
                    e.Start,
                    e.Type.Name,
                    e.Organiser.UserName
                    ))
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await data.EventParticipants
                .Where(e => e.HelperId == userId)
                .AsNoTracking()
                .Select(ep => new EventInfoViewModel(
                    ep.EventId,
                    ep.Event.Name,
                    ep.Event.Start,
                    ep.Event.Type.Name,
                    ep.Event.Organiser.UserName
                    ))
                .ToListAsync();

            return View(model);
        }

        


        [HttpPost]
        public async Task<IActionResult> Join(int Id)
        {
            var e = await data.Events
                .Where(e => e.Id == Id)
                .Include(e => e.EventParticipants)
                .FirstOrDefaultAsync();


            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!e.EventParticipants.Any(e => e.HelperId == userId))
            {
                e.EventParticipants.Add(new EventParticipant()
                {
                    EventId = e.Id,
                    HelperId = userId
                });

                await data.SaveChangesAsync();
            }

            return RedirectToAction("Joined");
        }

        public async Task<IActionResult> Leave(int Id)
        {
            string userId = GetUserId();

            var e = await data.Events
                .Where(e => e.Id == Id)
                .Include(e => e.EventParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            var ep = e.EventParticipants.FirstOrDefault(e => e.HelperId == userId);

            if (ep != null)
            {
                e.EventParticipants.Remove(ep);

                await data.SaveChangesAsync();

                return RedirectToAction("All");
            }

            return BadRequest();

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel();
            model.Types = await GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            DateTime start;
            DateTime end;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {DateFormat}");
            }

            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();
                return View(model);
            }

            var entity = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                OrganiserId = GetUserId(),
                Start = start,
                End = end,
                TypeId = model.TypeId,

            };

            await data.Events.AddAsync(entity);
            await data.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await data.Events
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new EventFormViewModel()
            {
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString(DateFormat),
                End = e.End.ToString(DateFormat),
                TypeId = e.TypeId,
            };

            model.Types = await GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {

            var e = await data.Events
               .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime start;
            DateTime end;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {DateFormat}");
            }

            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();
                return View(model);
            }

            e.Name = model.Name;
            e.Start = start;
            e.End = end;
            e.Description = model.Description;
            e.OrganiserId = GetUserId();
            e.TypeId = model.TypeId;

            await data.SaveChangesAsync();

            return RedirectToAction("All");


        }
       
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Description = e.Description,
                    Start = e.Start.ToString(DateFormat),
                    End = e.End.ToString(DateFormat),
                    Organiser = e.Organiser.Email,
                    CreatedOn = e.CreatedOn.ToString(DateFormat),
                    Type = e.Type.Name
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<Type>> GetTypes()
        {
            return await data.Types
                .AsNoTracking()
                .Select(t => new Type
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToListAsync();

        }
    }
}
