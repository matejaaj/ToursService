namespace Tours.Api.Dtos;

public class TourExecutionDto
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public int TouristId { get; set; }
    public string Status { get; set; }
    public DateTime LastActivity { get; set; }
    public double Completion { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public TourExecutionDto() { }
}