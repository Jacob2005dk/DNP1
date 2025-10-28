using ApiContracts;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(PostDto request);
    
    public Task<List<PostDto>> GetAllPostsAsync();
}