using DotNetPlayground.Models;

namespace DotNetPlayground.Repositories;

public interface IStudentRepository
{
    //Get all students
    Task<List<Student>> GetStudentsAsync();
    
    //Add new student
    Task<Student> AddStudentAsync(Student student);
    Task<Student?> GetStudentByIdAsync(Guid id);
    Task<Student?> UpdateStudentAsync(Guid id, Student studentEntity);
    Task<Student?> DeleteStudentByIdAsync(Guid id);
}