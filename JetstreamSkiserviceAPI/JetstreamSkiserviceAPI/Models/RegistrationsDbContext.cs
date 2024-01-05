// Add-Migration "Initial Create"
// Update-Database

using Microsoft.EntityFrameworkCore;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Database context class representing the connection to the database
    /// It includes DbSet properties for the different entities and configurations for seeding initial data
    /// </summary>
    public class RegistrationsContext : DbContext
    {
        private readonly IConfiguration _configuration;

        // Parameterless constructor for enabling migrations
        public RegistrationsContext()
        {
        }

        // Constructor that takes options and configuration parameters
        public RegistrationsContext(DbContextOptions<RegistrationsContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        // DbSet properties for each entity
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Configures the model while building the database
        /// </summary>
        /// <param name="modelBuilder">Provides a simple API for configuring the model</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set standard data for Priority
            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = 1, PriorityName = "Tief" },
                new Priority { PriorityId = 2, PriorityName = "Standard" },
                new Priority { PriorityId = 3, PriorityName = "Express" }
                );

            // Set standard data for Service
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, ServiceName = "Kleiner Service" },
                new Service { ServiceId = 2, ServiceName = "Grosser Service" },
                new Service { ServiceId = 3, ServiceName = "Rennski Service" },
                new Service { ServiceId = 4, ServiceName = "Bindungen montieren und einstellen" },
                new Service { ServiceId = 5, ServiceName = "Fell zuschneiden" },
                new Service { ServiceId = 6, ServiceName = "Heisswachsen" }
                );

            // Set standard data for Status
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = 1, StatusName = "Offen" },
                new Status { StatusId = 2, StatusName = "InArbeit" },
                new Status { StatusId = 3, StatusName = "abgeschlossen" },
                new Status { StatusId = 4, StatusName = "storniert" }
                );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Username = "admin", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 2, Username = "employee1", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 3, Username = "employee2", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 4, Username = "employee3", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 5, Username = "employee4", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 6, Username = "employee5", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 7, Username = "employee6", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 8, Username = "employee7", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 9, Username = "employee8", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 10, Username = "employee9", Password = "password", Attempts = 0 },
                new Employee { EmployeeId = 11, Username = "employee10", Password = "password", Attempts = 0 }
                );

            modelBuilder.Entity<Registration>().HasData(
                new Registration { RegistrationId = 1, FirstName = "Max", LastName = "Muster", Email = "max@mustermann.com", Phone = "0791234567", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 1, PriorityId = 1, StatusId = 1, Price = "50", Comment = "Testkommentar" },
                new Registration { RegistrationId = 2, FirstName = "Tim", LastName = "Muster", Email = "tim@mustermann.com", Phone = "0791234566", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 2, PriorityId = 2, StatusId = 2, Price = "60", Comment = "Testkommentar" },
                new Registration { RegistrationId = 3, FirstName = "Jack", LastName = "Muster", Email = "jack@mustermann.com", Phone = "0771234567", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 3, PriorityId = 3, StatusId = 3, Price = "70", Comment = "Testkommentar" },
                new Registration { RegistrationId = 4, FirstName = "Alice", LastName = "Muster", Email = "alice@mustermann.com", Phone = "0781234567", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 4, PriorityId = 1, StatusId = 4, Price = "80", Comment = "Testkommentar" },
                new Registration { RegistrationId = 5, FirstName = "Maximilian", LastName = "Muster", Email = "maximilian@mustermann.com", Phone = "0761334567", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 5, PriorityId = 2, StatusId = 1, Price = "90", Comment = "Testkommentar" },
                new Registration { RegistrationId = 6, FirstName = "Bob", LastName = "Muster", Email = "bob@mustermann.com", Phone = "0781434567", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 6, PriorityId = 3, StatusId = 2, Price = "100", Comment = "Testkommentar" },
                new Registration { RegistrationId = 7, FirstName = "Usher", LastName = "Muster", Email = "usher@mustermann.com", Phone = "0781234568", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 1, PriorityId = 1, StatusId = 3, Price = "110", Comment = "Testkommentar" },
                new Registration { RegistrationId = 8, FirstName = "Drake", LastName = "Muster", Email = "drake@mustermann.com", Phone = "0791234564", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 2, PriorityId = 2, StatusId = 1, Price = "120", Comment = "Testkommentar" },
                new Registration { RegistrationId = 9, FirstName = "Tailor", LastName = "Muster", Email = "tailor@mustermann.com", Phone = "0791234569", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 3, PriorityId = 3, StatusId = 2, Price = "130", Comment = "Testkommentar" },
                new Registration { RegistrationId = 10, FirstName = "Karen", LastName = "Muster", Email = "karen@mustermann.com", Phone = "0791234570", Create_date = DateTime.Now, Pickup_date = DateTime.Now, ServiceId = 4, PriorityId = 1, StatusId = 3, Price = "140", Comment = "Testkommentar" }
                );

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Configures the database to be used for this context
        /// This method is called for each instance of the context that is created
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string config = _configuration.GetConnectionString("JetstreamSkiserviceDB");
            optionsBuilder.UseSqlServer($@"{config}");
        }
    }
}
