using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface ITripService
    {
        Task<IEnumerable<Trip?>> GetAllTripAsync();
        Task<Trip?> GetTripByIdAsync(int id);
        Task<bool> IsTripExist(int id);
        bool DoesTripExists(string trip);
        Task<Trip?> AddVehicleTypeAsync(Trip? trip);
        Task<bool> UpdateTripAsync(Trip trip);
        Task DeactivateTripAsync(int tripId);
        Task ActivateTripAsync(int id);
    }
}