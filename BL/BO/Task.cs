using DO;

namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreateAt { get; set; } = null;
    public Status? Status { get; set; } = null;
    public MilestoneInTask? Milestone { get; set; } = null;
    public DateTime? Start { get; set; } = null;
    public DateTime? ScheduledDate { get; set; } = null; //???
    public DateTime? ForecastDate { get; set; } = null;
    public DateTime? Deadline { get; set; } = null;
    public DateTime? Complete { get; set; } = null;
    public string? Deliverables { get; set; } = null;
    public string? Remarks { get; set; } = null;
    public EngineerInTask? Engineer { get; set; } = null;
    public List<TaskInList>? Dependencies { get; set; } = null;
    public EngineerExperience? Level { get; set; } = null;
    public override string ToString() => this.ToStringProperty();
}