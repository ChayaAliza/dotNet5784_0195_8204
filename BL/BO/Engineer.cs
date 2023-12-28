using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public record Engineer
(
     int Id,
      string? Name = null,
      string? Email = null,
      EngineerExperience? Level = EngineerExperience.Junior,
      double? Cost = 0,
      bool active = true

)
{
}
