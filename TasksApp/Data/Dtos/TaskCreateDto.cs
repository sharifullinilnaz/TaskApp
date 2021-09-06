using System.ComponentModel.DataAnnotations;

namespace TasksApp.Data.Dtos
{
    /// <summary>
    /// DTO for task creation form
    /// </summary>
    public class TaskCreateDto
    {
        /// <summary>
        /// Task name
        /// </summary>
        [Required(ErrorMessage = "Task name not specified")]
        public string Name { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        [Required(ErrorMessage = "Task description not specified")]
        public string Description { get; set; }

        /// <summary>
        /// Task status identifier.
        /// </summary>
        [Required(ErrorMessage = "Task status not specified")]
        public string StatusId { get; set; }
    }
}
