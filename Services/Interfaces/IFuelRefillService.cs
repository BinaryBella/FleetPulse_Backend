// IFuelRefillService.cs
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IFuelRefillService
    {
        Task<List<FuelRefillDTO>> GetAllFuelRefillsAsync();
        Task<FuelRefill> GetFuelRefillByIdAsync(int fuelRefillId);
        bool DoesFuelRefillExist(string fType);
        Task<FuelRefill?> AddFuelRefillAsync(FuelRefillDTO fuelRefillDto);
        Task<bool> UpdateFuelRefillAsync(int fuelRefillId, FuelRefill fuelRefill);
        Task DeactivateFuelRefillAsync(int fuelRefillId);
        Task<bool> IsFuelRefillExist(int fuelRefillId);
    }
}