using System.ComponentModel.DataAnnotations;

namespace UniversityAppBackend.Models.DataModels
{
    public class Student : BaseEntity
    {        
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDay { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
