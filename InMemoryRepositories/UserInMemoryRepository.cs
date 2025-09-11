using DNP1;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users = new List<User>();

    public UserInMemoryRepository()
    {
        users.Add(new  User {Id = 1, UserName = "Jacob",  Password = "123"});
        users.Add(new  User {Id = 2, UserName = "Mustafa",  Password = "345"});
        users.Add(new  User {Id = 3, UserName = "Younas",  Password = "567"});
        users.Add(new  User {Id = 4, UserName = "Alex",  Password = "789"});
    }
    
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(p => p.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }
    
    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        User? postToRemove = users.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<User> GetSingleAsync(int id)
    {
        User? postToFind = users.SingleOrDefault(p => p.Id == id);
        if (postToFind is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        return Task.FromResult(postToFind);
    }
    
    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }
}