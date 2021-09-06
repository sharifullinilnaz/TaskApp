using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApp.Data.Models
{
    /// <summary>
    /// Task entity
    /// </summary>
    [Table("tasks")]
    public class Task
    {
        /// <summary>
        /// Task identifier. Unique key
        /// </summary>
        [Column("id", TypeName = "nvarchar(450)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Task name
        /// </summary>
        [Column("name", TypeName = "nvarchar(128)")]
        public string Name { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        [Column("description", TypeName = "nvarchar(128)")]
        public string Description { get; set; }

        /// <summary>
        /// Task status identifier.
        /// </summary>
        [Column("status_id", TypeName = "nvarchar(450)")]
        [ForeignKey("Status")]
        public string StatusId { get; set; }

        public Status Status { get; set; }
    }
}