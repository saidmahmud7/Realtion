using System.Net;
using Domain.DTO_s;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.HackatonService;

public class HackatonService(DataContext context) : IHackatonService
{
    public async Task<ApiResponse<List<HackatonDto>>> GetAll()
    {
        var hackatons = await context.Hackathons
            .Include(h => h.Teams)
            .ToListAsync();
        var hackatonDto = hackatons.Select(h => new HackatonDto()
        {
            Name = h.Name,
            Theme = h.Theme,
            Date = h.Date,
            Teams = h.Teams.Select(t => new TeamDto()
            {
                Id = t.Id,
                Name = t.Name,
                CreatedDate = t.CreatedDate
            }).ToList()
        }).ToList();
        return new ApiResponse<List<HackatonDto>>(hackatonDto);
    }

    public async Task<ApiResponse<Hackathon>> GetById(int id)
    {
        var hackaton = await context.Hackathons.FirstOrDefaultAsync(h => h.Id == id);
        return hackaton == null
            ? new ApiResponse<Hackathon>(HttpStatusCode.NotFound, "Hackaton Not Found")
            : new ApiResponse<Hackathon>(hackaton);
    }

    public async Task<ApiResponse<string>> Add(HackatonDto hackathon)
    {
        var hackaton = new Hackathon()
        {
            Name = hackathon.Name,
            Theme = hackathon.Theme
        };
        await context.Hackathons.AddAsync(hackaton);
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Hackaton not created")
            : new ApiResponse<string>("Hackaton created successfully");
    }

    public async Task<ApiResponse<string>> Update(Hackathon hackathon)
    {
        var existingHackaton = await context.Hackathons.FirstOrDefaultAsync(h => h.Id == hackathon.Id);

        if (existingHackaton == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Hackaton not found");
        }

        existingHackaton.Name = hackathon.Name;
        existingHackaton.Theme = hackathon.Theme;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Hackaton not updated")
            : new ApiResponse<string>("Hackaton updated successfully");
    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        var existingHackaton = await context.Hackathons.FirstOrDefaultAsync(h => h.Id == id);

        if (existingHackaton == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Hackaton not found");
        }

        context.Remove(existingHackaton);
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Hackaton not deleted")
            : new ApiResponse<string>("Hackaton deleted successfully");
    }
}