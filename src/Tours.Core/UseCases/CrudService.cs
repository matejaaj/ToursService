using Tours.Core.Domain.Entities;
using FluentResults;

namespace Tours.Core.UseCases;

public abstract class CrudService<TDomain>  where TDomain : Entity
{
    protected readonly ICrudRepository<TDomain> CrudRepository;

    protected CrudService(ICrudRepository<TDomain> crudRepository)
    {
        CrudRepository = crudRepository;
    }

    public Result<PagedResult<TDomain>> GetPaged(int page, int pageSize)
    {
        return CrudRepository.GetPaged(page, pageSize);
    }

    public Result<TDomain> Get(int id)
    {
        try
        {
            return CrudRepository.Get(id);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public virtual Result<TDomain> Create(TDomain entity)
    {
        try
        {
            return CrudRepository.Create(entity);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public virtual Result<TDomain> Update(TDomain entity)
    {
        try
        {
            return CrudRepository.Update(entity);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public virtual Result Delete(int id)
    {
        try
        {
            CrudRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}