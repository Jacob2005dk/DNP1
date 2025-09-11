using System.Collections.Immutable;
using System.Runtime.InteropServices.ComTypes;
using DNP1;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    private async Task GetAllPostsAsync()
    {
        List<Post> post = postRepository.GetManyAsync().ToList();
        if (post.Count == 0)
        {
            Console.WriteLine("No posts found");
        }
        else
        {
            Console.WriteLine("Post overview:");
            foreach (var posts in post)
            {
                Console.WriteLine($"Post Id: {posts.Id} Title: {posts.Title} ");
            }
            await Task.CompletedTask;
        }
    }
}