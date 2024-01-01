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
      EngineerExperience? Level = EngineerExperience.Junior ,
      double? Cost = 0,
      bool active = true
      
    
)
{
    public Engineer() : this(0, null, null, new EngineerExperience(),0,true) { }
}


