using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Univercity_Dashboard
{
    public class Employee : User
    {
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Department { get; set; }

        public float Salary { get; set; }



        public virtual ICollection<Score> EditScore { get; set; }


        public Employee() { }
        /// <summary>
        /// متد سازنده کلاس کارمند
        /// </summary>
        /// <param name="Department">دپارتمان</param>
        /// <param name="Salary">حقوق کارمند</param>
        /// <param name="Name">نام کارمند</param>
        /// <param name="Family">نام خانوادگی کارمند</param>
        /// <param name="PhoneNumber">تلفن همراه کارمند</param>
        /// <param name="Password">پسوورد کارمند</param>
        /// <param name="RoleId">آیدی نقش کاربری کارمند</param>
        /// <param name="Birthdate">تاریخ تولد کارمند</param>
        public Employee(string NationalCode, string Name, string Family, string PhoneNumber, string Password, Role RoleId, DateTime Birthdate, string Department, float Salary) : base(NationalCode, Name, Family, PhoneNumber, Password, RoleId, Birthdate)
        {
            this.Department = Department;
            this.Salary = Salary;
        }


        public static implicit operator Master(Employee employee)
        {
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                int Id = db.Masters.Max(t => t.UserId);
                Master master = new Master()
                {
                    Name = employee.Name,
                    Family = employee.Family,
                    IsActive = employee.IsActive,
                    NationalCode = employee.NationalCode,
                    Password = employee.Password,
                    Salary = employee.Salary,
                    RegisterDate = employee.RegisterDate,
                    PhoneNumber = employee.PhoneNumber,
                    Birthdate = employee.Birthdate,
                    Courses = null,
                    Students = null,
                    Degree = null,
                    RoleId = db.Roles.Find(2),
                    UserId = Id++
                };
                return master;
            }
        }

        public static implicit operator Student(Employee employee)
        {
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                int id = db.Students.Max(t => t.UserId);
                int code = db.Students.Max(t => t.StudentCode);
                Student student = new Student()
                {
                    Birthdate = employee.Birthdate,
                    Name = employee.Name,
                    Family = employee.Family,
                    IsActive = employee.IsActive,
                    NationalCode = employee.NationalCode,
                    Password = employee.Password,
                    PhoneNumber = employee.PhoneNumber,
                    Courses = null,
                    Degree = null,
                    Scores = null,
                    RegisterDate = employee.RegisterDate,
                    RoleId = db.Roles.Find(3),
                    UserId = id++,
                    StudentCode =code++     
                };
                return student;
            }
        }

    }
}
