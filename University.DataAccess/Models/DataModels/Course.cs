using System.ComponentModel.DataAnnotations;

namespace University.DataAccess.Models.DataModels
{
    public class Course : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        
        public string TargetPublic { get; set; } = string.Empty;
        public string Objectives { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;

        [DataType("smallint")]
        public CourseLevel CourseLevel { get; set; } = CourseLevel.Basic;

        [Required]
        public ICollection<Category> CourseCategories { get; set; } = new List<Category>();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public Curriculum Curriculum { get; set; } = new Curriculum();
       
    }

    public enum CourseLevel: short 
    {
        Basic = 1,
        Medium = 2,
        Advanced = 3,
        Expert = 4
    }
}
