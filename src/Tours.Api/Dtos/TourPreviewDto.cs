namespace Tours.Api.Dtos;

public class TourPreviewDto
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public PriceDto Price { get; set; }
}