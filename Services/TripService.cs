using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class TripService : ITripService
    {
        private readonly FleetPulseDbContext _context;

        public TripService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip?>> GetAllTripsAsync()
        {
            return await _context.Trip.ToListAsync();
        }

        public async Task<Trip?> GetTripByIdAsync(int id)
        {
            return await _context.Trip.FindAsync(id);
        }

        public Task<bool> IsTripExist(int id)
        {
            return Task.FromResult(_context.Trip.Any(x => x.Trip == id));
        }

        public bool DoesTripExists(string trip)
        {
            var trip = _context.Trip.FirstOrDefault(x => x.Trip == trip);
            return trip != null;
        }

        public async Task<Trip?> AddTripAsync(Trip? trip)
        {
            _context.Trip.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }



        public async Task<bool> UpdateTripAsync(Trip trip)
        {
            try
            {
                var result = _context.Trip.Update(trip);
                result.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task DeactivateTripAsync(int tripId)
        {
            var trip = await _context.Trip.FindAsync(tripId);

            if (trip == null)
            {
                throw new InvalidOperationException("Trip not found.");
            }

            if (TripIsActive(trip))
            {
                throw new InvalidOperationException("Trip is active and associated with trip records. Cannot deactivate.");
            }

            trip.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool TripIsActive(Trip trip)
        {
            return _context.Trip.Any(vt => vt.TripId == trip.TripId);
        }

        public async Task ActivateTripAsync(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                throw new KeyNotFoundException("Trip not found.");
            }

            trip.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}