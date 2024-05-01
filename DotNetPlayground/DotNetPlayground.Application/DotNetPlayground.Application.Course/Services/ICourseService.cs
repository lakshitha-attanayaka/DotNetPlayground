using DotNetPlayground.Dto;

namespace DotNetPlayground.Services;

public interface ICourseService
{
    Task<List<CourseDto>> GetCoursesAsync();
    Task<CourseDto?> GetCourseAsync(Guid id);
    Task<CourseDto?> GetCourseAsync(int id);
    Task<CourseDto> AddCourseAsync(AddCourseDto course);
    Task<CourseDto?> UpdateCourseAsync(Guid id, UpdateCourseDto course);
    Task DeleteCourseAsync(Guid id);
}