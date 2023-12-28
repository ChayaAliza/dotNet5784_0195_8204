using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public record  Task
(
    int Id,
    string Alias,
    string Description,
    DateTime CreatedAtDate,
    double RequiedEffort,
    bool Active=true,
    bool IsMilesStone=false,
    EngineerExperience Complexty=EngineerExperience.export,
    DateTime? Start = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadLine = null,
    DateTime? Complete = null,
     string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null
)
{
}
