using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserKey>? UserKeys { get; set; }
        public DbSet<Visa>? Visas { get; set; }
        public DbSet<VisaCountry>? VisaCountries { get; set; }
        public DbSet<VisaPrice>? VisaPrices { get; set; }
        public DbSet<Agent>? Agents { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<SupplierCode>? SupplierCodes { get; set; }
        public DbSet<Supplier>? Suppliers { get; set; }
        public DbSet<Request>? Requests { get; set; }
        public DbSet<Booking>? Bookings { get; set; }
        public DbSet<Traveler>? Traveler { get; set; }
        public DbSet<FlightRequest>? FlightRequests { get; set; }
        public DbSet<HotelRequest>? HotelRequests { get; set; }
        public DbSet<VisaRequest>? VisaRequests { get; set; }
    }
}
