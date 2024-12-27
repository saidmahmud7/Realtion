using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Service.ParticipiantService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class ParticipinatController(IParticipiantService service)
{
    [HttpGet]
    public async Task<ApiResponse<List<ParticipiantDto>>> GetAll() => await service.GetAll();

    [HttpGet("{id}")]
    public async Task<ApiResponse<Participant>> GetById(int id) => await service.GetById(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create(ParticipiantDto participiant) => await service.Add(participiant);

    [HttpPut]
    public async Task<ApiResponse<string>> Update(Participant participiant) => await service.Update(participiant);

    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.Delete(id);
}