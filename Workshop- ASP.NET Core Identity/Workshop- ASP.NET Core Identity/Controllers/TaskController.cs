using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Workshop__ASP.NET_Core_Identity.Data;
using Workshop__ASP.NET_Core_Identity.Models;

namespace Workshop__ASP.NET_Core_Identity.Controllers
{
	public class TaskController : Controller
	{
		private readonly TaskBoardAppDbContext data;
        public TaskController(TaskBoardAppDbContext _context)
        {
            data = _context;
        }
		[HttpGet]
        public async Task<IActionResult> Create()
		{
			var model = new TaskFormViewModel();
			model.Boards = await GetBoards();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(TaskFormViewModel model)
		{
			if (!(await GetBoards()).Any(b => b.Id == model.BoardId))
			{
				ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
			}

			if (!ModelState.IsValid)
			{
				model.Boards = await GetBoards();

				return View(model);
			}

			var entity = new Workshop__ASP.NET_Core_Identity.Data.Task()
			{
				BoardId = model.BoardId,
				CreatedOn = DateTime.Now,
				Description = model.Description,
				OwnerId = GetUserId(),
				Title = model.Title,
			};

			await data.AddAsync(entity);
			await data.SaveChangesAsync();

			return RedirectToAction("Index", "Board");
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var task = await data.Tasks
				.Where(t => t.Id == id)
				.Select(t => new TaskDetailsViewModel()
				{
					Board = t.Board.Name,
					Description = t.Description,
					Id = t.Id,
					CreatedOn = t.CreatedOn.Value.ToString("dd.MM.yyyy HH:mm"),
					Owner = t.Owner.UserName,
					Title = t.Title,
				})
				.FirstOrDefaultAsync();

			return View(task);
		}

		private string GetUserId()
		{
			return User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		private async Task<IEnumerable<TaskBoardViewModel>> GetBoards()
		{
			return await data.Boards
				.Select(x => new TaskBoardViewModel
				{
					Id = x.Id,
					Name = x.Name,
				})
				.ToListAsync();
		}
	}
}
