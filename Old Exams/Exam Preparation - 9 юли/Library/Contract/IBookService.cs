using Library.Models;

namespace Library.Contract
{
	public interface IBookService
	{
		Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
		Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);
		Task<BookViewModel?> GetBookByIdAsync(int id);
		Task AddBookToCollectionAsync(string userId, BookViewModel book);

	}
}
