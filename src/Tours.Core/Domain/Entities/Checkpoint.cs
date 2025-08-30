using System.Data;
using System.Security.Claims;

namespace Tours.Core.Domain.Entities
{
    public class Checkpoint : Entity
  {
  public long Latitude { get; set; }

  public long Longitude { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }
  public string ImageBase64 { get; set; }
  public long TourId{ get; set; }

    public Checkpoint() { }

    public Checkpoint(string name, string description,long longitude,long latitude,string imageBase64, long tourId)
    {
      Name = name;
      Description = description;
      TourId = tourId;
      ImageBase64 = imageBase64;
      Latitude = latitude;
      Longitude = longitude;
      Validate();
    }

    public void Validate()
    {
      if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.");
      if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Description is required.");
    }

  }
}
