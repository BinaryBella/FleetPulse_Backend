using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class FuelRefillService : IFuelRefillService
    {
        private readonly FleetPulseDbContext _context;

        public FuelRefillService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FuelRefill?>> GetAllFuelRefillsAsync()
        {
            return await _context.FuelRefill.ToListAsync();
        }

        public async Task<FuelRefill?> GetFuelRefillByIdAsync(int FuelRefillId)
        {
            return await _context.FuelRefill.FindAsync(FuelRefillId);
        }
        
        public Task<bool> IsFuelRefillExist(int FuelRefillId)
        {
            return Task.FromResult(_context.FuelRefill.Any(x => x.FuelRefillId == FuelRefillId));
        }
        
        public bool DoesFuelRefillExists(string FType)
        {
            var fuelRefillType = _context.FuelRefill.FirstOrDefault(x => x.FType == FType);
            return fuelRefillType != null;
        }

        public async Task<FuelRefill?> AddFuelRefillAsync(FuelRefill? fuelRefill)
        {
            _context.FuelRefill.Add(fuelRefill);
            await _context.SaveChangesAsync();
            return fuelRefill;
        }
        
        public async Task<bool> UpdateFuelRefillAsync(FuelRefill fuelRefill)
        {
            try
            {
                var result = _context.FuelRefill.Update(fuelRefill);
                result.State = EntityState.Modified;
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
            _context.Entry(fuelRefill).State = EntityState.Detached;
            
            fuelRefill.Status = false;
            
            var result = _context.FuelRefill.Update(fuelRefill);
            
            await _context.SaveChangesAsync();
            
            if (result.State == EntityState.Modified)
            {
                return true;
            }
            return false;       
        }
        
    }
}