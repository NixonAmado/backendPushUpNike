namespace API.Dtos;

public partial class G_PersonDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public int UserId { get; set; }

}
