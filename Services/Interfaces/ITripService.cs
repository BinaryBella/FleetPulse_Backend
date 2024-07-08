using System.Collections.Generic;
using System.Threading.Tasks;
using FleetPulse_BackEndDevelopment.DTOs;
using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<Trip> GetTripByIdAsync(int id);
        Task<bool> IsTripExist(int id);
        bool DoesTripExists(int tripId);
        Task<Trip> AddTripAsync(TripDTO tripDto); // Change to accept TripDTO
        Task<bool> UpdateTripAsync(Trip trip);
        Task DeactivateTripAsync(int tripId);
        Task ActivateTripAsync(int id);
    }
}
