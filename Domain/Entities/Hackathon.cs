namespace Domain.Entities;

public class Hackathon
{
    public int Id { get; set; }
  
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Theme { get; set; }
    //navigation
    public List<Team> Teams { get; set; } = [];
}