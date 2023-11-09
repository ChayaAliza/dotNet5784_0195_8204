namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level"></param>
/// <param name="Cost"></param>
public record Engineer
(
      int Id,
      string? Name = null,
      string? Email = null,
      EngineerExperience? Level = null,
      double? Cost = null
)
{
   /* Engineer(int Id, string? Name, string? Email, EngineerExperience Level, double Cost)
   {
       this.Id = Id;
       this.Name = Name;
       this.Email = Email;
        this.Level = Level;
        this.Cost = Cost;
  }*/

}

