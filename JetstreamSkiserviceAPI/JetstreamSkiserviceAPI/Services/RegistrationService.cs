using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Services
{
    /// <summary>
    /// The RegistrationService class provides methods to get and set data from the heart of the database, implementing from IRegistrationService
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly RegistrationsContext _context;

        /// <summary>
        /// Initializes a new instance of the RegistrationService class with the specified database context
        /// </summary>
        /// <param name="context">The database context to be used for data operations</param>
        public RegistrationService(RegistrationsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all registrations from the database and returns them as a list of RegistrationDto objects
        /// </summary>
        public async Task<IEnumerable<RegistrationDto>> GetRegistrations()
        {
            // Filtert alle Registrierungen heraus, deren Status nicht "storniert" ist
            var registrations = await _context.Registrations
                .Where(r => r.Status.StatusName != "storniert")
                .Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    Phone = r.Phone,
                    Create_date = r.Create_date,
                    Pickup_date = r.Pickup_date,
                    Status = r.Status.StatusName,
                    Priority = r.Priority.PriorityName,
                    Service = r.Service.ServiceName,
                    Price = r.Price,
                    Comment = r.Comment
                })
                .ToListAsync();

            return registrations;
        }

        /// <summary>
        /// Retrieves a single registration by its ID and returns it as a RegistrationDto object.
        /// </summary>
        /// <param name="id">The ID of the registration to retrieve.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Referenced ID or Item not found or doesn't exist</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task<RegistrationDto> GetRegistrationById(int id)
        {
            var registration = await _context.Registrations
                .Include(r => r.Status)
                .Include(r => r.Priority)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(r => r.RegistrationId == id);

            if (registration == null)
            {
                throw new KeyNotFoundException("Referenced ID or Item not found or doesn't exist");
            }

            try
            {
                return new RegistrationDto
                {
                    RegistrationId = registration.RegistrationId,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    Email = registration.Email,
                    Phone = registration.Phone,
                    Create_date = registration.Create_date,
                    Pickup_date = registration.Pickup_date,
                    Status = registration.Status?.StatusName,
                    Priority = registration.Priority?.PriorityName,
                    Service = registration.Service?.ServiceName,
                    Price = registration.Price,
                    Comment = registration.Comment
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// Adds a new registration to the database based on the provided RegistrationDto object
        /// </summary>
        /// <param name="registrationDto">The RegistrationDto object containing the registration details</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task<RegistrationDto> AddRegistration(RegistrationDto registrationDto)
        {
            var statusName = registrationDto.Status;
            if (string.IsNullOrWhiteSpace(statusName) || (statusName != "Offen" && statusName != "InArbeit" && statusName != "abgeschlossen"))
            {
                statusName = "Offen";
            }

            var priorityName = registrationDto.Priority;
            if (string.IsNullOrWhiteSpace(priorityName) || (priorityName != "Standard" && priorityName != "Express" && priorityName != "Tief"))
            {
                priorityName = "Tief";
            }

            var serviceName = registrationDto.Service;
            if (string.IsNullOrWhiteSpace(serviceName) || (serviceName != "Kleiner Service" && serviceName != "Grosser Service" && serviceName != "Rennski Service" && serviceName != "Bindungen montieren und einstellen" && serviceName != "Fell zuschneiden" && serviceName != "Heisswachsen" ))
            {
                serviceName = "Kleiner Service";
            }
            var service = _context.Services.FirstOrDefault(e => e.ServiceName == serviceName);


            var registration = new Registration
            {
                RegistrationId = registrationDto.RegistrationId,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                Phone = registrationDto.Phone,
                Create_date = registrationDto.Create_date,
                Pickup_date = registrationDto.Pickup_date,
                Status = _context.Status.FirstOrDefault(e => e.StatusName == statusName),
                Priority = _context.Priority.FirstOrDefault(e => e.PriorityName == priorityName),
                Service = _context.Services.FirstOrDefault(e => e.ServiceName == serviceName),
                Price = registrationDto.Price,
                Comment = registrationDto.Comment
            };
            
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
            registrationDto.RegistrationId = registration.RegistrationId;

            return registrationDto;
        }

        /// <summary>
        /// Updates an existing registration in the database based on the provided RegistrationDto object.
        /// </summary>
        /// <param name="registrationDto">The RegistrationDto object containing the updated registration details</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Referenced ID or Item not found or doesn't exist</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task UpdateRegistration(RegistrationDto registrationDto)
        {
            var registration = await _context.Registrations.FindAsync(registrationDto.RegistrationId);
            if (registration == null)
            {
                throw new KeyNotFoundException("Referenced ID or Item not found or doesn't exist");
            }

            registration.FirstName = registrationDto.FirstName;
            registration.LastName = registrationDto.LastName;
            registration.Email = registrationDto.Email;
            registration.Phone = registrationDto.Phone;
            registration.Create_date = registrationDto.Create_date;
            registration.Pickup_date = registrationDto.Pickup_date;
            registration.Status = _context.Status.FirstOrDefault(e => e.StatusName == registrationDto.Status);
            registration.Priority = _context.Priority.FirstOrDefault(e => e.PriorityName == registrationDto.Priority);
            registration.Service = _context.Services.FirstOrDefault(e => e.ServiceName == registrationDto.Service);
            registration.Price = registrationDto.Price;
            registration.Comment = registrationDto.Comment;

            _context.Entry(registration).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a registration from the database by its ID
        /// </summary>
        /// <param name="id">The ID of the registration to delete</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Referenced ID or Item not found or doesn't exist</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the process</exception>
        public async Task DeleteRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                throw new KeyNotFoundException("Referenced ID or Item not found or doesn't exist");
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
        }
    }
}
