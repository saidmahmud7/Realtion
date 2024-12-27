namespace Domain.DTO_s;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public int HackathonId { get; set; }

    public List<ParticipiantDto>? Participants { get; set; }
}