using System;
using System.IO;
using System.Linq;
using Ical.Net;
using Ical.Net.Interfaces.Components;

namespace GoogleCalendarAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles("../../Data/").Where(x => x.EndsWith(".ics")).ToList();
            foreach (var file in files)
            {
                var calendarCollection = Calendar.LoadFromFile(file);
                var firstCalendar = calendarCollection.First();
                var startDate = new DateTime(2016, 12, 1);
                var endDate = new DateTime(2017, 2, 28);

                var events = firstCalendar.Events
                    .Where(x => x.DtEnd.AsSystemLocal >= startDate && x.DtEnd.AsSystemLocal <= endDate )
                    .Where(x => string.Equals(x.Summary, "Praca", StringComparison.InvariantCultureIgnoreCase));

                double summary = events.Sum(@event => GetTotalMinutes(@event) + @event.GetOccurrences(startDate, endDate).Sum(x => x.Period.Duration.TotalMinutes));
                int hours = (int) (summary/60);
                int minutes = (int) summary - (hours*60);

                const int hours2 = 248;
                if (hours + minutes > 0)
                {
                    Console.WriteLine($"Time in calendar: {hours}:{minutes}");
                    
                    Console.WriteLine($"Time in timespan: {hours2}:{0}");
                    Console.ReadKey();
                }
            }
        }

        private static double GetTotalMinutes(IEvent @event)
        {
            return  GetTotalMinutes(@event.Start.AsSystemLocal, @event.DtEnd.AsSystemLocal);
        }

        private static double GetTotalMinutes(DateTime start, DateTime end)
        {
            return (end - start).TotalMinutes;
        }
    }
}
