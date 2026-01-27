using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.ActivitySessionBuilder
{
    public class ActivitySessionBuilder : IActivitySessionBuilder
    {
        private readonly ShiftAssignment _shiftAssignment;
        public ActivitySessionBuilder(ShiftAssignment shiftAssignment)
        {
            _shiftAssignment = shiftAssignment;
        }

        public List<ActivitySession> BuildActivitySessions(
            ShiftAssignment shift, 
            IReadOnlyList<Event> events)
        {
            if (events == null || events.Count == 0)
            {
                return new List<ActivitySession>();
            }

            var activitySessions = new List<ActivitySession>();

            ActivitySession? openSession = null;

            foreach (var evt in events.OrderBy(e => e.Timestamp))
            {
                switch (evt.EventType) { 
                
                    case "ShiftStarted":

                        CloseOpenSessionIfAny(evt.Timestamp, ref openSession, activitySessions);
                        openSession ??= new ActivitySession
                        {
                            ColleagueId = evt.ColleagueId,
                            DepartmentId = evt.DepartmentId,
                            ShiftAssignmentId = _shiftAssignment.Id,
                            SessionType = "Active",
                            SessionStart = TimeOnly.FromDateTime(evt.Timestamp)
                        };
                        break;

                    case "BreakStarted":
                            CloseOpenSessionIfAny(evt.Timestamp, ref openSession, activitySessions);
                        openSession = CreateSession(evt, "Break", _shiftAssignment);
                        break;

                    case "BreakEnded":
                            CloseOpenSessionIfAny(evt.Timestamp, ref openSession, activitySessions);
                            openSession = CreateSession(evt, "Active", _shiftAssignment);
                            break;

                     case "ShiftEnded":
                        CloseOpenSessionIfAny(evt.Timestamp, ref openSession, activitySessions);
                        break;

                    default:
                        // Unknown event type, ignore or log as needed
                        break;


                }
            }
            // Close any remaining open session at the end of the events
            if (openSession != null)
            {
                openSession.SessionEnd = TimeOnly.FromDateTime(events.Last().Timestamp);
                activitySessions.Add(openSession);
            }

            return activitySessions;
        }

        private static void CloseOpenSessionIfAny(DateTime timestamp, ref ActivitySession? openSession, 
            List<ActivitySession> activitySessions)
        {
            if (openSession == null) { 
            return;
            }
            openSession.SessionEnd = TimeOnly.FromDateTime(timestamp);
            activitySessions.Add(openSession);
            openSession = null;
        }

        private static ActivitySession CreateSession(Event evt, string sessionType, ShiftAssignment shiftAssignment)
        {
            return new ActivitySession
            {
                ColleagueId = evt.ColleagueId,
                DepartmentId = evt.DepartmentId,
                ShiftAssignmentId = shiftAssignment.Id,
                SessionType = sessionType,
                SessionStart = TimeOnly.FromDateTime(evt.Timestamp)
            };
        }


    }
}
