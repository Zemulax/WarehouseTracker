# WarehouseTracker

A .NET Web API for tracking warehouse colleague activity, task assignments, breaks, and work sessions in real time.

## About

WarehouseTracker solves the problem of tracking colleague movements and activity in a warehouse environment. It records when colleagues sign in, which departments they work in, when they take breaks, and builds a full activity timeline for each workday.

## Architecture

The solution follows a clean layered architecture:

```
WarehouseTracker.Api           → Controllers, DTOs, entry point
WarehouseTracker.Application   → Services, interfaces, business logic
WarehouseTracker.Domain        → Domain models
WarehouseTracker.Infrastructure → Repositories, DbContext, EF Core
WarehouseTracker.Tests         → Unit tests
```

## Core Concepts

### WorkDay
Represents a colleague being present at work for a given day. Everything hangs off the WorkDay — task assignments, events, and activity sessions. A WorkDay is created when a colleague signs in and closed when they sign out.

### TaskAssignment
Represents what a colleague is currently doing (e.g. Pick, Pack, Dispatch). A colleague can only have one active task at a time. When they check into a new department, the current task is closed and a new one is opened.

### Events
Things that happen during a workday — checking into a department, break started, break ended, shift ended. Events are the source of truth from which activity sessions are rebuilt.

### ActivitySession
Built from events by the `ActivitySessionBuilder`. Represents continuous periods of activity — either `Active` (working in a department) or `Break`. Sessions are rebuilt every time a new event is recorded.

### BreakSchedulerService
A background service that runs every 30 seconds and automatically fires `BreakStarted` and `BreakEnded` events based on configured break rules.

## Flow

```
Colleague signs in
  → WorkDay created (Status: Active)

Colleague checks into department
  → TaskAssignment created (e.g. Pick)
  → CheckedIntoDepartment event saved
  → ActivitySessions rebuilt

Break time reached
  → BreakSchedulerService fires BreakStarted event
  → WorkDay Status → OnBreak
  → ActivitySessions rebuilt (Break session added)

Break ends
  → BreakSchedulerService fires BreakEnded event
  → WorkDay Status → Active
  → ActivitySessions rebuilt (Active session resumed with previous department)

Colleague signs out
  → Open TaskAssignment closed
  → WorkDay closed (Status: Completed)
```

## API Endpoints

### WorkDay
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/workday/signin` | Sign a colleague in, creates a WorkDay |
| POST | `/api/workday/signout` | Sign a colleague out, closes WorkDay and active task |
| GET | `/api/workday/active/{colleagueId}` | Get the active WorkDay for a colleague |

### TaskAssignment
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/taskassignment` | Check a colleague into a department/task |

### Event
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/event` | Record a manual event for a colleague |

### ActivitySession
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/activitysession` | Get activity sessions by workDayId or colleagueId |

### Colleague
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/colleague` | Register a new colleague |
| GET | `/api/colleague` | Get all colleagues |
| GET | `/api/colleague/{id}` | Get colleague by ID |

## Domain Models

### WorkDay
```csharp
public class WorkDay
{
    public int Id { get; set; }
    public string ColleagueId { get; set; }
    public DateTimeOffset WorkDayStart { get; set; }
    public DateTimeOffset? WorkDayEnd { get; set; }
    public string Status { get; set; } // Active, OnBreak, Completed
    public ICollection<TaskAssignment> TaskAssignments { get; set; }
}
```

### TaskAssignment
```csharp
public class TaskAssignment
{
    public int Id { get; set; }
    public int WorkDayId { get; set; }
    public string ColleagueId { get; set; }
    public string DepartmentCode { get; set; }
    public DateTimeOffset TaskStart { get; set; }
    public DateTimeOffset? TaskEnd { get; set; }
    public string Status { get; set; } // Active, Completed
}
```

### ActivitySession
```csharp
public class ActivitySession
{
    public int Id { get; set; }
    public string ColleagueId { get; set; }
    public int WorkDayId { get; set; }
    public string? DepartmentCode { get; set; }
    public string SessionType { get; set; } // Active, Break
    public DateTimeOffset SessionStart { get; set; }
    public DateTimeOffset SessionEnd { get; set; }
}
```

## Getting Started

### Prerequisites
- .NET 8
- SQL Server
- Entity Framework Core

### Setup

1. Clone the repository:
```bash
git clone https://github.com/Zemulax/WarehouseTracker.git
```

2. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=WarehouseTracker;Trusted_Connection=True;"
  }
}
```

3. Apply migrations:
```bash
dotnet ef database update
```

4. Run the API:
```bash
dotnet run --project WarehouseTracker.Api
```

## Key Design Decisions

**Services vs Repositories** — Repositories handle data access only. Services contain business logic, validation, and orchestration. For example, `WorkDayService.StartWorkDayAsync` checks for an existing open workday before creating a new one — logic that belongs in the service, not the repository.

**Server-side resolution** — Callers provide minimal data (e.g. `ColleagueId`, `DepartmentCode`). The server resolves internal concerns like `WorkDayId` automatically, keeping the API surface clean.

**Event-sourced sessions** — `ActivitySessions` are not manually managed. They are fully rebuilt from the event log every time a new event is recorded, ensuring the session timeline always accurately reflects what happened.

**Background break scheduling** — Breaks are automatically triggered by `BreakSchedulerService` based on configurable `BreakRule` records, removing the need for manual break events.
