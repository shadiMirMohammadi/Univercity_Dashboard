using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Univercity_Dashboard
{
    public class Master : User
    {
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Degree { get; set; }

        public float Salary { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }
        public virtual IEnumerable<Student> Students { get; set; }

        public Master() { }
        /// <summary>
        /// متد سازنده کلاس استاد
        /// </summary>
        /// <param name="Degree">مقطع تحصیلی استاد</param>
        /// <param name="Salary">حقوق استاد</param>
        /// <param name="Name">نام استاد</param>
        /// <param name="Family">نام خانوادگی استاد</param>
        /// <param name="PhoneNumber">تلفن همراه استاد</param>
        /// <param name="Password">پسورد استاد</param>
        /// <param name="RoleId">آیدی نقش کاربری استاد</param>
        /// <param name="Birthdate">تاریخ تولد استاد</param>
        public Master(string NationalCode, string Name, string Family, string PhoneNumber, string Password, Role RoleId, DateTime Birthdate, string Degree, float Salary) : base(NationalCode, Name, Family, PhoneNumber, Password, RoleId, Birthdate)
        {
            this.Degree = Degree;
            this.Salary = Salary;
            Courses = new List<Course>();
            Students = new List<Student>();
        }



        public static explicit operator Student(Master master)
        {
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                int id = db.Students.Max(t => t.UserId);
                int code = db.Students.Max(t => t.StudentCode);
                Student student = new Student()
                {
                    Birthdate = master.Birthdate,
                    Family = master.Family,
                    Name = master.Name,
                    NationalCode = master.NationalCode,
                    Password = master.Password,
                    RegisterDate = master.RegisterDate,
                    IsActive = master.IsActive,
                    PhoneNumber = master.PhoneNumber,
                    Degree = null,
                    Scores = null,
                    Courses = null,
                    RoleId = db.Roles.Find(3),
                    StudentCode = code++,
                    UserId = id++
                };
                return student;
                
            }

        }


        public static implicit operator Employee(Master master)
        {
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                int id = db.Employees.Max(t => t.UserId);
                Employee employee = new Employee()
                {
                    Birthdate = master.Birthdate,
                    Family = master.Family,
                    Name = master.Name,
                    NationalCode = master.NationalCode,
                    Password = master.Password,
                    RegisterDate = master.RegisterDate,
                    IsActive = master.IsActive,
                    PhoneNumber = master.PhoneNumber,
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
