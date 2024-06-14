using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMaintenanceType>>> GetAllTrips()
        {
            var trips = await _tripService.GetAllTripsAsync();
            return Ok(trips);
        }
        [HttpGet("{tripid}")]
        public async Task<ActionResult<Trip>> GetTripById(int tripid)
        {
            var trip = await _tripService.GetTripByIdAsync(tripid);
            if (trip == null)
                return NotFound();

            return Ok(trip);
        }

        [HttpPost]
        public async Task<ActionResult> AddTripAsync([FromBody] TripDTO tripDTO)
        {
            var response = new ApiResponse();
            try
            {
                var trip = new Trip
                {
                    TripId = tripDTO.TripId,
                Date = tripDTO.Date,
                StartTime = tripDTO.StartTime,
                EndTime = tripDTO.EndTime,
                StartLocation = tripDTO.StartLocation,
               EndLocation = tripDTO.EndLocation,
               StartMeterValue = tripDTO.StartMeterValue,
                EndMeterValue = tripDTO.EndMeterValue,
                Status = tripDTO.Status
            };

                var tripExists = _tripService.DoesTripExists(trip.TripId);
                if (tripExists)
                {
                    response.Message = "Trip already exists";
                    return new JsonResult(response);
                }

                var addedTrip = await _tripService.AddTripAsync(trip);

                if (addedTrip != null)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Trip";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }


        [HttpPut("UpdateTrip")]
        public async Task<IActionResult> UpdateTrip([FromBody] TripDTO trip)
        {
            try
            {
                var existingTrip = await _tripService.IsTripExist(trip.Id);

                if (!existingTrip)
                {
                    return NotFound("Trip with Id not found");
                }

                var trip = new Trip
                {
                   
                    TripId = trip.TripId,
                    Date = trip.Date,
                    StartTime = trip.StartTime,
                    EndTime = trip.EndTime,
                    StartLocation = trip.StartLocation,
                    EndLocation = trip.EndLocation,
                    StartMeterValue = trip.StartMeterValue,
                    EndMeterValue = trip.EndMeterValue,
                    Status = trip.Status
                };
                var result = await _tripService.UpdateTripAsync(trip);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the trip: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateTrip(int id)
        {
            try
            {
                await _tripService.DeactivateTripAsync(id);
                return Ok("Trip deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateTripType(int id)
        {
            try
            {
                await _tripService.ActivateTripAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

