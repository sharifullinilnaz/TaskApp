using TasksApp.Data.Models;

namespace TasksApp.Data.Dtos
{
    /// <summary>
    /// DTO to display the task model
    /// </summary>
    public class TaskResponseDto
    {
        /// <summary>
        /// Task identifier. Unique key
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Task name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Task status
        /// </summary>
        public Status Status { get; set; }

    }
}
