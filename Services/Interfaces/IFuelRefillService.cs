using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IFuelRefillService
    {
        Task<IEnumerable<FuelRefill?>> GetAllFuelRefillsAsync();
        Task<FuelRefill?> GetFuelRefillByIdAsync(int FuelRefillId);
        Task<FuelRefill?> AddFuelRefillAsync(FuelRefill? fuelRefill);
        bool DoesFuelRefillExists(string fuelRefill);
        Task<bool> UpdateFuelRefillAsync(FuelRefill fuelRefill);
        Task<bool> DeactivateFuelRefillAsync(FuelRefill fuelRefill);
        Task<bool> IsFuelRefillExist(int FuelRefillId);
    }
}