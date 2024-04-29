using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IFuelRefillService
    {
        Task<IEnumerable<FuelRefill>> GetAllFuelRefillsAsync();
        Task<FuelRefill> GetFuelRefillByIdAsync(int id);
        Task<bool> AddFuelRefillAsync(FuelRefill fuelRefill);
        User? GetByNic(string nic);
        Vehicle? GetByRegNo(string regNo);
        Task<bool> UpdateFuelRefillAsync(int id, FuelRefill fuelRefill);
        Task<bool> DeleteFuelRefillAsync(int id);
    }
}