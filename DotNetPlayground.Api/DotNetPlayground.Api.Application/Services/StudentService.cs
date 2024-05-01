using AutoMapper;
using DotNetPlayground.Dto;
using DotNetPlayground.Models;
using DotNetPlayground.Repositories;
using DotNetPlayground.Services;

namespace DotNetPlayground.Api.Services;

public class StudentService(IStudentRepository studentRepository, IMapper mapper) : IStudentService
{
    public async Task<List<StudentDto>> GetStudentsAsync()
    {
        var students = await studentRepository.GetStudentsAsync();
        return mapper.Map<List<StudentDto>>(students);
    }

    public async Task<StudentDto> AddStudentAsync(AddOrUpdateStudentDto orUpdateStudentDto)
    {
        var studentEntity = mapper.Map<Student>(orUpdateStudentDto);
        var student = await studentRepository.AddStudentAsync(studentEntity);
        return mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto?> GetStudentByIdAsync(Guid id)
    {
        var student = await studentRepository.GetStudentByIdAsync(id);
        return mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto?> UpdateStudentAsync(Guid id, AddOrUpdateStudentDto student)
    {
        var studentEntity = mapper.Map<Student>(student);
        var updatedStudent = await studentRepository.UpdateStudentAsync(id, studentEntity);
        return mapper.Map<StudentDto>(updatedStudent);
    }

    public async Task<StudentDto?> DeleteStudentByIdAsync(Guid id)
    {
        var student = await studentRepository.DeleteStudentByIdAsync(id);
        return mapper.Map<StudentDto>(student);
    }
}