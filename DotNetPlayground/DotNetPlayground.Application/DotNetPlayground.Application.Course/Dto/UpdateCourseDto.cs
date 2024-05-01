namespace DotNetPlayground.Dto;

public class UpdateCourseDto
{
    //Only update if the property is not null
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    
}