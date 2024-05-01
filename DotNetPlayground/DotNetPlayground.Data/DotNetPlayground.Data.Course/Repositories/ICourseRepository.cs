using DotNetPlayground.Models;

namespace DotNetPlayground.Repositories;

public interface ICourseRepository
{
    //Get all courses
    Task<List<Course>> GetCoursesAsync();
    //Get course by id
    Task<Course?> GetCourseByIdAsync(Guid id);
    //Get course by id
    Task<Course?> GetCourseByIdAsync(int id);
    //Add new course
    Task<Course> AddCourseAsync(Course course);
    //Update course by id
    Task<Course?> UpdateCourseAsync(Guid id, Course course);
    //Delete course by id
    Task<bool> DeleteCourseAsync(Guid id);
}