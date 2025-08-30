using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Core.Domain.Entities.Tour;

namespace Tours.Core.Domain.RepositoryInterfaces;

public interface ITourRepository
{
    public Tour Get(long id);
}

