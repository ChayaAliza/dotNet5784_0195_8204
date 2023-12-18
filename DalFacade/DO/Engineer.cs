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
      double? Cost = null,
      bool active = false
      
    
)
{
    public Engineer() : this(0,"","",0,0,false) { }
}


