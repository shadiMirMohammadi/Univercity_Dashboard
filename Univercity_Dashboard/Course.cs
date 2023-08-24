using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Univercity_Dashboard
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar")]
        public string CourseName { get; set; }


        [Required]
        public int CourseUnit { get; set; }
        public DateTime Registerdate { get; set; }
        public bool IsActive { get; set; }



        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Master> Masters { get; set; }
        public virtual ICollection<Score> Scores { get; set; }


        public Course() { }

        public Course(int CourseId, string CourseName, int CourseUnit)
        {
            this.CourseId = CourseId;
            this.CourseName = CourseName;
            this.CourseUnit = CourseUnit;
            Registerdate = DateTime.Now;
            IsActive = true;
            Students = new List<Student>();
            Masters = new List<Master>();
            Scores = new List<Score>();
        }


        public Course(string CourseName, int CourseUnit)
        {
            this.CourseName = CourseName;
            this.CourseUnit = CourseUnit;
            Registerdate = DateTime.Now;
            IsActive = true;
            Students = new List<Student>();
            Masters = new List<Master>();
            Scores = new List<Score>();
        }
  
    }
}
