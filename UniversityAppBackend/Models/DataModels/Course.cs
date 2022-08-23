using System.ComponentModel.DataAnnotations;

namespace UniversityAppBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string TargetPublic { get; set; } = string.Empty;
        public string Objectives { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;

        [DataType("smallint")]
        public CourseLevel CourseLevel { get; set; }
    }

    public enum CourseLevel: short 
    {
        Basico = 1,
        Intermedio = 2,
        Avanzado = 3
    }
}
