using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleService
    {
        private readonly FleetPulseDbContext _context;
        public VehicleService(FleetPulseDbContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }


        public async Task<Vehicle> UpdateVehicle(int id,VehicleDTO updatedVehicle)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return null;
            }
            // Update properties
            vehicle.VehicleRegistrationNo = updatedVehicle.VehicleRegistrationNo;
            vehicle.LicenseNo = updatedVehicle.LicenseNo;
            vehicle.LicenseExpireDate = updatedVehicle.LicenseExpireDate;
            vehicle.VehicleColor = updatedVehicle.VehicleColor;
            vehicle.Status = updatedVehicle.Status;
            vehicle.VehicleModelId = updatedVehicle.VehicleModelId;
            vehicle.VehicleTypeId = updatedVehicle.VehicleTypeId;
            vehicle.ManufactureId = updatedVehicle.ManufactureId;
            vehicle.FuelRefillId = updatedVehicle.FuelRefillId;
            vehicle.VehicleMaintenanceId = updatedVehicle.VehicleMaintenanceId;
            vehicle.AccidentId = updatedVehicle.AccidentId;
            vehicle.TripId = updatedVehicle.TripId;

            await _context.SaveChangesAsync();
            return vehicle;
        }
        // Delete operation
        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return false; // Or handle not found scenario as per your requirement
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
