using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public record MilesStone
(
  int  Id,
  string Description,
  string Alias,
  string Status,
  DateTime CreatedAtDate,
  DateTime BaselineStartDate,
  DateTime  StartDate,
  DateTime ScheduledDate,
  DateTime ForecastDate,
  DateTime DeadlineDate,
  DateTime CompleteDate,
  double CompletionPercentage,
  string Remarks
//List&lt; TaskInList&gt;? Dependencies { get; set; }
//public override string? ToString() =&gt;$&quot;Milestone(ID:M{ Id})&quot;;
)
{
}
