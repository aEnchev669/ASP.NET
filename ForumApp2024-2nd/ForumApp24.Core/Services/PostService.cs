using ForumApp24.Core.Contacts;
using ForumApp24.Core.Models;
using ForumApp24.Infrastructure.Data;
using ForumApp24.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp24.Core.Services
{
	public class PostService : IPostService
	{
		private readonly ForumDBContext context;

		public PostService(ForumDBContext _context)
		{
			context = _context;
		}

        public async Task AddAsync(PostModel model)
        {
			var enity = new Post()
			{
				Title = model.Title,
				Content = model.Content,
			};

			await context.AddAsync(enity);
			await context.SaveChangesAsync();
        }

		public async Task DeleteAsync(int id)
		{
			var entity = await GetEntityByIdAsync(id);

			context.Remove(entity);
			context.SaveChanges();
		}

		public async Task EditAsync(PostModel model)
        {
            var entity = await GetEntityByIdAsync(model.Id);

			entity.Title = model.Title;
			entity.Content = model.Content;

			await context.SaveChangesAsync();	
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
		{
			return await context.Posts
				.Select(p => new PostModel()
				{
					Id = p.Id,
					Content = p.Content,
					Title = p.Title,
				})
				.AsNoTracking()
				.ToListAsync();
		}

        public async Task<PostModel?> GetByIdAsync(int id)
        {
			return await context.Posts
				.Where(p => p.Id == id)
				.Select(p => new PostModel()
				{
					Id = p.Id,
					Content = p.Content,
					Title = p.Title,
				})
				.AsNoTracking()
				.FirstOrDefaultAsync();

        }

		private async Task<Post> GetEntityByIdAsync(int id)
		{
			var entity = await context.FindAsync<Post>(id);

			return entity;
		}
    }
}
