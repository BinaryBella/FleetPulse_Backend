using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IFuelRefillService
    {
        Task<IEnumerable<FuelRefill>> GetAllFuelRefillsAsync();
        Task<FuelRefill> GetFuelRefillByIdAsync(int id);
        Task<FuelRefill> AddFuelRefillAsync(FuelRefill fuelRefill);
        Task<bool> UpdateFuelRefillAsync(int id, FuelRefill fuelRefill);
        Task<bool> DeleteFuelRefillAsync(int id);
    }
}