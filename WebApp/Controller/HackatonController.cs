using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Service.HackatonService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class HackatonController(IHackatonService service)
{
    [HttpGet]
    public async Task<ApiResponse<List<HackatonDto>>> GetAll() => await service.GetAll();

    [HttpGet("{id}")]
    public async Task<ApiResponse<Hackathon>> GetById(int id) => await service.GetById(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create(HackatonDto hackaton) => await service.Add(hackaton);

    [HttpPut]
    public async Task<ApiResponse<string>> Update(Hackathon hackathon) => await service.Update(hackathon);

    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.Delete(id);
}