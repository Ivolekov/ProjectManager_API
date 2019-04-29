namespace ProjectManager_API.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Project
    {
        public Project()
        {
            this.Id = System.Guid.NewGuid();
        }
        [Key]
        [Column(TypeName = "nvarchar(36)")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public DateTime? CreateDate { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
