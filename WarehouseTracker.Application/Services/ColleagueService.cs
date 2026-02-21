using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services;

public class ColleagueService : IColleagueService
{
    private readonly IColleagueRepository _colleagueRepository;

    public ColleagueService(IColleagueRepository colleagueRepository)
    {
        _colleagueRepository = colleagueRepository;
    }

    public async Task<Colleague> GetColleagueById(string colleagueId)
    {
        var coll = await _colleagueRepository.GetByIdAsync(colleagueId);
        if (coll == null) throw new Exception("Colleague does not exist");
        return coll;
    }

    public async Task RegisterColleagueAsync(Colleague colleague)
    {
        var existing = await _colleagueRepository.GetByIdAsync(
            colleague.ColleagueId
        );
        if (existing != null) throw new Exception("Colleague Exists");

        await _colleagueRepository.AddAsync(colleague);
        await _colleagueRepository.SaveChangesAsync();
    }

    public async Task<List<Colleague>> RetrieveColleagueAsync()
    {
        return await _colleagueRepository.GetAll();
    }
}