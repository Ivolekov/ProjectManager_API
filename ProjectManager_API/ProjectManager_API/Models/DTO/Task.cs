namespace ProjectManager_API.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public class Task
    {
        public Task()
        {
            this.Id = System.Guid.NewGuid();
        }

        [Key]
        [Column(TypeName = "nvarchar(36)")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [StringLength(250)]
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [Range(0.1, 24)]
        public float WorkHours { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
