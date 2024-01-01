using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal s_dal = Factory.Get;

    public int Create(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task(boTask.Id , boTask.Description , (DO.EngineerExperience)boTask.Level! , boTask.Alias , false
            , boTask.CreateAt , boTask.Start , boTask.ScheduledDate , boTask.ForecastDate , boTask.Deadline , boTask.Complete , boTask.Deliverables
            , boTask.Remarks , boTask.Engineer!.Id , boTask.IsActive);
        try
        {
            int idTask = s_dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task boTask)
    {
        throw new NotImplementedException();
    }
}
