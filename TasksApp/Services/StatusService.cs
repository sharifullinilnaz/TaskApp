using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using TasksApp.Data.Models;
using TasksApp.Data;
using TasksApp.Data.Response;
using AppTask = TasksApp.Data.Models.Task;
using System.Linq;

namespace TasksApp.Services
{
    /// <summary>
    /// Service that processes statsus-related actions
    /// </summary>
    public class StatusService : IStatusService
    {
        private readonly TaskAppDbContext _context;


        public StatusService(TaskAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method for task creating, add status.
        /// </summary>
        /// <param name="status">Status name</param>
        /// <returns>If task was created successfully, it returns a status model.</returns>
        public async Task<Response<Status>> CreateAsync(string status)
        {
            Status newStatus = new Status();
            newStatus.StatusName = status;
            await _context.Statuses.AddAsync(newStatus);
            await _context.SaveChangesAsync();
            return new Response<Status>
            {
                Succeeded = true,
                Data = newStatus
            };
        }

        /// <summary>
        /// Method for status hard deleting, delete status by id</summary>
        /// <param name="Id">The unique identifier of the status we try to hard delete</param>
        /// <returns></returns>
        public async Task<Response<Status>> DeleteAsync(string Id)
        {
            Status status = await _context.Statuses.FindAsync(Id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();
                return new Response<Status>
                {
                    Succeeded = true,
                    Data = status
                };
            }
            return new Response<Status>
            {
                Succeeded = false,
                Error = new Error { Code = "404", Message = "Status not found" }
            };
        }

        /// <summary>
        /// Method to get a list of all stauses available in the database
        /// </summary>
        /// <returns>If the request is processed successfully, the status list is returned.</returns>
        public async Task<Response<List<Status>>> GetAllAsync()
        {
            var statuses = await _context.Statuses.ToListAsync();
            return new Response<List<Status>>
            {
                Succeeded = true,
                Data = statuses
            };
        }

        /// <summary>
        /// Method for checking for the existence of a status in a table</summary>
        /// <param name="name">The name of the status we try to find</param>
        /// <returns></returns>
        public bool IsExist(string name)
        {
            Status status = _context.Statuses.Where(s => s.StatusName == name)
                       .FirstOrDefault<Status>();
            if (status == null)
            {
                return false;
            }
            return true;
        }


    }
}