namespace BO;

public class Milestone
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime CreateAt { get; set; }
    public Status? Status { get; set; } = null;
    public DateTime? ForecastDate { get; set; } = null;
    public DateTime? Deadline { get; set; } = null;
    public DateTime? Complete { get; set; } = null;
    public double? CompletionPercentage { get; set; } = null;
    public string? Remarks { get; set; } = null;
    public List<TaskInList> Dependencies { get; set; } = new List<TaskInList>();
}

