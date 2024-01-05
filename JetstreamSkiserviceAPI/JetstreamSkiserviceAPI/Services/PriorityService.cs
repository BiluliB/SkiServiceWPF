using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Services
{
    /// <summary>
    /// The PriorityService class provides methods to interact with priority data in the database, implementing from IPriorityService
    /// </summary>
    public class PriorityService : IPriorityService
    {
        private readonly RegistrationsContext _context;

        /// <summary>
        /// Constructor for the PriorityService class
        /// </summary>
        /// <param name="context">The database context used for data operations</param>
        public PriorityService(RegistrationsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all priorities along with their associated registrations from the database
        /// </summary>
        /// <returns>A collection of registrations ordered by priorities</returns>
        public async Task<IEnumerable<PriorityDto>> GetAll()
        {
            var priorities = await _context.Priority
                .Include(p => p.Registrations)
                    .ThenInclude(r => r.Status)
                 .Include(p => p.Registrations)
                    .ThenInclude(r => r.Service)
                .ToListAsync();

            return priorities.Select(p => new PriorityDto
            {
                PriorityId = p.PriorityId,
                PriorityName = p.PriorityName,
                Registration = p.Registrations.Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    Phone = r.Phone,
                    Create_date = r.Create_date,
                    Pickup_date = r.Pickup_date,
                    Priority = p.PriorityName,
                    Service = r.Service.ServiceName,
                    Status = r.Status.StatusName,
                    Price = r.Price,
                    Comment = r.Comment
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// Retrieves a single priority by its name along with its associated registrations
        /// </summary>
        /// <param name="priorityName">The name of the priority to retrieve (Tief/Standard/Express)</param>
        /// <returns>A collection of registrations with the associated priority</returns>
        public async Task<PriorityDto> GetByPriority(string priorityName)
        {
            var allPriorities = await GetAll();
            return allPriorities.FirstOrDefault(p => p.PriorityName == priorityName);
        }
    }
}
