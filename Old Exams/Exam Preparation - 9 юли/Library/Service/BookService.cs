using Library.Contract;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
	public class BookService : IBookService
	{
		private readonly LibraryDbContext data;
		public BookService(LibraryDbContext context)
		{
			data = context;
		}

		public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
		{
				bool alreadyAdded = await data.IdentityUsers
					.AnyAsync(b => b.CollectorId == userId && b.BookId == book.Id);

				if (!alreadyAdded)
				{
					var userBook = new IdentityUserBook
					{
						CollectorId = userId,
						BookId = book.Id,
					};

					await data.IdentityUsers.AddAsync(userBook);
					await data.SaveChangesAsync();
				}
		}

		public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
		{
			return await data
			   .Books
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
		}

		public async Task<BookViewModel?> GetBookByIdAsync(int id)
		{
			return await data.Books
				.Where(b => b.Id == id)
				.Select(b => new BookViewModel
				{
					Id = b.Id,
					Title = b.Title,
					Author = b.Author,
					ImageUrl = b.ImageUrl,
					Rating = b.Rating,
					Description = b.Description,
					CategoryId = b.CategoryId,
				})
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId)
		{
			return await data.IdentityUsers
				.Where(iu => iu.CollectorId == userId)
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
		}
	}
}
