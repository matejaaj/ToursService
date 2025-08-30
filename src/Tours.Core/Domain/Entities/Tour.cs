using System.Data;
using System.Security.Claims;

namespace Tours.Core.Domain.Entities
{
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
    public string Name { get; set; }

    public string Description { get; set; }

    public Difficulty Difficulty { get; set; }

    public List<string> Tags { get; set; }
    public Status Status { get; private set; } = Status.Draft;
    public long Price { get; set; } = 0;
    public long AuthorId { get; set; }
    public Tour() { }

    public Tour(string name, string description, Difficulty difficulty, List<string> tags, long authorId,long price)
    {
      Name = name;
      Description = description;
      Difficulty = difficulty;
      Tags = new List<string>(tags);
      AuthorId = authorId;
      Price = price;
      Validate();
    }

    public void Validate()
    {
      if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.");
      if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Description is required.");
      if (Tags == null || Tags.Count == 0) throw new ArgumentException("At least one tag is required.");
      if (Price <0) throw new ArgumentException("Price cannot be negative");

    }

  }
}
