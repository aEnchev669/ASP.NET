using ForumApp24.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp24.Core.Contacts
{
	public interface IPostService
	{
        Task AddAsync(PostModel model);
		Task DeleteAsync(int id);
		Task EditAsync(PostModel model);
        Task<IEnumerable<PostModel>> GetAllPostsAsync();
        Task<PostModel?> GetByIdAsync(int id);
    }
}
