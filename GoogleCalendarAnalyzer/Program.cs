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
                var firstEvent = firstCalendar.Events.First();
            }
        }
    }
}
