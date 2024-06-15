using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class DriverService : IDriverService
    {
        private readonly FleetPulseDbContext _context;

        public DriverService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllDriversAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetDriverByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<bool> IsDriverExist(int id)
        {
            return Task.FromResult(_context.Users.Any(x => x.UserId == id));
        }

        public bool DoesDriverExist(string NIC)
        {
            return _context.Users.Any(x => x.NIC == NIC);
        }

        public async Task<User> AddDriverAsync(User driver)
        {
            _context.Users.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<bool> UpdateDriverAsync(User driver)
        {
            try
            {
                _context.Users.Update(driver);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeactivateDriverAsync(int userId)
        {
            var driver = await _context.Users.FindAsync(userId);

            if (driver == null)
            {
                throw new InvalidOperationException("Driver not found.");
            }

            if (DriverIsActive(driver))
            {
                throw new InvalidOperationException("Driver is active and associated with driver details records. Cannot deactivate.");
            }

            driver.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool DriverIsActive(User driver)
        {
            return _context.Users.Any(vt => vt.UserId == driver.UserId && vt.Status);
        }

        public async Task ActivateDriverAsync(int id)
        {
            var driver = await _context.Users.FindAsync(id);
            if (driver == null)
            {
                throw new KeyNotFoundException("Driver not found.");
            }

            driver.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}
