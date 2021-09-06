using System.Collections.Generic;
using System.Threading.Tasks;

using TasksApp.Data.Models;
using TasksApp.Data.Response;

namespace TasksApp.Services
{
    /// <summary>
    /// Interface for a service that processes status-related actions
    /// </summary>
    public interface IStatusService
    {
        Task<Response<List<Status>>> GetAllAsync();
        Task<Response<Status>> CreateAsync(string status);
        Task<Response<Status>> DeleteAsync(string statusId);
        bool IsExist(string name);
    }
}
