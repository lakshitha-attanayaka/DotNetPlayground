using AutoMapper;
using DotNetPlayground.Dto;
using DotNetPlayground.Models;

namespace DotNetPlayground;

public class StudentAutoMapperProfile : Profile
{
    public StudentAutoMapperProfile()
    {
        CreateMap<Student, StudentDto>()
            .ReverseMap();
        CreateMap<AddOrUpdateStudentDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
    }
}