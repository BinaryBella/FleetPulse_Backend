using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IFuelRefillService
    {
        Task<List<FuelRefillDTO>> GetAllFuelRefillsAsync();
        Task<FuelRefill> GetFuelRefillByIdAsync(int FuelRefillId);
        Task<FuelRefill?> AddFuelRefillAsync(FuelRefill fuelRefill);
        bool DoesFuelRefillExist(string FType);
        Task<bool> UpdateFuelRefillAsync(FuelRefill fuelRefill);
        Task<bool> DeactivateFuelRefillAsync(FuelRefill fuelRefill);
        Task<bool> IsFuelRefillExist(int FuelRefillId);
    }
}