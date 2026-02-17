using WarehouseTracker.Application.ActivitySessions;
using WarehouseTracker.Domain;

public class ActivitySessionBuilder : IActivitySessionBuilder
{
    public List<ActivitySession> Build(
        ShiftAssignment shift,
        IReadOnlyList<Event> events)
    {
        var sessions = new List<ActivitySession>();

        if (events == null || events.Count == 0)
            return sessions;

        var orderedEvents = events
            .OrderBy(e => e.TimestampUtc)
            .ToList();

        ActivitySession? openSession = null;

        foreach (var evt in orderedEvents)
        {
            switch (evt.EventType)
            {
                case "CheckedIntoDepartment":

                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);

                    openSession = new ActivitySession
                    {
                        ColleagueId = evt.ColleagueId,
                        ShiftAssignmentId = shift.Id,
                        DepartmentId = evt.Id,
                        SessionType = "Active",
                        SessionEnd = evt.TimestampUtc
                    };

                    break;

                case "BreakStarted":

                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);

                    openSession = new ActivitySession
                    {
                        ColleagueId = evt.ColleagueId,
                        ShiftAssignmentId = shift.Id,
                        DepartmentId = null,
                        SessionType = "Break",
                        SessionStart = evt.TimestampUtc
                    };

                    break;

                case "BreakEnded":
                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);
                    var lastDept = sessions.LastOrDefault(s => s.SessionType == "Active")?.DepartmentId;
                    
   
                        openSession = new ActivitySession
                        {
                            ColleagueId = evt.ColleagueId,
                            ShiftAssignmentId = shift.Id,
                            DepartmentId = lastDept,
                            SessionType = "Active",
                            SessionStart = evt.TimestampUtc
                        }; 
                        break;
                    

                case "ClockedOff":

                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);
                    openSession = null;

                    break;
            }
        }

        // Close remaining session at shift end
        if (openSession != null)
        {
            openSession.SessionEnd = shift.ShiftEnd;
            sessions.Add(openSession);
        }

        return sessions;
    }

    private static void CloseIfOpen(
        DateTimeOffset timestamp,
        ref ActivitySession? openSession,
        List<ActivitySession> sessions)
    {
        if (openSession == null)
            return;

        openSession.SessionEnd = timestamp;
        sessions.Add(openSession);
        openSession = null;
    }
}