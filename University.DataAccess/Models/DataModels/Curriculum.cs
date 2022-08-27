namespace University.DataAccess.Models.DataModels
{
    using System.ComponentModel.DataAnnotations;

    public class Curriculum : BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();

        [Required]
        public string Chapters { get; set; } = string.Empty;
    }
}
