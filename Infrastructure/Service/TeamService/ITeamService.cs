using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Service.TeamService;

public interface ITeamService
{
    Task<ApiResponse<List<TeamDto>>> GetAll();
    Task<ApiResponse<Team>> GetById(int id);
    Task<ApiResponse<string>> Add(TeamDto team);
    Task<ApiResponse<string>> Update(Team team);
    Task<ApiResponse<string>> Delete(int id);
}