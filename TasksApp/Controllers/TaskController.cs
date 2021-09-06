using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using TasksApp.Data.Dtos;
using TasksApp.Services;

namespace InternPlatform.Controllers
{
    /// <summary>
    /// The controller class that handles the actions associated with the tasks.
    /// </summary>
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// GET request method to get a list of all tasks available in the database
        /// </summary>
        /// <returns>If the request is processed successfully, the DTO(display the task fields) list is returned, 
        /// else errors are displayed.</returns>
        [HttpGet(Name = "GetAllTasks")]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> Get()
        {
            return new ObjectResult(await _taskService.GetAllAsync());
        }

        /// <summary>
        /// Method for POST request for task creating, add task using DTO, 
        /// which contains the required fields for task creating.
        /// </summary>
        /// <param name="taskDto">DTO for task creating</param>
        /// <returns>If task was created successfully, it returns a DTO to display the task model, 
        /// else it displays errors.</returns>
        /// <response code="400">Returned when the invalid form sent</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDto taskDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskService.CreateAsync(taskDto);
                if (result.Succeeded)
                {
                    return new ObjectResult(result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid form fields.");
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// DELETE request method to hard delete a specific task by his unique identifier
        /// </summary>
        /// /// <param name="id">The unique identifier of the task we try to delete</param>
        /// <returns></returns>
        /// <response code="404">Returned when the entity was not found</response>
        [HttpDelete("delete/{id}", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var result = await _taskService.DeleteAsync(id);
            if (result.Succeeded)
            {
                return new ObjectResult(result);
            }
            else
            {
                return result.Error.Code switch
                {
                    "404" => NotFound(result.Error.Message),
                    _ => StatusCode(500)
                };
            }
        }

        /// <summary>
        /// Method for PUT request for task update </summary>
        /// <param name="taskDto">DTO for task update</param>
        /// <returns>If task was updates successfully, it returns a DTO to display the task model, 
        /// else it displays errors.</returns>
        /// <response code="404">Returned when the entity was not found</response>
        /// <response code="400">Returned when the invalid form sent</response>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskUpdateDto taskDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskService.Update(taskDto);
                if (result.Succeeded)
                {
                    return new ObjectResult(result);
                }
                else
                {
                    return result.Error.Code switch
                    {
                        "404" => NotFound(result.Error.Message),
                        _ => StatusCode(500)
                    };
                }
            } 
            return BadRequest(ModelState);
        }

    }
}