using ApiContracts;
using DNP1;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    private async Task VerifyUserNameIsAvailableAsync(string userName)
    {
        bool user = userRepo.GetManyAsync().Any(u => u.UserName == userName);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        try
        {
            await VerifyUserNameIsAvailableAsync(request.UserName);

            User user = new()
            {
                UserName = request.UserName,
                Password = request.Password,
            };
            User created = await userRepo.AddAsync(user);
            UserDto dto = new()
            {
                Id = created.Id,
                UserName = created.UserName
            };
            return Created($"/Users/{dto.Id}", created);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser([FromBody] User request)
    {
        try
        {
            await VerifyUserNameIsAvailableAsync(request.UserName);
            await userRepo.UpdateAsync(request);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        try
        {
            await userRepo.DeleteAsync(id);
            return NoContent();
        } 
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetSingleAsync(int id)
    {
        try
        {
            return await userRepo.GetSingleAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetManyAsync(
        [FromQuery] string? userName,
        [FromQuery] int? id)
    {
        try
        {
            var users = userRepo.GetManyAsync();

            if (!string.IsNullOrWhiteSpace(userName))
                users = users.Where(p => p.UserName.Contains(userName));

            if (id.HasValue)
                users = users.Where(p => p.Id == id.Value);

            var result = users.ToList();
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}