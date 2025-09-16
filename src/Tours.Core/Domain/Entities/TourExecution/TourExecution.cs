using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public bool Validate(double longitude, double latitude)
    {
        return longitude is >= -180 and <= 180 && latitude is >= -90 and <= 90;
    }

    public void Finalize(string status)
    {
        if (Enum.TryParse<TourExecutionStatus>(status, true, out var parsedStatus))
        {
            Status = parsedStatus;
            LastActivity = DateTime.UtcNow;
        }
        else
        {
            throw new ArgumentException($"Invalid status value: {status}");
        }
    }

    public void CompleteCheckpoint(int checkpointId, int checkpointNum)
    {
        foreach (CompletedCheckpoint check in CompletedCheckpoints)
        {
            if (check.CheckpointId == checkpointId)
                return;
        }
        LastActivity = DateTime.UtcNow;
        CompletedCheckpoint checkpoint = new CompletedCheckpoint(checkpointId, DateTime.UtcNow);
        CompletedCheckpoints.Add(checkpoint);
        this.CalculateCompletion(checkpointNum);
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
