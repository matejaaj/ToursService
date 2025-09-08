namespace Tours.Core.UseCases.Interfaces;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    long UserId { get; }
    long? PersonId { get; }
    string Role { get; }
}