using DotNetPlayground.Models;
using DotNetPlayground.Repositories;
using DotNetPlayground.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace DotNetPlayground.Api.Repositories;

public class StudentRepository(StudentDbContext dbContext) : IStudentRepository
{
    public Task<List<Student>> GetStudentsAsync()
    {
        return dbContext.Students.ToListAsync();
    }

    public async Task<Student> AddStudentAsync(Student student)
    { 
        var studentEntity = await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
        return studentEntity.Entity;
    }

    public Task<Student?> GetStudentByIdAsync(Guid id)
    {
        return dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Student?> UpdateStudentAsync(Guid id, Student studentEntity)
    {
        var student = dbContext.Students.FirstOrDefault(x => x.Id == id);
        if (student == null)
        {
            return null;
        }
        student.Name = studentEntity.Name;
        student.DateOfBirth = studentEntity.DateOfBirth;
        student.Email = studentEntity.Email;
        dbContext.Students.Update(student);
        await dbContext.SaveChangesAsync();
        return student;
    }
    
    public async Task<Student?> DeleteStudentByIdAsync(Guid id)
    {
        var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student == null)
        {
            return null;
        }
        dbContext.Students.Remove(student);
        await dbContext.SaveChangesAsync();
        return student;
    }
}