using DNP1;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    private async Task AddPostAsync(string title, string body, int userId)
    {
        Post post = new Post()
        {
            Title = title,
            Body = body,
            UserId = userId
        };
        Post created = await postRepository.AddAsync(post);
        Console.WriteLine(created + " has been created");
    }
}