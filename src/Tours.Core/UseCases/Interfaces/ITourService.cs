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
        public Result<Tour> Create(Tour tour, long userId);
        Result<Checkpoint> CreateCheckpoint(Checkpoint checkpoint, long userId);
        public Result<List<Tour>> GetByAuthor(long id);
        public  Result<Tour> Get(long id);
        Result<Tour> GetPublished(long tourId);
        Result<Tour> GetById(long userId, long tourId);
}

