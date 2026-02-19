using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WarehouseTracker.Api.Enums;
using WarehouseTracker.Application.Services;

public class BreakSchedulerService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BreakSchedulerService> _logger;

    public BreakSchedulerService(
        IServiceScopeFactory scopeFactory,
        ILogger<BreakSchedulerService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckAndTriggerBreaksAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in break scheduler");
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // Check every 30s
        }
    }


    private async Task CheckAndTriggerBreaksAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();

        var breakService = scope.ServiceProvider.GetRequiredService<IBreakRuleService>();
        var shiftService = scope.ServiceProvider.GetRequiredService<IShiftAssignmentService>();
        var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();

        var now = DateTimeOffset.UtcNow;
        var activeShifts = await shiftService.GetShiftsAsync(now);
        var breakRules = await breakService.GetAllBreakRules();

        foreach (var shift in activeShifts)
        {
            var existingEvents = await eventService.GetEventsByShiftAsync(shift.Id);

            foreach (var breakRule in breakRules)
            {
                var shiftDate = shift.ShiftStart.Date;
                var breakStart = new DateTimeOffset(shiftDate.Year, shiftDate.Month, shiftDate.Day,
                    breakRule.BreakStart.Hour, breakRule.BreakStart.Minute, 0, TimeSpan.Zero);
                var breakEnd = new DateTimeOffset(shiftDate.Year, shiftDate.Month, shiftDate.Day,
                    breakRule.BreakEnd.Hour, breakRule.BreakEnd.Minute, 0, TimeSpan.Zero);

                if (breakStart < shift.ShiftStart) breakStart = breakStart.AddDays(1);
                if (breakEnd < shift.ShiftStart) breakEnd = breakEnd.AddDays(1);

                // ✅ FIXED: Check for BreakStarted (not BreakEnded!)
                var breakStartExists = existingEvents.Any(e =>
                    e.EventType == EventTypes.BreakStarted &&
                    Math.Abs((e.TimestampUtc - breakStart).TotalMinutes) < 1);

                if (!breakStartExists && Math.Abs((now - breakStart).TotalSeconds) < 60)
                {
                    _logger.LogInformation($" ====================Triggering break start for {shift.ColleagueId} at {now} ========================");
                    await eventService.CreateBreakStartedEventAsync(shift);
                }

                // Check if break end already exists
                var breakEndExists = existingEvents.Any(e =>
                    e.EventType == EventTypes.BreakEnded &&
                    Math.Abs((e.TimestampUtc - breakEnd).TotalMinutes) < 1);

                if (!breakEndExists && Math.Abs((now - breakEnd).TotalSeconds) < 60)
                {
                    _logger.LogInformation($"=====================Triggering break end for {shift.ColleagueId} at {now}=================");
                    await eventService.CreateBreakEndedEventAsync(shift);
                }
            }
        }
    }
}