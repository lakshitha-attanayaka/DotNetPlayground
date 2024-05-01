using DotNetPlayground.Dto;
using DotNetPlayground.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPlayground.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    
    //Get all students
    [HttpGet]
    [ProducesResponseType(typeof(IList<StudentDto>), 200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get()
    {
        return Ok(await studentService.GetStudentsAsync());
    }
    
    //Add new student
    [HttpPost]
    [ProducesResponseType(typeof(StudentDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post([FromBody] AddOrUpdateStudentDto orUpdateStudent)
    {
        return CreatedAtAction(nameof(Get),await studentService.AddStudentAsync(orUpdateStudent));
    }
    
    //Get student by id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudentDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var student = await studentService.GetStudentByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }
    
    //Update student by id
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StudentDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Put(Guid id, [FromBody] AddOrUpdateStudentDto student)
    {
        var studentDto = await studentService.UpdateStudentAsync(id, student);
        if (studentDto == null)
        {
            return NotFound();
        }
        return Ok(studentDto);
    }
    
    //Delete student by id
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await studentService.DeleteStudentByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}