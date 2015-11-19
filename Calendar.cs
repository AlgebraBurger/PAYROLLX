using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPAYROLLCONSOLE
{
    public class CalendarEvent
    {
        public int ID { get; set; }
        public string EventName { get; set; }        
        public TypeOfWorkingDay typeOfWorkingDay { get; set; }
        public DateTime EventDate { get; set; }
    
    }

    public  static class Calendar
    {
        public static List<CalendarEvent> getCalendar()
        {
            return new List<CalendarEvent>(){
                new CalendarEvent() { EventName="New Year", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-01-01") },
                new CalendarEvent() { EventName="Special non-working day after New Year", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-01-02") },
                new CalendarEvent() { EventName="Chinese Lunar New Year's Dayr", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-02-08") },
                new CalendarEvent() { EventName="People Power Anniversary", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-02-25") },
                new CalendarEvent() { EventName="March equinox", typeOfWorkingDay = TypeOfWorkingDay.OrdinaryDay, EventDate = DateTime.Parse("2016-03-20") },
                new CalendarEvent() { EventName="Maundy Thursday", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-03-24") },
                new CalendarEvent() { EventName="Good Friday", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-03-25") },
                new CalendarEvent() { EventName="Holy Saturday", typeOfWorkingDay = TypeOfWorkingDay.OrdinaryDay, EventDate = DateTime.Parse("2016-03-26") },
                new CalendarEvent() { EventName="Easter Sunday", typeOfWorkingDay = TypeOfWorkingDay.OrdinaryDay, EventDate = DateTime.Parse("2016-03-27") },
                new CalendarEvent() { EventName="The Day of Valor", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-04-09") },
                new CalendarEvent() { EventName="Labor Day", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-05-01") },
                new CalendarEvent() { EventName="Lailatul Isra Wal Mi Raj", typeOfWorkingDay = TypeOfWorkingDay.OrdinaryDay, EventDate = DateTime.Parse("2016-05-05") },
                new CalendarEvent() { EventName="Independence Day", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-06-12") },
                new CalendarEvent() { EventName="June Solstice", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-06-20") },
                new CalendarEvent() { EventName="Eidul-Fitar", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-07-08") },
                new CalendarEvent() { EventName="Ninoy Aquino Day", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-08-21") },
                new CalendarEvent() { EventName="National Heroes Day holiday", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-08-29") },
                new CalendarEvent() { EventName="Id-ul-Adha (Feast of the Sacrifice)", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-09-13") },
                new CalendarEvent() { EventName="September equinox", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-09-22") },
                new CalendarEvent() { EventName="Amun Jadid", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-10-03") },
                new CalendarEvent() { EventName="Special non-working Day", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-10-31") },
                new CalendarEvent() { EventName="All Saints' Day", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-11-01") },
                new CalendarEvent() { EventName="All Souls' Day", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-11-02") },
                new CalendarEvent() { EventName="Bonifacio Day", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-11-30") },
                new CalendarEvent() { EventName="Maulid un-Nabi", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-12-12") },
                new CalendarEvent() { EventName="December Solstice", typeOfWorkingDay = TypeOfWorkingDay.OrdinaryDay, EventDate = DateTime.Parse("2016-12-21") },
                new CalendarEvent() { EventName="Christmas Eve", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-12-24") },
                new CalendarEvent() { EventName="Christmas Day", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-12-25") },
                new CalendarEvent() { EventName="Rizal Day", typeOfWorkingDay = TypeOfWorkingDay.RegularDay, EventDate = DateTime.Parse("2016-12-30") },
                new CalendarEvent() { EventName="New Year's Eve", typeOfWorkingDay = TypeOfWorkingDay.SpecialDay, EventDate = DateTime.Parse("2016-12-31") }
            };
        }
    }
}
