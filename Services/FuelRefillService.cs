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

        public async Task<IEnumerable<FuelRefill>> GetAllFuelRefillsAsync()
        {
            return await _context.FuelRefill.ToListAsync();
        }

        public async Task<FuelRefill> GetFuelRefillByIdAsync(int id)
        {
            return await _context.FuelRefill.FindAsync(id);
        }

        public async Task<FuelRefill> AddFuelRefillAsync(FuelRefill fuelRefill)
        {
            _context.FuelRefill.Add(fuelRefill);
            await _context.SaveChangesAsync();
            return fuelRefill;
        }

        public async Task<bool> UpdateFuelRefillAsync(int id, FuelRefill fuelRefill)
        {
            var existingFuelRefill = await _context.FuelRefill.FindAsync(id);
            if (existingFuelRefill == null)
                return false;

            existingFuelRefill.Date = fuelRefill.Date;
            existingFuelRefill.Time = fuelRefill.Time;
            existingFuelRefill.LiterCount = fuelRefill.LiterCount;
            existingFuelRefill.FType = fuelRefill.FType;
            existingFuelRefill.Cost = fuelRefill.Cost;
            existingFuelRefill.Status = fuelRefill.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFuelRefillAsync(int id)
        {
            var fuelRefill = await _context.FuelRefill.FindAsync(id);
            if (fuelRefill == null)
                return false;

            _context.FuelRefill.Remove(fuelRefill);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}