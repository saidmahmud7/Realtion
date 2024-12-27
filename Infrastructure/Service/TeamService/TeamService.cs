using System.Net;
using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.TeamService;

public class TeamService(DataContext context) : ITeamService
{
    public async Task<ApiResponse<List<TeamDto>>> GetAll()
    {
        var teams = await context.Teams.Select(t => new TeamDto()
        {
            Name = t.Name,
            HackathonId = t.HackathonId,
            CreatedDate = t.CreatedDate,
            Participants = t.Participants.Select(p => new ParticipiantDto()
            {
                Name = p.Name,
                Email = p.Email,
                Role = p.Role
            }).ToList()
        }).ToListAsync();

        return new ApiResponse<List<TeamDto>>(teams);
    }

    public async Task<ApiResponse<Team>> GetById(int id)
    {
        var teams = await context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        return teams == null
            ? new ApiResponse<Team>(HttpStatusCode.NotFound, "Team NOt Found")
            : new ApiResponse<Team>(teams);
    }

    public async Task<ApiResponse<string>> Add(TeamDto teams)
    {
        var team = new Team()
        {
            Name = teams.Name,
            CreatedDate = teams.CreatedDate,
            HackathonId = teams.HackathonId
        };
        await context.Teams.AddAsync(team);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Team not Created")
            : new ApiResponse<string>("Created SuccesFuly");
    }

    public async Task<ApiResponse<string>> Update(Team team)
    {
        var existingTeam = await context.Teams.FirstOrDefaultAsync(h => h.Id == team.Id);

        if (existingTeam == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Team not found");
        }

        existingTeam.Name = team.Name;
        existingTeam.CreatedDate = team.CreatedDate;
        existingTeam.HackathonId = team.HackathonId;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Team not updated")
            : new ApiResponse<string>("Team updated successfully");
    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        var existingTeam = await context.Teams.FirstOrDefaultAsync(h => h.Id == id);

        if (existingTeam == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Team not found");
        }

        context.Remove(existingTeam);
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Team not deleted")
            : new ApiResponse<string>("Team deleted successfully");
    }
}