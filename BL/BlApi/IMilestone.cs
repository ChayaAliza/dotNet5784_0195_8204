namespace BlApi;
public interface IMilestone   
{
    public IEnumerable<DO.Dependency> Create();
    public BO.Milestone? Read(int id);
    public void Update(BO.Milestone boMilestone);
}
