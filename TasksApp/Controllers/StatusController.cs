using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using TasksApp.Data.Models;
using TasksApp.Services;

namespace InternPlatform.Controllers
{
    /// <summary>
    /// The controller class that handles the actions associated with the statuses.
    /// </summary>
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// GET request method to get a list of all tasks available in the database
        /// </summary>
        /// <returns>If the request is processed successfully, the status list is returned, 
        /// else errors are displayed.</returns>
        [HttpGet(Name = "GetAllStatuses")]
        public async Task<ActionResult<IEnumerable<Status>>> Get()
        {
            return new ObjectResult(await _statusService.GetAllAsync());
        }

    }
}