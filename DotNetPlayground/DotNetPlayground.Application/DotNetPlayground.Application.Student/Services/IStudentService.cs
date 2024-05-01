using DotNetPlayground.Dto;

namespace DotNetPlayground.Services;

public interface IStudentService
{
    //Get all students
    Task<List<StudentDto>> GetStudentsAsync();
    //Add new student
    Task<StudentDto> AddStudentAsync(AddOrUpdateStudentDto orUpdateStudent);
    Task<StudentDto?> GetStudentByIdAsync(Guid id);
    Task<StudentDto?> UpdateStudentAsync(Guid id, AddOrUpdateStudentDto student);
    Task<StudentDto?> DeleteStudentByIdAsync(Guid id);
}