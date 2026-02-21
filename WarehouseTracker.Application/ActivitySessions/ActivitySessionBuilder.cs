using WarehouseTracker.Api.Enums;
using WarehouseTracker.Application.ActivitySessions;
using WarehouseTracker.Domain;

public class ActivitySessionBuilder : IActivitySessionBuilder
{
    public List<ActivitySession> Build(TaskAssignment task, IReadOnlyList<Event> events)
    {
        var sessions = new List<ActivitySession>();

        if (events == null || events.Count == 0)
            return sessions;

        var orderedEvents = events.OrderBy(e => e.TimestampUtc).ToList();
        ActivitySession? openSession = null;

        foreach (var evt in orderedEvents)
        {
            EventTypes eventTypes;

            eventTypes = (EventTypes)evt.EventType;


            switch (eventTypes)
            {
                case EventTypes.CheckedIntoDepartment:
                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);
                    openSession = new ActivitySession
                    {
                        ColleagueId = evt.ColleagueId,
                        TaskAssignmentId = task.Id,
                        DepartmentId = evt.Id,
                        SessionType = "Active",
                        SessionStart = evt.TimestampUtc
                    };
                    break;

                case EventTypes.BreakStarted:
                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);
                    openSession = new ActivitySession
                    {
                        ColleagueId = evt.ColleagueId,
                        TaskAssignmentId = task.Id,
                        DepartmentId = null,
                        SessionType = "Break",
                        SessionStart = evt.TimestampUtc
                    };
                    break;

                case EventTypes.BreakEnded:
                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);
                    var lastDept = sessions.LastOrDefault(s => s.SessionType == "Active")?.DepartmentId;
                    openSession = new ActivitySession
                    {
                        ColleagueId = evt.ColleagueId,
                        TaskAssignmentId = task.Id,
                        DepartmentId = lastDept,
                        SessionType = "Active",
                        SessionStart = evt.TimestampUtc
                    };
                    break;

                case EventTypes.ShiftEnded:
                    CloseIfOpen(evt.TimestampUtc, ref openSession, sessions);
                    openSession = null;
                    break;
            }
        }

        // ✅ FIX: If there's an open session (ongoing break or active work), add it!
        if (openSession != null)
        {
            // For ongoing sessions, set SessionEnd to now (or task end if you prefer)
            openSession.SessionEnd = DateTimeOffset.UtcNow; // or task.ShiftEnd
            sessions.Add(openSession);
        }

        return sessions.OrderBy(s => s.SessionStart).ToList();
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