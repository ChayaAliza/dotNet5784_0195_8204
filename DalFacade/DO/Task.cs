namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Description"></param>
/// <param name="Allas"></param>
/// <param name="MilesStone"></param>
/// <param name="CreatedAt"></param>
/// <param name="Start"></param>
/// <param name="ScheduledDate"></param>
/// <param name="ForecastDate"></param>
/// <param name="DeadLine"></param>
/// <param name="Complete"></param>
/// <param name="Deliverables"></param>
/// <param name="Remarks"></param>
/// <param name="EngineerId"></param>
/// <param name="CopmlexityLevel"></param>
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



