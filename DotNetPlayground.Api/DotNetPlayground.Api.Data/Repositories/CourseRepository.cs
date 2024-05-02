using AutoMapper;
using DotNetPlayground.Models;
using DotNetPlayground.Repositories;
using DotNetPlayground.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace DotNetPlayground.Api.Repositories;

public class CourseRepository(CourseDbContext dbContext, IMapper mapper) : ICourseRepository
{
    public async Task<List<Course>> GetCoursesAsync()
    {
        return await dbContext.Courses.ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(Guid id)
    {
        return await dbContext.Courses.FindAsync(id);
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await dbContext.Courses.Where(x=>x.FriendlyId == id).SingleOrDefaultAsync();
    }

    public async Task<Course> AddCourseAsync(Course course)
    {
        var entity = dbContext.Courses.Add(course).Entity;
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Course?> UpdateCourseAsync(Guid id, Course course)
    {
        var entity = await dbContext.Courses.FindAsync(id);
        if (entity == null)
        {
            return null;
        }

        mapper.Map(course, entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteCourseAsync(Guid id)
    {
        var entity = await dbContext.Courses.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        dbContext.Courses.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
}