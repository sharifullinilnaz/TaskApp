using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using TasksApp.Data.Dtos;
using TasksApp.Data;
using TasksApp.Data.Response;
using AppTask = TasksApp.Data.Models.Task;

namespace TasksApp.Services
{
    /// <summary>
    /// Service that processes task-related actions
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly TaskAppDbContext _context;
        private readonly IMapper _mapper;


        public TaskService(TaskAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get a list of all tasks available in the database
        /// </summary>
        /// <returns>If the request is processed successfully, the DTO(display the task fields) list is returned.</returns>
        public async Task<Response<List<TaskResponseDto>>> GetAllAsync()
        {
            var tasksDto = await _context.Tasks.Include(t => t.Status).ProjectTo<TaskResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
            return new Response<List<TaskResponseDto>>
            {
                Succeeded = true,
                Data = tasksDto
            };
        }

        /// <summary>
        /// Method for task creating, add task using DTO, 
        /// which contains the required fields for task creating.
        /// </summary>
        /// <param name="taskDto">DTO for task creating</param>
        /// <returns>If task was created successfully, it returns a DTO to display the task model.</returns>
        public async Task<Response<TaskResponseDto>> CreateAsync(TaskCreateDto taskDto)
        {
            AppTask task = _mapper.Map<AppTask>(taskDto);         
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return new Response<TaskResponseDto>
            {
                Succeeded = true,
                Data = _mapper.Map<TaskResponseDto>(task)
            };
        }

        /// <summary>
        /// Method for task hard deleting, delete task by id</summary>
        /// <param name="Id">The unique identifier of the task we try to hard delete</param>
        /// <returns></returns>
        public async Task<Response<TaskResponseDto>> DeleteAsync(string Id)
        {
            AppTask task = await _context.Tasks.FindAsync(Id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return new Response<TaskResponseDto>
                {
                    Succeeded = true,
                    Data = _mapper.Map<TaskResponseDto>(task)
                };
            }
            return new Response<TaskResponseDto>
            {
                Succeeded = false,
                Error = new Error { Code = "404", Message = "Task not found" }
            };
        }

        /// <summary>
        /// Method for task updating, update task stream using DTO, 
        /// which contains the required fields for task updating.
        /// </summary>
        /// <param name="taskDto">DTO for task updating</param>
        /// <returns>If task was updated successfully, it returns a DTO to display the task model.</returns>
        public async Task<Response<TaskResponseDto>> Update(TaskUpdateDto taskDto)
        {
            AppTask taskToUpdate = await _context.Tasks.FindAsync(taskDto.Id);
            if (taskToUpdate != null)
            {
                _mapper.Map(taskDto, taskToUpdate);
                _context.Tasks.Update(taskToUpdate);
                await _context.SaveChangesAsync();
                AppTask taskUpdated = await _context.Tasks.FindAsync(taskDto.Id);
                return new Response<TaskResponseDto>
                {
                    Succeeded = true,
                    Data = _mapper.Map<TaskResponseDto>(taskUpdated)
                };
            }
            else
            {
                return new Response<TaskResponseDto>
                {
                    Succeeded = false,
                    Error = new Error { Code = "404", Message = "Task not found" }
                };
            }
        }

    }
}