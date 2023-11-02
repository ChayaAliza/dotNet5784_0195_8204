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
      // EngineerExperience? Level,
      double? Cost = null
);
