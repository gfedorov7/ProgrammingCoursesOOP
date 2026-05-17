using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    /// <summary>
    /// Класс, представляющий запись о курсе в БД.
    /// </summary>
    public class ProgrammingCourse
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название курса.
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ProgrammingCourseName { get; set; }

        /// <summary>
        /// Имя автора.
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Тип курса.
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string TypeCourse { get; set; }

        /// <summary>
        /// Дата создания курса по программированию.
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Продолжительность курса.
        /// </summary>
        [Required]
        [Column(TypeName = "int")]
        public int Duration { get; set; }

        /// <summary>
        /// Стоимость курса.
        /// </summary>
        [Required]
        [Column(TypeName = "numeric")]
        public decimal CourseCost { get; set; }
    }
}
