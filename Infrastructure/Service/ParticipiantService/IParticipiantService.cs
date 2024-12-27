using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Service.ParticipiantService;

public interface IParticipiantService
{
    Task<ApiResponse<List<ParticipiantDto>>> GetAll();
    Task<ApiResponse<Participant>> GetById(int id);
    Task<ApiResponse<string>> Add(ParticipiantDto participiant);
    Task<ApiResponse<string>> Update(Participant participant);
    Task<ApiResponse<string>> Delete(int id);
}