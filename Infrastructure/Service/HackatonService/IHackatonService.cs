using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Service.HackatonService;

public interface IHackatonService
{
    Task<ApiResponse<List<HackatonDto>>> GetAll();
    Task<ApiResponse<Hackathon>> GetById(int id);
    Task<ApiResponse<string>> Add(HackatonDto hackathon);
    Task<ApiResponse<string>> Update(Hackathon hackathon);
    Task<ApiResponse<string>> Delete(int id);
}