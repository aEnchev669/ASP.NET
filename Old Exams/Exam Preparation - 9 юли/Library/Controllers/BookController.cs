using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
	[Authorize]
	public class BookController : Controller
	{
		private readonly LibraryDbContext data;
		public BookController(LibraryDbContext context)
		{
			data = context;
		}
		public async Task<IActionResult> All()
		{
			var events = await data.Books
				.AsNoTracking()
				.Select(b => new AllBookViewModel
				{
					Id = b.Id,
					Title = b.Title,
					Author = b.Author,
					ImageUrl = b.ImageUrl,
					Rating = b.Rating,
					Category = b.Category.Name
				})
				.ToListAsync();

			return View(events);
		}

		[HttpGet]

		public async Task<IActionResult> Mine()
		{
			var userId = GetUserId();
			var model =await data.IdentityUsers
				.Where(iu => iu.CollectorId == userId)
				.AsNoTracking()
				.Select(b => new AllBookViewModel
				{
					Id = b.BookId,
					Title = b.Book.Title,
					Author = b.Book.Author,
					ImageUrl = b.Book.ImageUrl,
					Description = b.Book.Description,
					Category = b.Book.Category.Name
				})
				.ToListAsync();

			return View(model);
		}

        public async Task<IActionResult> RemoveFromCollection(int Id)
		{
            string userId = GetUserId();

            var b = await data.Books
                .Where(e => e.Id == Id)
                .Include(e => e.UsersBooks)
                .FirstOrDefaultAsync();

            if (b == null)
            {
                return BadRequest();
            }

			var ub = b.UsersBooks.FirstOrDefault(u => u.CollectorId == userId);

			if (ub != null)
			{
				b.UsersBooks.Remove(ub);
				 await data.SaveChangesAsync();	

				return RedirectToAction("Mine");
			}

			return BadRequest();
        }

        [HttpPost]
		public async Task<IActionResult> AddToCollection( BookViewModel givenBook)
		{
			var book = await data.Books
			   .Select(b => new AllBookViewModel
			   {
				   Id = b.Id,
				   Title = b.Title,
				   Author = b.Author,
				   ImageUrl = b.ImageUrl,
				   Rating = b.Rating,
				   Category = b.Category.Name
			   })
			   .ToListAsync();

			if (book == null)
			{
				return BadRequest();
			}


			var id = GetUserId();

			bool alreadyAdded = await data.IdentityUsers
				.AnyAsync(b => b.CollectorId == id && b.BookId == givenBook.Id);

			if (!alreadyAdded)
			{
				var userBook = new IdentityUserBook()
				{
					CollectorId = id,
					BookId = givenBook.Id
				};

				await data.IdentityUsers.AddAsync(userBook);
				await data.SaveChangesAsync();
				return RedirectToAction("All");

			}

			return RedirectToAction("All");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
