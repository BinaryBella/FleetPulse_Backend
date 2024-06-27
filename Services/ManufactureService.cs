﻿using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class ManufactureService : IManufactureService
    {
        private readonly FleetPulseDbContext _context;

        public ManufactureService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manufacture?>> GetAllManufacturesAsync() // Corrected name
        {
            return await _context.Manufacture.ToListAsync();
        }

        public async Task<Manufacture?> GetManufactureByIdAsync(int id)
        {
            return await _context.Manufacture.FindAsync(id);
        }

        public Task<bool> IsManufactureExist(int id)
        {
            return Task.FromResult(_context.Manufacture.Any(x => x.ManufactureId == id));
        }

        public bool DoesManufactureExists(string manufacturer)
        {
            return _context.Manufacture.Any(x => x.Manufacturer == manufacturer);
        }

        public async Task<Manufacture?> AddManufactureAsync(Manufacture? manufacture)
        {
            if (manufacture != null)
            {
                _context.Manufacture.Add(manufacture);
                await _context.SaveChangesAsync();
            }
            return manufacture;
        }

        public async Task<bool> UpdateManufactureAsync(Manufacture manufacture)
        {
            var existingManufacture = await _context.Manufacture.FindAsync(manufacture.ManufactureId);
            if (existingManufacture != null)
            {
                _context.Entry(existingManufacture).CurrentValues.SetValues(manufacture);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeactivateManufactureAsync(int ManufactureId)
        {
            var manufacture = await _context.Manufacture.FindAsync(ManufactureId);

            if (manufacture == null)
            {
                throw new InvalidOperationException("Manufacturer not found.");
            }

            if (ManufactureIsActive(manufacture))
            {
                throw new InvalidOperationException("Manufacturer is active and associated with manufacture records. Cannot deactivate.");
            }

            manufacture.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool ManufactureIsActive(Manufacture manufacture)
        {
            return _context.Manufacture.Any(vt => vt.ManufactureId == manufacture.ManufactureId);
        }

        public async Task ActivateManufactureAsync(int id)
        {
            var manufacture = await _context.Manufacture.FindAsync(id);
            if (manufacture == null)
            {
                throw new KeyNotFoundException("Manufacturer not found.");
            }

            manufacture.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}