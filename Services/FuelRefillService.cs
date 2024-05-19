using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class FuelRefillService : IFuelRefillService
    {
        private readonly FleetPulseDbContext _context;

        public FuelRefillService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<List<FuelRefillDTO>> GetAllFuelRefillsAsync()
        {
            return await _context.FuelRefills
                .Include(fr => fr.Vehicle)
                .Include(fr => fr.FuelRefillUsers)
                .ThenInclude(fru => fru.User)
                .Select(fr => new FuelRefillDTO
                {
                    FuelRefillId = fr.FuelRefillId,
                    Date = fr.Date,
                    Time = fr.Time,
                    LiterCount = fr.LiterCount,
                    FType = fr.FType,
                    Cost = fr.Cost,
                    VehicleRegistrationNo = fr.Vehicle.VehicleRegistrationNo,
                    NIC = fr.FuelRefillUsers.FirstOrDefault().User.NIC ?? string.Empty,
                    Status = fr.Status
                })
                .ToListAsync();
        }

        public async Task<FuelRefill> GetFuelRefillByIdAsync(int FuelRefillId)
        {
            return await _context.FuelRefills
                .Include(fr => fr.Vehicle)
                .Include(fr => fr.FuelRefillUsers)
                .ThenInclude(fru => fru.User)
                .FirstOrDefaultAsync(fr => fr.FuelRefillId == FuelRefillId);
        }

        public bool DoesFuelRefillExist(string FType)
        {
            return _context.FuelRefills.Any(fr => fr.FType == FType);
        }

        public async Task<FuelRefill?> AddFuelRefillAsync(FuelRefill fuelRefill)
        {
            _context.FuelRefills.Add(fuelRefill);
            await _context.SaveChangesAsync();
            return fuelRefill;
        }

        public async Task<bool> UpdateFuelRefillAsync(FuelRefill fuelRefill)
        {
            try
            {
                _context.FuelRefills.Update(fuelRefill);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeactivateFuelRefillAsync(FuelRefill fuelRefill)
        {
            fuelRefill.Status = false;
            _context.FuelRefills.Update(fuelRefill);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> IsFuelRefillExist(int FuelRefillId)
        {
            return _context.FuelRefills.AnyAsync(fr => fr.FuelRefillId == FuelRefillId);
        }
    }
}
