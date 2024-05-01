using AutoMapper;
using DotNetPlayground.Dto;
using DotNetPlayground.Models;
using DotNetPlayground.Repositories;
using DotNetPlayground.Services;

namespace DotNetPlayground.Api.Services;

public class CourseService(ICourseRepository courseRepository, IMapper mapper) : ICourseService
{
    public async Task<List<CourseDto>> GetCoursesAsync()
    {
        var courses = await courseRepository.GetCoursesAsync();
        return mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<CourseDto?> GetCourseAsync(Guid id)
    {
        var course = await courseRepository.GetCourseByIdAsync(id);
        return mapper.Map<CourseDto?>(course);
    }

    public async Task<CourseDto?> GetCourseAsync(int id)
    {
        var course = await courseRepository.GetCourseByIdAsync(id);
        return mapper.Map<CourseDto?>(course);
    }

    public async Task<CourseDto> AddCourseAsync(AddCourseDto course)
    {
        var newCourse = mapper.Map<Course>(course);
        var addedCourse = await courseRepository.AddCourseAsync(newCourse);
        return mapper.Map<CourseDto>(addedCourse);
    }

    public async Task<CourseDto?> UpdateCourseAsync(Guid id, UpdateCourseDto course)
    {
        var existingCourse = await courseRepository.GetCourseByIdAsync(id);
        if (existingCourse == null)
        {
            return null;
        }

        mapper.Map(course, existingCourse);
        var updatedCourse = await courseRepository.UpdateCourseAsync(id, existingCourse);
        return mapper.Map<CourseDto?>(updatedCourse);
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        await courseRepository.DeleteCourseAsync(id);
    }
}