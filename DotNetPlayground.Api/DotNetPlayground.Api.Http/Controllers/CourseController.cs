using DotNetPlayground.Dto;
using DotNetPlayground.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPlayground.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    //Get all courses
    [HttpGet]
    [ProducesResponseType(typeof(IList<CourseDto>), 200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get()
    {
        return Ok(await courseService.GetCoursesAsync());
    }

    //Add new course
    [HttpPost]
    [ProducesResponseType(typeof(CourseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post([FromBody] AddCourseDto course)
    {
        return CreatedAtAction(nameof(Get), await courseService.AddCourseAsync(course));
    }

    //Get course by id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CourseDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var course = await courseService.GetCourseAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }
    
    //Get course by id
    [HttpGet("{id}/byfriendlyid")]
    [ProducesResponseType(typeof(CourseDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetByFriendlyId(int id)
    {
        var course = await courseService.GetCourseAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    //Update course by id
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CourseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCourseDto course)
    {
        var courseDto = await courseService.UpdateCourseAsync(id, course);
        if (courseDto == null)
        {
            return NotFound();
        }
        return Ok(courseDto);
    }

    //Delete course by id
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await courseService.DeleteCourseAsync(id);
        return NoContent();
    }
}