using System;
using System.IO;
using System.Linq;
using Ical.Net;

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
                var events = firstCalendar.Events
                    .Where(x => x.DtEnd.AsSystemLocal >= new DateTime(2016, 12, 1) && x.DtEnd.AsSystemLocal <= new DateTime(2017, 2, 28))
                    .Where(x => string.Equals(x.Summary, "Praca", StringComparison.InvariantCultureIgnoreCase));
                double summary = events.Sum(@event => (@event.DtEnd.AsSystemLocal - @event.Start.AsSystemLocal).TotalMinutes);
                int hours = (int) (summary/60);
                int minutes = (int) summary - (hours*60);
                if (hours + minutes > 0)
                {
                    Console.WriteLine($"{hours}:{minutes}");
                    Console.ReadKey();
                }
            }
        }
    }
}
