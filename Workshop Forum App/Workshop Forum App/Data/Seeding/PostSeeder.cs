
namespace Workshop_Forum_App.Data.Seeding
{
    using Models;
    class PostSeeder
    {
        internal Post[] GeneratePosts()
        {

            ICollection<Post> posts = new HashSet<Post>();
            Post currentPost;

            currentPost = new Post()
            {
                Title = "My first post",
                Content = "Lorem ipsum dolor sit amet,"
            };
            posts.Add(currentPost);

            currentPost = new Post()
            {
                Title = "My second post",
                Content = "2 - Lorem ipsum dolor sit amet,"
            };
            posts.Add(currentPost);

            currentPost = new Post()
            {
                Title = "My third post",
                Content = "3 - Lorem ipsum dolor sit amet,"
            };
            posts.Add(currentPost);

            return posts.ToArray();
        }


    }
}
