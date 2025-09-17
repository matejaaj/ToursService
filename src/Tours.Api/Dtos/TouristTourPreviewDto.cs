namespace Tours.Api.Dtos;

public class TouristTourPreviewDto
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DistanceDto TotalLength{ get; set; }
    public string ImageData { get; set; }
    public List<TourDurationDto> Durations { get; set; }
    public List<ReviewDto> Reviews { get; set; }
    public List<CheckpointDto> Checkpoints { get; set; }
}