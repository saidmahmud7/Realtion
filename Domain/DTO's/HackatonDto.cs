namespace Domain.DTO_s;

public class HackatonDto
{
    public string Name { get; set; } 
    public string? Theme { get; set; } 
    public DateTime Date { get; set; }
    public List<TeamDto>? Teams { get; set; }
}