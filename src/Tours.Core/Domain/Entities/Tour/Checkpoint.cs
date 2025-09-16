using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tours.Core.Domain.Entities.Tour;

public class Checkpoint : Entity
{
    public long? TourId { get;  set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageData { get; private set; }
    public Location Location { get; private set; }
    public string Secret { get; private set; }
    public List<long> EncounterIds { get; private set; }



    public bool IsPublic { get; private set; }



    public Checkpoint() { }
    public Checkpoint(string? name, string? description, string? imageData, long? tourId, Location location, string secret)
    {
        Name = name;
        Description = description;
        ImageData = imageData;
        Location = location;
        TourId = tourId;
        Secret = secret;
        EncounterIds = new List<long>();

        Validate();

    }
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (string.IsNullOrWhiteSpace(ImageData)) throw new ArgumentException("Invalid image data");

    }

}