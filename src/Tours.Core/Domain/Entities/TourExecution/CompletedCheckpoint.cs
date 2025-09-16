using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tours.Core.Domain.Entities.TourExecution;
public class CompletedCheckpoint : ValueObject
{
    public long CheckpointId { get; init; }
    public DateTime CompletionTime { get; init; }

    [JsonConstructor]
    public CompletedCheckpoint(long checkpointId, DateTime completionTime)
    {
        CheckpointId = checkpointId;
        CompletionTime = completionTime;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CheckpointId;
        yield return CompletionTime;
    }
}
