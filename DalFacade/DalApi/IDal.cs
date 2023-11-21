﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IDal
{
    IDependency Dependency { get; }
    ITask Task { get; }
    IEngineer Engineer { get; }

}
