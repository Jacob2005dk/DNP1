namespace ApiContracts;

public class UpdateUserDto
{
    public int Id { get;}
    public required string UserName { get; set; }
    public required string Password { get; set; }
}