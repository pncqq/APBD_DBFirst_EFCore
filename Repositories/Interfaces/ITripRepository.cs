using WebApplication1.DAL;
using WebApplication1.Enums;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Interfaces;

public interface ITripRepository
{
    public Task<List<TripModel>> GetTripsSortedAsync();
    public Task<DatabaseErrors> DeleteClientAsync(int idClient);
    public Task<bool> AddClientToTripAsync(AddClientToTripDTO data);
}