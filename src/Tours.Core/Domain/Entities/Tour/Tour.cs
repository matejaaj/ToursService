using Microsoft.AspNetCore.Mvc.ViewEngines;
using Object = Tours.Core.Domain.Entities.Tour.Object;

namespace Tours.Core.Domain.Entities.Tour;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public enum Status
{
    Draft,
    Published,
    Archived
}

public class Tour : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageData { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public List<string> Tags { get; private set; }
    public Price Price { get; private set; }
    public Status Status { get; private set; } = Status.Draft;
    public long AuthorId { get; private set; }
    public Distance TotalLength { get; private set; }
    public DateTime StatusChangeTime { get; private set; }
    public List<TourDuration> Durations { get; private set; }
    public List<Checkpoint> Checkpoints { get; private set; } = new();
    public List<Object> Objects { get; private set; }
    public List<Equipment> Equipment { get; private set; }
    public List<TourReview> Reviews { get; private set; }

    public Tour(string? name, string? description, string? imageData, Difficulty difficulty, List<string> tags, long authorId, Distance totalLength, List<TourDuration> durations, Price price)
    {
        Name = name;
        Description = description;
        ImageData = imageData;
        Difficulty = difficulty;
        Tags = new List<string>(tags);
        AuthorId = authorId;
        TotalLength = totalLength;
        Price = price;
        Durations = new List<TourDuration>(durations);
        Validate();
    }

    public Tour() { }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Description is required.");
        if (Tags == null || Tags.Count == 0) throw new ArgumentException("At least one tag is required.");

    }



    private bool CanArchive()
    {
        return Status == Status.Published;
    }

    public bool Archive()
    {
        if (!CanArchive())
            return false;
        StatusChangeTime = DateTime.UtcNow;
        Status = Status.Archived;
        return true;
    }

    public bool Publish()
    {
        if (!CanPublish())
            return false;

        StatusChangeTime = DateTime.UtcNow;
        Status = Status.Published;
        return true;

    }

    private bool ValidatePublishInfo()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description) && Tags.Count > 0 && Durations.Count > 0;
    }

    private bool CanPublish()
    {
        return Status != Status.Published && Checkpoints.Count >= 2 && ValidatePublishInfo();
        ;

    }

    public int GetCheckpointNum()
    {
        return Checkpoints?.Count ?? 0;
    }
}


