using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace JetstreamSkiserviceAPI.Services
{
    /// <summary>
    /// Service for handling token-based operations, implementing from ITokenService
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly RegistrationsContext _registrationsContext;
        private readonly SymmetricSecurityKey _key;

        /// <summary>
        /// Initializes a new instance of the TokenService class
        /// </summary>
        /// <param name="configuration">Configuration settings for token generation</param>
        /// <param name="registrationsContext">Context for accessing registration data</param>
        public TokenService(IConfiguration configuration, RegistrationsContext registrationsContext)
        {
            _registrationsContext = registrationsContext;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        }


        /// <summary>
        /// Creates a new token for a specified username
        /// </summary>
        /// <param name="username">The username to generate a token for</param>
        /// <returns>A JWT token as a string</returns>
        /// <exception cref="Exception">Thrown when token creation fails</exception>
        public string CreateToken(string username)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, username)
            };

                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// Retrieves a list of all employees for the login check
        /// </summary>
        /// <returns>A list of Employee objects</returns>
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees = _registrationsContext.Employees.ToList();
            return employees;
        }


        /// <summary>
        /// Increments the login attempt count for a specific employee
        /// </summary>
        /// <param name="employeeId">ID of the employee whose login attempts are to be incremented</param>
        /// <exception cref="NotImplementedException">Thrown if the method is not implemented</exception>
        public void Attempts(int employeeId)
        {
            Employee employee = new Employee();

            employee = _registrationsContext.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
            employee.Attempts += 1;
            _registrationsContext.Entry(employee).State = EntityState.Modified;
            _registrationsContext.SaveChanges();
        }

        /// <summary>
        /// Resets the login attempt count for a specific employee, effectively unbanning them
        /// </summary>
        /// <param name="employeeId">ID of the employee to unban</param>
        /// <exception cref="NotImplementedException">Thrown if the method is not implemented</exception>
        public void Unban(int employeeId)
        {
            Employee employee = new Employee();
            employee = _registrationsContext.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
            employee.Attempts = 0;
            _registrationsContext.Entry(employee).State = EntityState.Modified;
            _registrationsContext.SaveChanges();
        }
    }
}
