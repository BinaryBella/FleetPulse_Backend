using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.DTOs;
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

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            return await _context.Trips
                .Include(t => t.Vehicle)
                .Include(t => t.TripUsers)
                .ThenInclude(tu => tu.User)
                .ToListAsync();
        }

        public async Task<Trip> GetTripByIdAsync(int id)
        {
            return await _context.Trips
                .Include(t => t.Vehicle)
                .Include(t => t.TripUsers)
                .ThenInclude(tu => tu.User)
                .FirstOrDefaultAsync(t => t.TripId == id);
        }

        public async Task<bool> IsTripExist(int id)
        {
            return await _context.Trips.AnyAsync(t => t.TripId == id);
        }

        public bool DoesTripExists(int tripId)
        {
            return _context.Trips.Any(t => t.TripId == tripId);
        }

        public async Task<Trip> AddTripAsync(TripDTO tripDto)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleRegistrationNo == tripDto.VehicleRegistrationNo);

            if (vehicle == null)
            {
                throw new ArgumentException("Vehicle not found");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.NIC == tripDto.NIC);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var trip = new Trip
            {
                Date = tripDto.StartDate,
                StartTime = tripDto.StartTime,
                StartMeterValue = tripDto.StartMeterValue,
                EndTime = tripDto.EndTime,
                EndMeterValue = tripDto.EndMeterValue,
                Status = false,
                VehicleId = vehicle.VehicleId,
                TripUsers = new List<TripUser>
                {
                    new TripUser
                    {
                        UserId = user.UserId
                    }
                }
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task<bool> UpdateTripAsync(Trip trip)
        {
            var existingTrip = await _context.Trips.FindAsync(trip.TripId);
            if (existingTrip == null) return false;

            existingTrip.Date = trip.Date;
            existingTrip.StartTime = trip.StartTime;
            existingTrip.EndTime = trip.EndTime;
            existingTrip.StartMeterValue = trip.StartMeterValue;
            existingTrip.EndMeterValue = trip.EndMeterValue;
            existingTrip.Status = trip.Status;
            existingTrip.VehicleId = trip.VehicleId;
            existingTrip.TripUsers = trip.TripUsers;

            _context.Trips.Update(existingTrip);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeactivateTripAsync(int tripId)
        {
            var trip = await _context.Trips.FindAsync(tripId);
            if (trip != null)
            {
                trip.Status = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActivateTripAsync(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip != null)
            {
                trip.Status = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
