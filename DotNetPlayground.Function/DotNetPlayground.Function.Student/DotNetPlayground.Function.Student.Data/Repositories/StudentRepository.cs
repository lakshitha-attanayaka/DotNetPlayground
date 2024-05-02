using AutoMapper;
using DotNetPlayground.Repositories;
using DotNetPlayground.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace DotNetPlayground.Function.Student.Repositories;

public class StudentRepository(StudentDbContext dbContext, IMapper mapper) : IStudentRepository
{
    public Task<List<Models.Student>> GetStudentsAsync()
    {
        return dbContext.Students.ToListAsync();
    }

    public Task<Models.Student> AddStudentAsync(Models.Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Student?> GetStudentByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Student?> UpdateStudentAsync(Guid id, Models.Student studentEntity)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Student?> DeleteStudentByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}