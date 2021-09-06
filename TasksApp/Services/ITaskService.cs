using System.Collections.Generic;
using System.Threading.Tasks;

using TasksApp.Data.Dtos;
using TasksApp.Data.Response;

namespace TasksApp.Services
{
    /// <summary>
    /// Interface for a service that processes task-related actions
    /// </summary>
    public interface ITaskService
    {
        Task<Response<List<TaskResponseDto>>> GetAllAsync();
        Task<Response<TaskResponseDto>> CreateAsync(TaskCreateDto taskDto);
        Task<Response<TaskResponseDto>> DeleteAsync(string taskId);
        Task<Response<TaskResponseDto>> Update(TaskUpdateDto taskDto);
    }
}
