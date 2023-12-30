﻿using DO;
using System.Data;

namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public EngineerExperience Level { get; init; }
    public double Cost { get; set; }
    public TaskInEngineer? Task { get; set; } = null;
}



