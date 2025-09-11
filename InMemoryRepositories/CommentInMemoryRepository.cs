using DNP1;
using Entities;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    List<Comment> comments = new List<Comment>();

    public CommentInMemoryRepository()
    {
        comments.Add(new  Comment {Body = "What da helly??", Id = 1, PostId = 1, UserId = 1});
        comments.Add(new  Comment {Body = "What da helly??", Id = 2, PostId = 2, UserId = 2});
        comments.Add(new  Comment {Body = "What da helly??", Id = 3, PostId = 3, UserId = 3});
        comments.Add(new  Comment {Body = "What da helly??", Id = 4, PostId = 4, UserId = 4});
    }
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(p => p.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }
    
    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? commentToFind = comments.SingleOrDefault(p => p.Id == id);
        if (commentToFind is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        return Task.FromResult(commentToFind);
    }
    
    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }
}