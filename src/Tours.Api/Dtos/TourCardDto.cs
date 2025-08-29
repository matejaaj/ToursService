namespace Tours.Api.Dtos;

public class TourCardDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double AverageRating { get; set; }
    public DistanceDto Distance { get; set; }
    public List<TourDurationDto> Durations { get; set; }
    public int ReviewsCount { get; set; }
    public string Difficulty { get; set; }


    public TourCardDto(long id, string name, double price, DistanceDto distance, double averageRating, string difficulty, int reviewsCount, List<TourDurationDto> durations)
    {
        Durations = durations;
        Id = id;
        Name = name;
        Price = price;
        ReviewsCount = reviewsCount;
        Distance = distance;
        Difficulty = difficulty;
        AverageRating = averageRating;
    }
}

