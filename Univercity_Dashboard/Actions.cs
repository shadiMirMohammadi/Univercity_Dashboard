using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace Univercity_Dashboard
{
    public static class Actions
    {
        #region JustDateTime
        public static Action JustDateTime = delegate
        {
            Console.Clear();
            PersianCalendar pc = new PersianCalendar();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"    {pc.GetYear(DateTime.Now)}/{pc.GetMonth(DateTime.Now)}/{pc.GetDayOfMonth(DateTime.Now)}  {pc.GetHour(DateTime.Now)}:{pc.GetMinute(DateTime.Now)}\t\t\t\t     Pannell University");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        };
        #endregion

        #region DateTimeWithUserStatistic
        public static Action<string> DateTimeWithUserStatistic = delegate (string name)
        {
            Console.Clear();
            PersianCalendar pc = new PersianCalendar();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"    {pc.GetYear(DateTime.Now)}/{pc.GetMonth(DateTime.Now)}/{pc.GetDayOfMonth(DateTime.Now)}  {pc.GetHour(DateTime.Now)}:{pc.GetMinute(DateTime.Now)}\t\t\t\t     Pannell University\t\t\t\t\t  {name}");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            using (UnivercityContext dbPrintDateTime = new UnivercityContext("UniDBConStr"))
            {
                Console.WriteLine($"\n\t\t\t\tEmployees : {dbPrintDateTime.Employees.Count()}\t  Master : {dbPrintDateTime.Masters.Count()}\t  Student : {dbPrintDateTime.Students.Count()}\t  Course : {dbPrintDateTime.Courses.Count()}");
            }
            Console.WriteLine("\t\t\t____________________________________________________________________________");
        };
        #endregion

        #region DateTimeWithoutUserStatistic
        public static Action<string> DateTimeWithoutUserStatistic = delegate (string name)
        {
            Console.Clear();
            PersianCalendar pc = new PersianCalendar();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"    {pc.GetYear(DateTime.Now)}/{pc.GetMonth(DateTime.Now)}/{pc.GetDayOfMonth(DateTime.Now)}  {pc.GetHour(DateTime.Now)}:{pc.GetMinute(DateTime.Now)}\t\t\t\t     Pannell University\t\t\t\t\t  {name}");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        };
        #endregion


        #region WarningJustNumber
        public static Action<string> WarningJustNumber = delegate (string Title)
        {
            JustDateTime();
            Console.WriteLine("\n\t\t\t\t\t\t\t  Warning!");
            Thread.Sleep(1000);
            Console.WriteLine($"\n\t\t\t\t\t  Only number are approved for {Title}! ");
            Thread.Sleep(3000);
            JustDateTime();
        };
        #endregion

        #region WarningNotFoundId
        public static Action<string, int> WarningNotFoundId = delegate (string TitleList, int id)
        {
            JustDateTime();
            Console.WriteLine("\n\t\t\t\t\t\t\t  Warning!");
            Thread.Sleep(1000);
            Console.WriteLine($"\n\t\t\t\t\t\tNot Found Id [{id}] in {TitleList}! ");
            Thread.Sleep(3000);
            JustDateTime();
        };
        #endregion

        #region IncorrectPhonenumber
        public static Action IncorrectPhonenumber = delegate ()
        {
            JustDateTime();
            Console.WriteLine("\n\n\t\t\t\t\t\tIncorrect phonenumber");
            Thread.Sleep(3000);
            JustDateTime();
        };
        #endregion

        #region Incorrect Birthdate
        public static Action IncorrectBirthdate = delegate ()
        {
            JustDateTime();
            Console.WriteLine("\n\n\t\t\t\t\t\tIncorrect Birthdate");
            Thread.Sleep(3000);
            JustDateTime();
        };
        #endregion

        #region Incorrect National Code
        public static Action IncorrectNationalCode = delegate ()
        {
            JustDateTime();
            Console.WriteLine("\n\n\t\t\t\t\t\tIncorrect National Code");
            Thread.Sleep(3000);
            JustDateTime();
        };
        #endregion

        #region DuplicateItem
        public static Action<string, string> DuplicateItem = delegate (string newItem, string section)
        {
            JustDateTime();
            Console.WriteLine($"\n\t\t\t\t\t   {newItem} is already reserved for this section");
            Console.WriteLine($"\n\t\t\t\t\t\t   Please enter a new {section}");
            Thread.Sleep(3000);
            JustDateTime();

        };
        #endregion

        #region AlreadySelected
        public static Action AlreadySelected = delegate ()
        {
            JustDateTime();
            Console.WriteLine("\n\t\t\t\t\t\t\t\tYou have already selected all courses");
            Thread.Sleep(3000);
            JustDateTime();
        };
        #endregion

        #region return the menu
        public static Action ReturnTheMenu = delegate ()
        {
            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
            Console.ReadKey();
        };
        #endregion

        #region PrintEmployees
        public static void PrintEmployees()
        {
            using (UnivercityContext dbViewEmployees = new UnivercityContext("UniDBConStr"))
            {
                List<Employee> employeeList = dbViewEmployees.Employees.ToList();
                foreach (var employee in employeeList)
                {
                    Console.WriteLine($"\n\tId : {employee.UserId}    Name : {employee.Name}    Family : {employee.Family}    Phonenumber : {employee.PhoneNumber}    Password : {employee.Password}   Salary : {employee.Salary}\n\n\tDepartment : {employee.Department}   Is active : {employee.IsActive}   Birthdate : {employee.Birthdate}   RegisterDate : {employee.RegisterDate} ");
                    Console.WriteLine("\n\t    ______________________________________________________________________________________________________");
                }
            }
        }
        #endregion

        #region PrintStudents
        public static void PrintStudents()
        {
            using (UnivercityContext dbViewStudents = new UnivercityContext("UniDBConStr"))
            {
                List<Student> studentList = dbViewStudents.Students.ToList();
                foreach (var student in studentList)
                {
                    Console.WriteLine($"\n\tId : {student.UserId}    Code : {student.StudentCode}    Name : {student.Name}    Family : {student.Family}     Phonenumber : {student.PhoneNumber}     Password : {student.Password}\n\n\t    Is active : {student.IsActive}     Birthdate : {student.Birthdate}    RegisterDate : {student.RegisterDate} ");
                    if (student.Courses.Any())
                    {
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine("\n\t           *************************************************************************************");
                            Console.WriteLine($"\t\t\t\t\t\tName :{course.CourseName}    Unit :{course.CourseUnit}");
                            foreach (var master in course.Masters)
                            {
                                Console.Write($"\t\t\t\t\t       Master : {master.Name} {master.Family}\n");
                            }
                        }
                    }
                    Console.WriteLine("\n\t    ______________________________________________________________________________________________________");
                }
            }
        }
        #endregion

        #region PrintMasters
        public static void PrintMasters()
        {
            using (UnivercityContext dbViewMasters = new UnivercityContext("UniDBConStr"))
            {
                List<Master> mastersList = dbViewMasters.Masters.ToList();
                foreach (var master in mastersList)
                {
                    Console.WriteLine($"\n\tId : {master.UserId}     Name : {master.Name}     Family : {master.Family}     Phonenumber : {master.PhoneNumber}     Password : {master.Password}      Salary :{master.Salary}\n\n\tDegree : {master.Degree}     Is active : {master.IsActive}     Birthdate : {master.Birthdate}     RegisterDate : {master.RegisterDate} ");
                    Console.WriteLine("\n\t    ______________________________________________________________________________________________________");

                }
            }
        }
        #endregion

        #region  PrintCourse
        public static void PrintCourse()
        {
            using (UnivercityContext dbViewCourses = new UnivercityContext("UniDBConStr"))
            {
                List<Course> coursesList = dbViewCourses.Courses.ToList();
                foreach (var course in coursesList)
                {
                    Console.WriteLine($"\n\t\tId :{course.CourseId}    Name :{course.CourseName}    Unit :{course.CourseUnit}    Is active:{course.IsActive}    Registerdate : {course.Registerdate}     ");
                    foreach (var master in course.Masters)
                    {
                        Console.Write($"\n\t\t\t\t\t\tMaster : {master.Name} {master.Family}\n");
                    }
                    Console.WriteLine("\n\t    ______________________________________________________________________________________________________");
                }
            }
        }


        #endregion



        #region AddEmployee
        public static Action AddEmployee = delegate ()
        {
            using (UnivercityContext dbAddEmployee = new UnivercityContext("UniDBConStr"))
            {
                JustDateTime();
                Console.WriteLine("\n\t\t\t\t\t\tNew employee registration form");
                Console.Write("\n\t\t\t\t\t\tDepartmant name :");
                string addDepartmentName = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tMonthly salary  :");
                float addSalary;
                while (!float.TryParse(Console.ReadLine(), out addSalary))
                {
                    WarningJustNumber("Monthly salary");
                    Console.WriteLine("\n\t\t\t\t\t\tNew employee registration form");
                    Console.Write($"\n\t\t\t\t\t\tDepartmant name : {addDepartmentName}");
                    Console.Write("\n\t\t\t\t\t\tMonthly salary  :");
                }
                Console.Write("\t\t\t\t\t\tPhoneNumber     :");
                string addPhoneNumber = Console.ReadLine();
                string patternPhoneNumber = @"^09[0-9]{9}$";
                while (!Regex.IsMatch(addPhoneNumber, patternPhoneNumber))
                {
                    IncorrectPhonenumber();
                    Console.WriteLine("\n\t\t\t\t\t\tNew employee registration form");
                    Console.Write($"\n\t\t\t\t\t\tDepartmant name : {addDepartmentName}");
                    Console.Write($"\n\t\t\t\t\t\tMonthly salary  : {addSalary}");
                    Console.Write("\n\t\t\t\t\t\tPhoneNumber     :");
                    addPhoneNumber = Console.ReadLine();
                }
                Console.Write("\t\t\t\t\t\tPassword        :");
                string eAddPassword = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tBirthdate       :");
                DateTime addBirthdate;
                while (!DateTime.TryParse(Console.ReadLine(), out addBirthdate))
                {
                    IncorrectBirthdate();
                    Console.WriteLine("\n\t\t\t\t\t\tNew employee registration form");
                    Console.Write($"\n\t\t\t\t\t\tDepartmant name : {addDepartmentName}");
                    Console.Write($"\n\t\t\t\t\t\tMonthly salary  : {addSalary}");
                    Console.Write($"\n\t\t\t\t\t\tPhoneNumber     : {addPhoneNumber}");
                    Console.Write($"\n\t\t\t\t\t\tPassword        : {eAddPassword}");
                    Console.Write("\n\t\t\t\t\t\tBirthdate       :");
                }
                Console.Write("\t\t\t\t\t\tFirst name      :");
                string addFirstName = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tLast name       :");
                string addLastName = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tNational Code       :");
                string addNationalCode = Console.ReadLine();
                string patternNationalCode = "^[0-9]{10}$";
                while (!Regex.IsMatch(addNationalCode, patternNationalCode))
                {
                    IncorrectNationalCode();
                    Console.WriteLine("\n\t\t\t\t\t\tNew employee registration form");
                    Console.Write($"\n\t\t\t\t\t\tDepartmant name : {addDepartmentName}");
                    Console.Write($"\n\t\t\t\t\t\tMonthly salary  : {addSalary}");
                    Console.Write($"\n\t\t\t\t\t\tPhoneNumber     : {addPhoneNumber}");
                    Console.Write($"\n\t\t\t\t\t\tPassword        : {eAddPassword}");
                    Console.Write($"\n\t\t\t\t\t\tBirthdate       : {addBirthdate}");
                    Console.Write($"\n\t\t\t\t\t\tFirstName       : {addFirstName}");
                    Console.Write($"\n\t\t\t\t\t\tLastName        : {addLastName}");
                    Console.Write("\n\t\t\t\t\t\tNationalCode    :");
                    addNationalCode = Console.ReadLine();
                }
                dbAddEmployee.Employees.Add(new Employee(addNationalCode, addFirstName, addLastName, addPhoneNumber, eAddPassword, dbAddEmployee.Roles.Find(1), addBirthdate, addDepartmentName, addSalary));
                Console.WriteLine($"\n\t\t\t\t\t\t{addFirstName} {addLastName} was registered\n");
                dbAddEmployee.SaveChanges();
            }
        };
        #endregion

        #region AddStudent
        public static Action AddStudent = delegate ()
        {
            UnivercityContext dbNewStudent = new UnivercityContext("UniDBConStr");
            using (dbNewStudent)
            {
                JustDateTime();
                Console.WriteLine("\n\t\t\t\t\t\tNew student registration form");
                Console.Write("\n\t\t\t\t\t\tPhoneNumber :");
                string addPhoneNumber = Console.ReadLine();
                string patternPhoneNumber = @"^09[0-9]{9}$";
                while (!Regex.IsMatch(addPhoneNumber, patternPhoneNumber))
                {
                    IncorrectPhonenumber();
                    Console.WriteLine("\n\t\t\t\t\t\tNew student registration form");
                    Console.Write("\n\t\t\t\t\t\tPhoneNumber :");
                    addPhoneNumber = Console.ReadLine();
                }
                Console.Write("\t\t\t\t\t\tPassword    :");
                string addPassword = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tBirthdate   :");
                DateTime addBirthdate;
                while (!DateTime.TryParse(Console.ReadLine(), out addBirthdate))
                {
                    IncorrectBirthdate();
                    Console.WriteLine("\n\t\t\t\t\t\tNew student registration form");
                    Console.Write($"\n\t\t\t\t\t\tPhoneNumber : {addPhoneNumber}");
                    Console.Write($"\n\t\t\t\t\t\tPassword    : {addPassword}");
                    Console.Write("\n\t\t\t\t\t\tBirthdate   :");
                }
                Console.Write("\t\t\t\t\t\tFirstname   :");
                string addFirstName = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tLastname    :");
                string addLastName = Console.ReadLine();         
                Console.Write("\t\t\t\t\t\tNationalCode:");
                string addNationalCode = Console.ReadLine();
                string patternNationalCode = "^[0-9]{10}$";
                while (!Regex.IsMatch(addNationalCode,patternNationalCode))
                {
                    IncorrectNationalCode();
                    Console.WriteLine("\n\t\t\t\t\t\tNew student registration form");
                    Console.Write($"\n\t\t\t\t\t\tPhoneNumber : {addPhoneNumber}");
                    Console.Write($"\n\t\t\t\t\t\tPassword    : {addPassword}");
                    Console.Write($"\n\t\t\t\t\t\tBirthdate   : {addBirthdate}");
                    Console.Write($"\n\t\t\t\t\t\tFirstName   : {addFirstName}");
                    Console.Write($"\n\t\t\t\t\t\tLastName    : {addLastName}");
                     Console.Write("\n\t\t\t\t\t\tNationalCode: ");
                    addNationalCode = Console.ReadLine();
                }
                Console.Write("\t\t\t\t\t\tDegree      :");
                string addDegree = Console.ReadLine();
                dbNewStudent.Students.Add(new Student(addNationalCode, addFirstName, addLastName, addPhoneNumber, addPassword, dbNewStudent.Roles.Find(3), addBirthdate, addDegree));
                dbNewStudent.SaveChanges();
                Console.WriteLine($"\n\t\t\t\t\t\t{addFirstName} {addLastName} was registered\n");
            }
        };
        #endregion

        #region AddMaster
        public static void AddMaster()
        {
            UnivercityContext dbAddMaster = new UnivercityContext("UniDBConStr");
            using (dbAddMaster)
            {
                JustDateTime();
                Console.WriteLine("\n\t\t\t\t\t\tNew master registration form");
                Console.Write("\n\t\t\t\t\t\tPhoneNumber    : ");
                string addPhoneNumber = Console.ReadLine();
                string patternPhoneNumber = @"^09[0-9]{9}$";
                while (!Regex.IsMatch(addPhoneNumber, patternPhoneNumber))
                {
                    IncorrectPhonenumber();
                    Console.WriteLine("\n\t\t\t\t\t\tNew master registration form");
                    Console.Write("\n\t\t\t\t\t\tPhoneNumber    : ");
                    addPhoneNumber = Console.ReadLine();
                }
                Console.Write("\t\t\t\t\t\tPassword       : ");
                string addPassword = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tMonthly salary : ");
                float addSalary;
                while (!float.TryParse(Console.ReadLine(), out addSalary))
                {
                    WarningJustNumber("Monthly salary");
                    Console.WriteLine("\n\t\t\t\t\t\tNew master registration form");
                    Console.Write($"\n\t\t\t\t\t\tPhoneNumber    : {addPhoneNumber}");
                    Console.Write($"\n\t\t\t\t\t\tPassword       : {addPassword}");
                    Console.Write("\n\t\t\t\t\t\tMonthly salary : ");
                }
                Console.Write("\t\t\t\t\t\tBirthdate      : ");
                DateTime addBirthdate;
                while (!DateTime.TryParse(Console.ReadLine(), out addBirthdate))
                {
                    IncorrectBirthdate();
                    Console.WriteLine("\n\t\t\t\t\t\tNew master registration form");
                    Console.Write($"\n\t\t\t\t\t\tPhoneNumber    : {addPhoneNumber}");
                    Console.Write($"\n\t\t\t\t\t\tPassword       : {addPassword}");
                    Console.Write($"\n\t\t\t\t\t\tMonthly salary : {addSalary}");
                     Console.Write("\n\t\t\t\t\t\tBirthdate      : ");
                }
                Console.Write("\t\t\t\t\t\tFirst name     :");
                string addFirstName = Console.ReadLine();

                Console.Write("\t\t\t\t\t\tLast name      :");
                string addLastName = Console.ReadLine();

                Console.Write("\t\t\t\t\t\tNationalCode:");
                string addNationalCode = Console.ReadLine();
                string patternNationalCode = "^[0-9]{10}$";
                while (!Regex.IsMatch(addNationalCode, patternNationalCode))
                {
                    IncorrectNationalCode();
                    Console.WriteLine("\n\t\t\t\t\t\tNew master registration form");
                    Console.Write($"\n\t\t\t\t\t\tPhoneNumber    : {addPhoneNumber}");
                    Console.Write($"\n\t\t\t\t\t\tPassword       : {addPassword}");
                    Console.Write($"\n\t\t\t\t\t\tMonthly salary : {addSalary}");
                    Console.Write($"\n\t\t\t\t\t\tBirthdate      : {addBirthdate}");
                    Console.Write($"\n\t\t\t\t\t\tFirst Name     : {addFirstName}");
                    Console.Write($"\n\t\t\t\t\t\tLast Name      : {addLastName}");
                     Console.Write("\n\t\t\t\t\t\tNationalCode   : ");
                    addNationalCode = Console.ReadLine();
                }
                Console.Write("\t\t\t\t\t\tDegree         :");
                string addDegree = Console.ReadLine();
                dbAddMaster.Masters.Add(new Master(addNationalCode, addFirstName, addLastName, addPhoneNumber, addPassword, dbAddMaster.Roles.Find(3), addBirthdate, addDegree, addSalary));
                dbAddMaster.SaveChanges();
                Console.WriteLine($"\n\t\t\t\t\t\t{addFirstName} {addLastName} was registered\n");
            }
        }

        #endregion

        #region AddCourse
        public static void AddCourse()
        {
            UnivercityContext dbAddNewCourse = new UnivercityContext("UniDBConStr");
            using (dbAddNewCourse)
            {
                JustDateTime();
                Console.WriteLine("\n\t\t\t\t\t\tNew Course registration form");
                Console.Write("\n\t\t\t\t\t\tCourse name : ");
                string addCorseName = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tCourse unit : ");
                int addCourseUnit;
                while (!int.TryParse(Console.ReadLine(), out addCourseUnit))
                {
                    WarningJustNumber("Course unit");
                    Console.WriteLine("\n\t\t\t\t\t\tNew Course registration form");
                    Console.Write($"\n\t\t\t\t\t\tCourse name : {addCorseName}");
                    Console.Write("\n\t\t\t\t\t\tCourse unit : ");
                }
                dbAddNewCourse.Courses.Add(new Course(addCorseName, addCourseUnit));
                dbAddNewCourse.SaveChanges();

                Console.WriteLine($"\n\n\t\t\t\t\t\t{addCorseName} was registered\n");
            }
        }
        #endregion




        //public static void P(List<Course> coursesList)
        //{
        //    using (UnivercityContext dbViewCourses = new UnivercityContext("UniDBConStr"))
        //    {
        //        Console.WriteLine("\nCourse : \n");
        //        foreach (var course in coursesList)
        //        {
        //            Console.WriteLine($"\tId :{course.CourseId}    Name :{course.CourseName}    Unit :{course.CourseUnit}");
        //            if (course.Masterss.Any())
        //            {

        //                Console.Write("Master : ");
        //                foreach (var master in course.Masterss)
        //                {
        //                    Console.WriteLine($"\n\tId : {master.UserId}     Name : {master.Name}     Family : {master.Family}");
        //                }
        //            }
        //            Console.WriteLine("\n____________________________________________________________________________________________________________________________\n");
        //        }
        //    }

        //}



    }
}
