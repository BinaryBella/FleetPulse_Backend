using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle?>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public Task<bool> IsVehicleExist(int id)
        {
            return Task.FromResult(_context.Vehicles.Any(x => x.VehicleId == id));
        }

        public bool DoesVehicleExists(string vehicleRegistrationNo)
        {
            return _context.Vehicles.Any(x => x.VehicleRegistrationNo == vehicleRegistrationNo);
        }

        public async Task<Vehicle?> AddVehicleAsync(Vehicle? vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<bool> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeactivateVehicleAsync(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new InvalidOperationException("Vehicle not found.");
            }

            if (VehicleIsActive(vehicle))
            {
                throw new InvalidOperationException("Vehicle is active and associated with vehicle records. Cannot deactivate.");
            }

            vehicle.Status = "Inactive"; 
            await _context.SaveChangesAsync();
        }

        private bool VehicleIsActive(Vehicle vehicle)
        {
            return vehicle.Status == "Active"; 
        }

        public async Task ActivateVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException("Vehicle not found.");
            }

            vehicle.Status = "Active"; 
            await _context.SaveChangesAsync();
        }
    }
}
