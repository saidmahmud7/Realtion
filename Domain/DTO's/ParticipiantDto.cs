namespace Domain.DTO_s;

public class ParticipiantDto
{
    public string Name { get; set; } 
    public string Email { get; set; }
    public string Role { get; set; } 
    public DateTime JoinedDate { get; set; }
    public int TeamId { get; set; }
}