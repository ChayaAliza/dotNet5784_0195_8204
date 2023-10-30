namespace DO;

/// <param name="Id">Personal unique ID of the engineer (as in national id card)</param>
/// <param name="Name">Private Name of the engineer</param>
/// <param name="Email"></param>

public record Engineer
(
      int Id,
      string? Name = null,
      string? Email = null,
      //EngineerExperience? Level,
      double? Cost = null
);