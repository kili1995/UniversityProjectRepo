using System.ComponentModel.DataAnnotations;

namespace University.DataAccess.Models.DataModels
{
    public class BaseEntity
    {
        [Required, Key]        
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
