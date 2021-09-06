using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApp.Data.Models
{
    /// <summary>
    /// Status entity
    /// </summary>
    [Table("statuses")]
    public class Status
    {

        /// <summary>
        /// Status identifier. Unique key
        /// </summary>
        [Column("status_id", TypeName = "nvarchar(450)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string StatusId { get; set; }

        /// <summary>
        /// Status name
        /// </summary>
        [Column("status_name", TypeName = "nvarchar(128)")]
        public string StatusName { get; set; }

    }
}