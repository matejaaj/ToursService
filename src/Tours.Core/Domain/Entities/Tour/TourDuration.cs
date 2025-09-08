using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tours.Core.Domain.Entities.Tour;
public enum TransportType
{
    Car,
    Bike,
    Walking
}

public class TourDuration : ValueObject
{
    public TimeOnly Duration { get; }
    public TransportType TransportType { get; }

    [JsonConstructor]
    public TourDuration(TimeOnly duration, TransportType transportType)
    {
        Duration = duration;
        TransportType = transportType;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TransportType;
        yield return Duration;
    }

    public string ToString()
    {
        return $"{Duration} by {TransportType}";
    }
}
