using Forum.Services.Interfaces;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;

namespace Workshop_Forum_App.Controllers
{
    public class PostController
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<PostListViewModel> allPosts = 
                await this.postService.ListAllAsync();

            return View(allPosts);
        }
    }
}
