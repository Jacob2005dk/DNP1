using DNP1;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;

    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    private async Task GetSinglePost(int id)
    {
        Post post = await postRepository.GetSingleAsync(id);
        Console.WriteLine($"Post ID: {post.Id} \n Title: {post.Title}");
    }
}