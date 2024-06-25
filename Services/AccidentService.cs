using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class AccidentService : IAccidentService
    {
        private readonly FleetPulseDbContext _context;

        public AccidentService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Accident?>> GetAllAccidentsAsync()
        {
            return await _context.Accident.ToListAsync();
        }

        public async Task<Accident?> GetAccidentByIdAsync(int id)
        {
            return await _context.Accident.FindAsync(id);
        }

        public Task<bool> IsAccidentExist(int id)
        {
            return Task.FromResult(_context.Accident.Any(x => x.AccidentId == id));
        }

        public bool DoesAccidentExists(string venue)
        {
            return _context.Accident.Any(x => x.Venue == venue);
        }

        public async Task<Accident?> AddAccidentAsync(Accident? accident)
        {
            if (accident != null)
            {
                _context.Accident.Add(accident);
                await _context.SaveChangesAsync();
            }
            return accident;
        }

        public async Task<bool> UpdateAccidentAsync(Accident accident)
        {
            var existingAccident = await _context.Accident.FindAsync(accident.AccidentId);
            if (existingAccident != null)
            {
                _context.Entry(existingAccident).CurrentValues.SetValues(accident);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeactivateAccidentAsync(int accidentId)
        {
            var accident = await _context.Accident.FindAsync(accidentId);

            if (accident == null)
            {
                throw new InvalidOperationException("Accident not found.");
            }

            if (AccidentIsActive(accident))
            {
                throw new InvalidOperationException("Accident is active and associated with accident records. Cannot deactivate.");
            }

            accident.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool AccidentIsActive(Accident accident)
        {
            return accident.Status; 
        }

        public async Task ActivateAccidentAsync(int id)
        {
            var accident = await _context.Accident.FindAsync(id);
            if (accident == null)
            {
                throw new KeyNotFoundException("Accident not found.");
            }

            accident.Status = true; 
            await _context.SaveChangesAsync();
        }
    }
}
