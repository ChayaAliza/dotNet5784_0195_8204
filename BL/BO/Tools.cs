using BlApi;
using BO;

namespace BO;

public static class Tools
{

    private DalApi.IDal s_dal = Factory.Get;
    public static Status CalculateStatus(DateTime? start, DateTime? forecastDate, DateTime? deadline, DateTime? complete)
    {
        if (start == null && deadline == null)
            return Status.Unscheduled;

        if (start != null && deadline != null && complete == null)
            return Status.Scheduled;

        if (start != null && complete != null && complete <= forecastDate)
            return Status.OnTrack;

        if (start != null && complete != null && complete > forecastDate)
            return Status.InJeopardy;

        return Status.Unscheduled;
    }
   
}
