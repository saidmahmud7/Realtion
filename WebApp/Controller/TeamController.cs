using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Service.TeamService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;



[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService service)
{
    [HttpGet]
    public async Task<ApiResponse<List<TeamDto>>> GetAll() => await service.GetAll();

    [HttpGet("{id}")]
    public async Task<ApiResponse<Team>> GetById(int id) => await service.GetById(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create(TeamDto team) => await service.Add(team);

    [HttpPut]
    public async Task<ApiResponse<string>> Update(Team team) => await service.Update(team);

    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.Delete(id);
}