namespace Tours.Core.Dtos;

public class TourDto
{
  public long? Id { get; set; }
  public long? AuthorId{ get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public string Difficulty { get; set; }

  public List<string> Tags { get; set; }
  public string? Status { get; set; }
  public long? Price { get; set; }


}