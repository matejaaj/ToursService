namespace Tours.Api.Dtos;
public class CheckpointDto
{
    public int Id { get; set; }
    public int? TourId { get; set; }
    public LocationDto Location { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageData { get; set; }

    public string Secret { get; set; }

}

