namespace Tours.Core.UseCases.Interfaces;

public interface ITourPaymentService
{
    Task<bool> HasUserBoughtTour(long userId, long tourId);
}