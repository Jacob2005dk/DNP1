using DNP1;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;
[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
    {
        try
        {
            var created = await postRepo.AddAsync(post);
            Post createdPost = new()
            {
                Id = created.Id,
                Title = created.Title,
                Body = created.Body,
                UserId = created.UserId
            };
            return Created($"Posts/{created.Id}", createdPost);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Post>> UpdatePost([FromBody] Post post)
    {
        try
        {
            await postRepo.UpdateAsync(post);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<Post>> DeletePost(int id)
    {
        try
        {
            await postRepo.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Post>> GetSinglePost(int id)
    {
        try
        {
            return await postRepo.GetSingleAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetAllPosts(
        [FromQuery] string? title,
        [FromQuery] int? userId)
    {
        try
        {
            var posts = postRepo.GetManyAsync();

            if (!string.IsNullOrWhiteSpace(title))
                posts = posts.Where(p => p.Title.Contains(title));

            if (userId.HasValue)
                posts = posts.Where(p => p.UserId == userId.Value);

            var result = posts.ToList();
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}