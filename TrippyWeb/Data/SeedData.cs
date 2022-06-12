using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;

namespace TrippyWeb.Data
{
    public static class SeedData
    {
        public static void Initialize(TrippyWebDbContext context)
        {
            using (context)
            {
                if (context == null ||
                 context.Trips == null ||
                  context.TrippyUsers == null ||
                   context.Reviews == null ||
                   context.Messages == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                if (context.Trips.Any() && context.TrippyUsers.Any() && context.Messages.Any())
                {
                    return;
                }

                Model.TrippyUser owner1 = new()
                {
                    Name = "Owner1",
                    OfferedTrips = { },
                    TripId = 0,
                    JoinedTrips = { },
                    Reviews = { },
                    Email = "email1@example.local",
                    UserName = "Jack"
                };
                Model.TrippyUser owner2 = new()
                {
                    Name = "Owner2",
                    OfferedTrips = { },
                    TripId = 0,
                    JoinedTrips = { },
                    Reviews = { },
                    Email = "email2@example.local",
                    UserName = "Tony"
                };
                Model.TrippyUser owner3 = new()
                {
                    Name = "Owner3",
                    OfferedTrips = { },
                    TripId = 0,
                    JoinedTrips = { },
                    Reviews = { },
                    Email = "email3@example.local",
                    UserName = "Emily"
                };
                Model.TrippyUser passenger1 = new()
                {
                    Name = "Passenger1",
                    OfferedTrips = { },
                    TripId = 0,
                    JoinedTrips = { },
                    Reviews = { },
                    Email = "email4@example.local",
                    UserName = "Stan"
                };
                Model.TrippyUser passenger2 = new()
                {
                    Name = "Passenger2",
                    OfferedTrips = { },
                    TripId = 0,
                    JoinedTrips = { },
                    Reviews = { },
                    Email = "email5@example.local",
                    UserName = "Stefany"
                };
                Model.TrippyUser passenger3 = new()
                {
                    Name = "Passenger3",
                    OfferedTrips = { },
                    TripId = 0,
                    JoinedTrips = { },
                    Reviews = { },
                    Email = "eamil6@example.local",
                    UserName = "Jack"
                };

                if (!context.TrippyUsers.Any())
                {
                    context.TrippyUsers.AddRange(owner1, owner2, owner3, passenger1, passenger2, passenger3);
                    context.SaveChanges();
                }

                if (!context.Trips.Any())
                {
                    context.Trips.AddRange(
                       new Model.Trip
                       {
                           Beginning = "START 1",
                           Destination = "DESTINATION 1",
                           StartDate = DateTime.Parse("2008-05-01 7:34:42Z"),
                           EndDate = DateTime.Parse("2008-05-01 10:34:42Z"),
                           DurationInMinutes = 180,
                           FreeSpots = 3,
                           OwnerID = owner1.Id,
                           Owner = owner1,
                           Price = 22.0,
                           Stops = "",
                           Passengers = { passenger1, passenger3, owner3 },
                           NonSmoking = false
                       },
                       new Model.Trip
                       {
                           Beginning = "Start 2",
                           Destination = "Destination 3",
                           StartDate = DateTime.Now.AddHours(2),
                           EndDate = DateTime.Now.AddHours(7),
                           DurationInMinutes = 60,
                           FreeSpots = 4,
                           OwnerID = owner3.Id,
                           Owner = owner3,
                           Price = 11.0,
                           Stops = "Grocery Shop 1",
                           // Messages = { },
                           Passengers = { owner2, passenger1, passenger3 },
                           NonSmoking = false
                       },
                       new Model.Trip
                       {
                           Beginning = "Start 3",
                           Destination = "Destination 4",
                           StartDate = DateTime.Parse("2022-06-13 10:34:42Z"),
                           EndDate = DateTime.Parse("2022-06-13 12:34:42Z"),
                           DurationInMinutes = 120,
                           FreeSpots = 2,
                           OwnerID = owner2.Id,
                           Owner = owner2,
                           Price = 120.0,
                           Stops = "Grocery Shop",
                           Passengers = { owner1, passenger2 },
                           NonSmoking = false
                       }

                    );
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }
    }
}
