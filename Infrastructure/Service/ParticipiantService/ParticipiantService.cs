using System.Net;
using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.ParticipiantService;

public class ParticipiantService(DataContext context) : IParticipiantService
{
    public async Task<ApiResponse<List<ParticipiantDto>>> GetAll()
    {
        var participiant = await context.Participants.Select(p => new ParticipiantDto()
        {
            Name = p.Name,
            Email = p.Email,
            Role = p.Role
        }).ToListAsync();
        return new ApiResponse<List<ParticipiantDto>>(participiant);
    }

    public async Task<ApiResponse<Participant>> GetById(int id)
    {
        var participiant = await context.Participants.FirstOrDefaultAsync(t => t.Id == id);
        return participiant == null
            ? new ApiResponse<Participant>(HttpStatusCode.NotFound, "Participiant Not Found")
            : new ApiResponse<Participant>(participiant);
    }

    public async Task<ApiResponse<string>> Add(ParticipiantDto participiant)
    {
        var participiants = new Participant()
        {
            Name = participiant.Name,
            Email = participiant.Email,
            Role = participiant.Role,
            TeamId = participiant.TeamId
        };
        await context.Participants.AddAsync(participiants);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Participinat not Created")
            : new ApiResponse<string>("Created SuccesFuly");
    }

    public async Task<ApiResponse<string>> Update(Participant participant)
    {
        var existingParticipinat = await context.Participants.FirstOrDefaultAsync(h => h.Id == participant.Id);

        if (existingParticipinat == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Participinat not found");
        }

        existingParticipinat.Name = participant.Name;
        existingParticipinat.Email = participant.Email;
        existingParticipinat.Role = participant.Role;
        existingParticipinat.TeamId = participant.TeamId;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Participinat not updated")
            : new ApiResponse<string>("Participinat updated successfully");    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        var existingParticipinat = await context.Participants.FirstOrDefaultAsync(h => h.Id == id);

        if (existingParticipinat == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Participinat not found");
        }

        context.Remove(existingParticipinat);
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Participinat not deleted")
            : new ApiResponse<string>("Participinat deleted successfully");    }
}