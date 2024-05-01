using AutoMapper;
using DotNetPlayground.Dto;
using DotNetPlayground.Models;

namespace DotNetPlayground;

public class CourseAutoMapperProfile : Profile
{
    public CourseAutoMapperProfile()
    {
        //Map Course to CourseDto and vice versa
        CreateMap<Course, CourseDto>()
            .ReverseMap();
        //Map AddCourseDto to Course
        CreateMap<AddCourseDto, Course>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
        //Map UpdateCourseDto to Course ignoring Id and ignore if the property is null
        CreateMap<UpdateCourseDto, Course>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}