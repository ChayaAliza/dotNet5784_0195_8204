using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class EngineerExperienceCollection : IEnumerable
    {
        // Enumerator for EngineerExperience values
        static readonly IEnumerable<BO.EngineerExperience> s_enums =
            (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

        // Returns an enumerator that iterates through the collection of EngineerExperience values
        public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
    }

    internal class StatusExperienceCollection : IEnumerable
    {
        // Enumerator for Status values
        static readonly IEnumerable<BO.Status> s_enums =
            (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

        // Returns an enumerator that iterates through the collection of Status values
        public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
    }
}
