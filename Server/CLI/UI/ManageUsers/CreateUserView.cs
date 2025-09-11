using DNP1;
using Entities;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    private async Task AddUserAsync(string name, string password)
    {
        User user = new User()
        {
            UserName = name,
            Password = password,
        };
        User created = await userRepository.AddAsync(user);
        Console.WriteLine($"User {created.Id} created");
    }
}