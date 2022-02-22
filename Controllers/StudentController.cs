using Ass_10.Models;
using Ass_10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ass_10.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{

    private readonly ILogger<StudentController> _logger;

    private readonly IStudentService _studentService;
    public StudentController(ILogger<StudentController> logger, IStudentService studentService)
    {
        _logger = logger;
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entities = await _studentService.GetAllAsync();
        var result = from item in entities
                     select new StudentViewModel
                     {
                         Id = item.Id,
                         FullName = $"{item.LastName} {item.FirstName}",
                         City = item.City
                     };
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneAsync(int id)
    {
        var entity = await _studentService.GetOneAsync(id);
        if (entity == null) return NotFound();

        return new JsonResult(new StudentViewModel
        {
            Id = entity.Id,
            FullName = $"{entity.LastName} {entity.FirstName}",
            City = entity.City
        });
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(StudentCreateModel model)
    {
        try
        {

            var entity = new Data.Entities.Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = model.State
            };
            var result = await _studentService.AddAsync(entity);
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, StudentCreateModel model)
    {

        try
        {
            var student = await _studentService.GetOneAsync(id);

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.City = model.City;
            student.State = model.State;

            _studentService.Edit(id, student);

            return new JsonResult(student);
        }
        catch (Exception ex)
        {
            // return NotFound(ex);
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {

        try
        {
            _studentService.Remove(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }


    }
}
