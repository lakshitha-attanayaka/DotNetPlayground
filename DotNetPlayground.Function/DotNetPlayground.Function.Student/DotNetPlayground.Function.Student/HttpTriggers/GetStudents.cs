using System.Net;
using DotNetPlayground.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DotNetPlayground.Function.Student.HttpTriggers;

public class GetStudents(ILoggerFactory loggerFactory, IStudentService studentService)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<GetStudents>();

    [Function("Students")]
    public async Task<HttpResponseData> Students([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request");

        var students = await studentService.GetStudentsAsync();
        //Add students to response
        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(students);

        return response;
        
    }
}