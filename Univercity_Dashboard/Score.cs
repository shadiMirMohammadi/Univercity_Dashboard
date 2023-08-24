using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univercity_Dashboard
{
    public class Score
    {
        public int Id { get; set; }
        public float ScoreNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Student Student { get; set; }
        public virtual Master Registerar { get; set; }
        public virtual Course Course { get; set; }
        public virtual Employee Edit { get; set; }

        public Score() { }
        public Score(int id, float scoreNumber, Student student, Master registerar, Course course, Employee EmployeeEditor)
        {
            Id = id;
            ScoreNumber = scoreNumber;
            RegisterDate = DateTime.Now;
            IsActive = true;
            Student = student;
            Registerar = registerar;
            Course = course;
            Edit = EmployeeEditor;
        }
        public Score(float scoreNumber, Student student, Master registerar, Course course, Employee EmployeeEditor)
        {
            ScoreNumber = scoreNumber;
            RegisterDate = DateTime.Now;
            IsActive = true;
            Student = student;
            Registerar = registerar;
            Course = course;
            Edit = EmployeeEditor;
        }
        public Score(int id, float scoreNumber, Student student, Master registerar, Course course)
        {
            Id = id;
            ScoreNumber = scoreNumber;
            RegisterDate = DateTime.Now;
            IsActive = true;
            Student = student;
            Registerar = registerar;
            Course = course;
            Edit = null;
        }
        public Score(float scoreNumber, Student student, Master registerar, Course course)
        {
            ScoreNumber = scoreNumber;
            RegisterDate = DateTime.Now;
            IsActive = true;
            Student = student;
            Registerar = registerar;
            Course = course;
            Edit = null;
        }


    }
}
