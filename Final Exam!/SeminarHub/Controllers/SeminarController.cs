using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Models;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Controllers
{
	public class SeminarController : Controller
	{
		private readonly SeminarHubDbContext data;
		public SeminarController(SeminarHubDbContext context)
		{
			data = context;
		}
		public async Task<IActionResult> All()
		{
			var seminars = await data.Seminars
				.AsNoTracking()
				.Select(s => new AllSeminarsViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					Lecturer = s.Lecturer,
					Category = s.Category.Name,
					Organizer = s.Organizer.UserName,
					DateAndTime = s.DateAndTime.ToString(FormatDate),
				})
				.ToListAsync();

			return View(seminars);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new SeminarFormViewModel();
			model.Categories = await GetCategories();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(SeminarFormViewModel model)
		{
			DateTime date;
			string userId = GetUserId();

			if (!DateTime.TryParseExact(model.DateAndTime, FormatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
			{
				ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {FormatDate}");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await GetCategories();
				return View(model);
			}

			var entity = new Seminar()
			{
				Id = model.Id,
				Topic = model.Topic,
				Lecturer = model.Lecturer,
				Details = model.Details,
				DateAndTime = date,
				CategoryId = model.CategoryId,
				OrganizerId = userId,
			};

			if (model.Duration != null)
			{
				entity.Duration = model.Duration;
			}

			await data.AddAsync(entity);
			await data.SaveChangesAsync();

			return RedirectToAction("All");
		}

		[HttpGet]
		public async Task<IActionResult> Joined()
		{
			var userId = GetUserId();

			var seminars = await data.SeminarsParticipants
				.Where(s => s.ParticipantId == userId)
				.AsNoTracking()
				.Select(s => new AllSeminarsViewModel
				{
					Id = s.Seminar.Id,
					Topic = s.Seminar.Topic,
					Lecturer = s.Seminar.Lecturer,
					Category = s.Seminar.Category.Name,
					Organizer = s.Seminar.Organizer.UserName,
					DateAndTime = s.Seminar.DateAndTime.ToString(FormatDate),
				})
				.ToListAsync();

			return View(seminars);
		}


		public async Task<IActionResult> Join(int id)
		{
			var userId = GetUserId();

			bool alreadyAdded = await data.SeminarsParticipants
				.AnyAsync(b => b.SeminarId == id && b.ParticipantId == userId);

			var currentSeminar = await data.Seminars.FindAsync(id);

			if (!alreadyAdded)
			{
				var seminarParticipant = new SeminarParticipant()
				{
					ParticipantId = userId,
					SeminarId = currentSeminar.Id,
				};

				await data.SeminarsParticipants.AddAsync(seminarParticipant);
				await data.SaveChangesAsync();
				return RedirectToAction("Joined");
			}

			return RedirectToAction("All");
		}


		public async Task<IActionResult> Leave(int id)
		{
			string userId = GetUserId();

			var seminar = await data.Seminars
				.Where(e => e.Id == id)
				.Include(e => e.SeminarsParticipants)
				.FirstOrDefaultAsync();

			if (seminar == null)
			{
				return BadRequest();
			}

			var sp = seminar.SeminarsParticipants.FirstOrDefault(s => s.ParticipantId == userId);

			if (sp == null)
			{
				return BadRequest();
			}

			seminar.SeminarsParticipants.Remove(sp);

			await data.SaveChangesAsync();
			return RedirectToAction("Joined");
		}

		public async Task<IActionResult> Details(int id)
		{
			var model = await data.Seminars
				.Where(s => s.Id == id)
				.Select(s => new SeminarDetailsViewModel
				{
					Id = s.Id,
					DateAndTime = s.DateAndTime.ToString(FormatDate),
					Duration = s.Duration.Value,
					Lecturer = s.Lecturer,
					Category = s.Category.Name,
					Details = s.Details,
					Organizer = s.Organizer.UserName,
					Topic = s.Topic,
				})
				.FirstOrDefaultAsync();

			if (model == null)
			{
				return BadRequest();
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var task = await data.Seminars.FindAsync(id);

			if (task == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();

			if (task.OrganizerId != userId)
			{
				return Unauthorized();
			}

			var taskModel = new SeminarDeleteViewModel()
			{
				Id = task.Id,
				DateAndTime = task.DateAndTime,
				Topic = task.Topic
			};

			return View(taskModel);

		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var task = await data.Seminars.FindAsync(id);

			data.Seminars.Remove(task);
			await data.SaveChangesAsync();

			return RedirectToAction("All");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await data.Seminars.FindAsync(id);

			if (model == null)
			{
				return BadRequest();
			}

			var userId = GetUserId();

			if (userId != model.OrganizerId)
			{
				return Unauthorized();
			}

			var entity = new SeminarFormViewModel()
			{
				Id = model.Id,
				Topic = model.Topic,
				Lecturer = model.Lecturer,
				Details = model.Details,
				DateAndTime = model.DateAndTime.ToString(FormatDate),
				CategoryId = model.CategoryId,
				Duration = model.Duration

			};

			entity.Categories = await GetCategories();
			return View(entity);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
		{
			var seminar = await data.Seminars.FindAsync(id);
			string userId = GetUserId();

			if (seminar == null)
			{
				return BadRequest();
			}
			if (userId == null)
			{
				return Unauthorized();
			}

			DateTime date;

			if (!DateTime.TryParseExact(model.DateAndTime, FormatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
			{
				ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {FormatDate}");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await GetCategories();
				return View(model);
			}

			seminar.Id = id;
			seminar.Topic = model.Topic;
			seminar.Lecturer = model.Lecturer;
			seminar.CategoryId = model.CategoryId;
			seminar.Duration = model.Duration;
			seminar.Details = model.Details;
			seminar.DateAndTime = date;

			await data.SaveChangesAsync();

			return RedirectToAction("All");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}

		private async Task<IEnumerable<Category>> GetCategories()
		{
			return await data.Categories
				.AsNoTracking()
				.Select(t => new Category
				{
					Id = t.Id,
					Name = t.Name,
				})
				.ToListAsync();

		}
	}
}
