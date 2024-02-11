﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop__ASP.NET_Core_Identity.Data;
using Workshop__ASP.NET_Core_Identity.Models;

namespace Workshop__ASP.NET_Core_Identity.Controllers
{
	[Authorize]
	public class BoardController : Controller
	{

		private readonly TaskBoardAppDbContext data;

        public BoardController(TaskBoardAppDbContext context)
        {
            data = context;
        }
        public async Task<IActionResult> Index()
		{
			var boards = await data.Boards
				.Select(b => new BoardViewModel()
				{
					Id = b.Id,
					Name = b.Name,
					Tasks = b.Tasks.Select(t => new TaskViewModel()
					{
						Id = t.Id,
						Description = t.Description,
						Owner = t.Owner.UserName,
						Title = t.Title
					})
				})
				.ToListAsync();

			return View(boards);
		}
	}
}
