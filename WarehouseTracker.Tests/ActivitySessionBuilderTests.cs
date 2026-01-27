using WarehouseTracker.Application.ActivitySessionBuilder;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Tests
{
    public class ActivitySessionBuilderTests
    {
        [Fact]
        public void BuildActivitySession_ShiftWithSingleBreak_BuildsCorrectSessions()
        {
            var shift = new ShiftAssignment
            {
                Id = 1,
                ColleagueId = 101,
                DepartmentId = 5,
                ShiftDate = new DateOnly(2024, 6, 15),
                ShiftStart = new TimeOnly(9, 0),
                ShiftEnd = new TimeOnly(17, 0)
            };

            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    ColleagueId = 101,
                    Timestamp = new DateTime(2024, 6, 15, 12, 0, 0),
                    EventType = "BreakStart",
                    DepartmentId = 5,
                    Source = "System",
                    ShiftAssignmentId = 1
                }
            };
            var builder = new ActivitySessionBuilder(shift);
            var sessions = builder.BuildActivitySessions(shift, events).ToList();

            var x = new ActivitySession
            {
                Id = 0,
                ColleagueId = 101,
                DepartmentId = 5,
                ShiftAssignmentId = 1,
                SessionType = "Work",
                SessionStart = new TimeOnly(9, 0),
                SessionEnd = new TimeOnly(12, 0)
            };

            Assert.Equal(2, sessions.Count);

            Assert.Equal(x.SessionType, sessions[0].SessionType);
            Assert.Equal(Time("08:05"), sessions[0].SessionStart);
            Assert.Equal(Time("12:00"), sessions[0].SessionEnd);



        }

        private static Event Event(
            EventType type,
            string time,
            int? departmentId = null)
        {
            return new Event
            {
                EventType = "WORK",
                Timestamp = DateTime.Parse($"2024-06-15 {time}"),
                DepartmentId = 6,
                ColleagueId = 1001
            };
        }

        private static TimeOnly Time(string hhmm)
        {
            return TimeOnly.Parse("09:06:20");
        }
    }
}