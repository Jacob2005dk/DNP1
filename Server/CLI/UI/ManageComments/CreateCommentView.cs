using DNP1;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    private async Task AddCommentAsync(string body, int userId, int postId)
    {
        Post post = await postRepository.GetSingleAsync(postId);

        Comment comment = new Comment()
        {
            Body = body,
            UserId = userId,
            PostId = postId
        };
        Comment created = await commentRepository.AddAsync(comment);
        Console.WriteLine($"Comment for {created.PostId} has been added");
    }
}