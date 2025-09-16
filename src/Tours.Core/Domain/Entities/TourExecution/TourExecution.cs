using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Core.Domain.Entities.Tour;

namespace Tours.Core.Domain.Entities.TourExecution;
public enum TourExecutionStatus
{
    ONGOING,
    ABANDONED,
    COMPLETED
};

public class TourExecution : Entity
{
    public long TourId { get; init; }
    public long TouristId { get; init; }
    public TourExecutionStatus Status { get; private set; }
    public DateTime LastActivity { get; private set; }
    public double Completion { get; private set; }
    public TouristPosition Position { get; private set; }
    public List<CompletedCheckpoint> CompletedCheckpoints { get; private set; } = new ();


    public TourExecution(long tourId, long touristId, double longitude, double latitude)
    {
        if (!Validate(longitude, latitude))
        {
            throw new ArgumentException("Invalid longitude or latitude values");
        }
        Position = new TouristPosition(longitude, latitude);
        CompletedCheckpoints = new List<CompletedCheckpoint>();
        TourId = tourId;
        TouristId = touristId;
        Status = TourExecutionStatus.ONGOING;
        LastActivity = DateTime.UtcNow;
        Completion = 0;
    }

    public TourExecution() { }

    public bool Validate(double latitude, double longitude)
    {
        return longitude is >= -180 and <= 180 && latitude is >= -90 and <= 90;
    }


    public void CompleteTour(double latitude, double longitude)
    {
        EnsureOngoing();        
        EnsureCompletionReady();

        this.Status = TourExecutionStatus.COMPLETED;
        this.Completion = 100;

        SetLastActivity(longitude, latitude);
    }


    public void AbandonTour(double latitude, double longitude)
    {
        EnsureOngoing(); 

        this.Status = TourExecutionStatus.ABANDONED;
        SetLastActivity(longitude, latitude);
    }

    private void EnsureOngoing()
    {
        if (this.Status != TourExecutionStatus.ONGOING)
        {
            throw new InvalidOperationException("Tour must be ongoing to complete/abandon.");
        }
    }

    private void EnsureCompletionReady()
    {
        if (this.Completion != 100)
        {
            throw new InvalidOperationException("Tour cannot be completed unless progress is 100%.");
        }
    }


    public bool TryCompleteNearestCheckpoint(
        IEnumerable<Checkpoint> checkpoints,
        double latitude,
        double longitude,
        double thresholdMeters = 10)
    {
        EnsureOngoing();
        SetLastActivity(longitude, latitude); 

        var pending = checkpoints
            .Where(cp => !CompletedCheckpoints.Any(c => c.CheckpointId == cp.Id))
            .ToList();

        if (!pending.Any()) return false;

        var best = pending
            .Select(cp => new
            {
                Cp = cp,
                Dist = cp.Location.DistanceTo(latitude, longitude)
            })
            .OrderBy(x => x.Dist)
            .First();

        if (!best.Cp.Location.IsNearby(latitude, longitude, thresholdMeters))
            return false;

        CompletedCheckpoints.Add(new CompletedCheckpoint(best.Cp.Id, DateTime.UtcNow));
        CalculateCompletion(checkpoints.Count());

        if (Completion == 100)
            Status = TourExecutionStatus.COMPLETED;

        return true;
    }

    public double CalculateCompletion(int totalCheckpointNum)
    {
        this.Completion = Math.Round((((double)CompletedCheckpoints.LongCount() / totalCheckpointNum) * 100), 2);
        if (this.Completion == 100)
            this.Status = TourExecutionStatus.COMPLETED;

        return this.Completion;
    }

    public void SetLastActivity(double longitude, double latitude)
    {
        this.LastActivity = DateTime.UtcNow;
        this.Position = new TouristPosition(longitude, latitude);
    }


}
