using Microsoft.EntityFrameworkCore;
using FlightsList.Models;

namespace FlightsList.Data
{
    public class FlightsContext : DbContext
    {
        public FlightsContext(DbContextOptions<FlightsContext> options) : base(options) { }

        public DbSet<Flight> Flights => Set<Flight>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.FlightNumber)
                .IsUnique();

            // Seed data (must include primary keys)
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    FlightId = 1,
                    FlightNumber = "UA3321",
                    From = "Chicago",
                    To = "New York",
                    Date = new DateTime(2026, 2, 15),
                    Price = 235m
                },
                new Flight
                {
                    FlightId = 2,
                    FlightNumber = "QA1078",
                    From = "Dubai",
                    To = "London",
                    Date = new DateTime(2026, 3, 1),
                    Price = 590m
                },
                new Flight
                {
                    FlightId = 3,
                    FlightNumber = "CA9087",
                    From = "Hong Kong",
                    To = "San Francisco",
                    Date = new DateTime(2026, 6, 15),
                    Price = 900m
                }
            );
        }
    }
}
