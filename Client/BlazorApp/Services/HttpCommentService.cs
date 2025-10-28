using ApiContracts;

namespace BlazorApp.Services;

public class HttpCommentService  : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CommentDto> AddCommentToPost(CommentDto request)
    {
        
    }
}