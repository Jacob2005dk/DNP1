using DNP1;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts = new List<Post>();

    public PostInMemoryRepository()
    {
        posts.Add(new Post {Title = "My stomach hurts!", Body = "I dont know why??", Id = 1, UserId = 1});
        posts.Add(new Post {Title = "My brain hurts!", Body = "I want to know why!!", Id = 2, UserId = 2});
        posts.Add(new Post {Title = "My finger hurts!", Body = "I like it", Id = 3, UserId = 3});
        posts.Add(new Post {Title = "My teeth hurts!", Body = "I hate toothpaste", Id = 4, UserId = 4});
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }
    
    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Post> GetSingleAsync(int id)
    {
        Post? postToFind = posts.SingleOrDefault(p => p.Id == id);
        if (postToFind is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return Task.FromResult(postToFind);
    }
    
    public IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }
}