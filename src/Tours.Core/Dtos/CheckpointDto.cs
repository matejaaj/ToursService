namespace Tours.Core.Dtos;

public class CheckpointDto
{
  public long Latitude { get; set; }

  public long Longitude { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }
  public string ImageBase64 { get; set; }
  public long TourId{ get; set; }

}