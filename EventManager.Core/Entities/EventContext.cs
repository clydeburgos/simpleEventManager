using Faker;
using Microsoft.EntityFrameworkCore;
using System;
using TimeZoneNames;

namespace EventManager.Domain.Entities
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            for (int x = 0; x <= 100; x++) {

                int randomNumber = Faker.RandomNumber.Next(1, 100);
                string companyName = Faker.Company.Name();
                string address = $"{ Faker.Address.Country() }, { Faker.Address.City() }";
                string eventName =  $"{ companyName } 's { randomNumber } rd/th Annual Conference";
                string eventDescription = $"{eventName} at { address } - { Faker.Company.CatchPhrase().ToUpper() }. ";

                string[] zones = TZNames.GetTimeZoneIdsForCountry("AU");
                string timeZone = zones[Faker.RandomNumber.Next(0, zones.Length)];

                DateTime startDate = DateTime.Now.AddDays(randomNumber);

                modelBuilder.Entity<Event>().HasData(new Event
                {
                    Identifier = Guid.NewGuid(),
                    EventName = eventName,
                    EventDescription = eventDescription,
                    EventTimezone = timeZone,
                    StartDate = startDate,
                    EndDate = startDate.AddDays(1),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
            }
        }
    }
}
