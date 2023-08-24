using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Univercity_Dashboard
{
    public class Student : User
    {
        public int StudentCode { get; set; }
        public static int Code { get; set; } = 1000000000;

        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Degree { get; set; }


        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Score> Scores { get; set; }



        public Student() { }

        public Student(string NationalCode, string Name, string Family, string PhoneNumber, string Password, Role RoleId, DateTime Birthdate, string Degree) : base(NationalCode, Name, Family, PhoneNumber, Password, RoleId, Birthdate)
        {
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                if (db.Students.Any())
                {
                    Code = db.Students.Max(t => t.StudentCode);
                }            
            }
            StudentCode = Code++;
            this.Degree = Degree;
            Courses = new List<Course>();
            Scores = new List<Score>();
        }



        public static implicit operator Master(Student student)
        {
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                int Id = db.Masters.Max(t => t.UserId);
                Master master = new Master()
                {
                    Name = student.Name,
                    Family = student.Family,
                    IsActive = student.IsActive,
                    NationalCode = student.NationalCode,
                    Password = student.Password,
                    RegisterDate = student.RegisterDate,
                    PhoneNumber = student.PhoneNumber,
                    Birthdate = student.Birthdate,
                    Courses = null,
                    Students = null,
                    Degree = null,
                    RoleId = db.Roles.Find(2),
                    UserId = Id++,
                    Salary = 5000000                  
                };
                return master;
            }
        }
        public static implicit operator Employee(Student student)
        {
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                int id = db.Employees.Max(t => t.UserId);
                Employee employee = new Employee()
                {
                    Birthdate = student.Birthdate,
                    Family = student.Family,
                    Name = student.Name,
                    NationalCode = student.NationalCode,
                    Password = student.Password,
                    RegisterDate = student.RegisterDate,
                    IsActive = student.IsActive,
                    PhoneNumber = student.PhoneNumber,
                    RoleId = db.Roles.Find(1),
                    UserId = id++,
                    Department = null,
                    EditScore = null,
                    Salary = 5000000
                };
                return employee;
            }
        }
    }
}
