using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Core.Domain.Entities;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.RepositoryInterfaces;

namespace Tours.Infrastructure.Database.Repositories;

public class TourRepository : CrudRepository<Tour, ToursContext>, ITourRepository
{
    public TourRepository(ToursContext dbContext) : base(dbContext) { }

    public Tour GetById(long id)
    {
        var tour = DbContext.Tours
            .Include(t => t.Checkpoints)
            .FirstOrDefault(t => t.Id == id);

        if (tour == null)
            throw new KeyNotFoundException("Not found: " + id);

        return tour;
    }

    public List<Tour> GetByAuthor(long id)
    {
        return DbContext.Tours.Where(tour => tour.AuthorId == id).ToList();
    }
    public Tour GetByIdWithReviews(long id)
    {
        var tour = DbContext.Tours
            .Include(t => t.Checkpoints)
            .Include(t => t.Reviews)
            .FirstOrDefault(t => t.Id == id);

        if (tour == null)
            throw new KeyNotFoundException("Not found: " + id);

        return tour;
    }

}

