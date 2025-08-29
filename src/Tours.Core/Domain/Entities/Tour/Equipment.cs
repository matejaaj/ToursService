﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tours.Core.Domain.Entities.Tour;
public class Equipment : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }

    public Equipment(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        Name = name;
        Description = description;
    }
}
