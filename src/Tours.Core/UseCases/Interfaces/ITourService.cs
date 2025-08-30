using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Core.Domain.Entities.Tour;

namespace Tours.Core.UseCases.Interfaces;

public interface ITourService
{
    Result<Tour> Get(long id);
}

