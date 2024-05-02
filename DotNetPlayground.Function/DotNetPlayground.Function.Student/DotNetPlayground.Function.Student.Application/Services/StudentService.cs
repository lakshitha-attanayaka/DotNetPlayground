using AutoMapper;
using DotNetPlayground.Dto;
using DotNetPlayground.Repositories;
using DotNetPlayground.Services;

namespace DotNetPlayground.Function.Student.Services;

public class StudentService(IStudentRepository studentRepository, IMapper mapper) : IStudentService
{
    public async Task<List<StudentDto>> GetStudentsAsync()
    {
        var students = await studentRepository.GetStudentsAsync();
        return mapper.Map<List<StudentDto>>(students);
    }

    public Task<StudentDto> AddStudentAsync(AddOrUpdateStudentDto orUpdateStudent)
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto?> GetStudentByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto?> UpdateStudentAsync(Guid id, AddOrUpdateStudentDto student)
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto?> DeleteStudentByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}