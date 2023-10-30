namespace DO;


public record Task
(
    int Id,
    string? Description = null,
    string? Allas = null,
    bool MilesStone = false,
    DateTime? CreatedAt = null,
    DateTime? Start = null,
    DateTime? ScheduledDate = null,
    DateTime? ForecastDate = null,
    DateTime? DeadLine = null,
    DateTime? Complete = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null
    //EngineerExperience? CopmlexityLevel
);

