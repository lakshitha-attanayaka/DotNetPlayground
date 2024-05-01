namespace DotNetPlayground.Dto;

public class CourseDto
{
    public Guid Id { get; set; }
    public int FriendlyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = string.Empty;
}