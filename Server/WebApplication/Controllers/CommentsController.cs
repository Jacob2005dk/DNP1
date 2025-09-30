using DNP1;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }
    
    [HttpPost]
    public async Task<ActionResult<Comment>> AddComment([FromBody] Comment comment)
    {
        try
        {
            var created = await commentRepo.AddAsync(comment);
            Comment createdComment = new()
            {
                Id = created.Id,
                Body = created.Body,
                UserId = created.UserId,
                PostId = created.PostId
            };
            return Created($"Comments/{created.Id}", createdComment);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Comment>> UpdateComment([FromBody] Comment comment)
    { 
        await commentRepo.UpdateAsync(comment);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult<Comment>> DeleteComment(int id)
    {
        try
        {
            await commentRepo.DeleteAsync(id); 
            return NoContent(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Comment>> GetSingleComment(int id)
    {
        try
        {
            return await commentRepo.GetSingleAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Comment>>> GetAllComments(
        [FromQuery] int? postId,
        [FromQuery] int? userId)
    {
        try
        {
            var comments = commentRepo.GetManyAsync();

            if (postId.HasValue)
                comments = comments.Where(p => p.PostId == postId.Value);

            if (userId.HasValue)
                comments = comments.Where(p => p.UserId == userId.Value);

            var result = comments.ToList();
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}