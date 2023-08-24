using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Univercity_Dashboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Defualt Data
            Console.WriteLine("\n\n\n\n\n\t\n\t\t\t\t\t\tDatabase is loading ...");
            using (UnivercityContext db = new UnivercityContext("UniDBConStr"))
            {
                db.Database.CreateIfNotExists();
                Console.WriteLine();
                if (!db.Roles.Any())
                {
                    db.Roles.Add(new Role(0, "admin"));
                    db.Roles.Add(new Role(1, "employee"));
                    db.Roles.Add(new Role(2, "master"));
                    db.Roles.Add(new Role(3, "student"));
                }
                if (!db.Admins.Any())
                {
                    db.Admins.Add(new Admin("0312443802", "shadi", "daraye", "09918915659", "12345", db.Roles.Find(0), new DateTime(1990, 09, 03)));
                }

                if (!db.Employees.Any())
                {
                    db.Employees.Add(new Employee("0594324575", "mahdi", "darabi", "09918901234", "12345", db.Roles.Find(1), new DateTime(1982, 04, 23), "support_1", 7000000));
                }

                if (!db.Courses.Any())
                {
                    db.Courses.Add(new Course("C Sharp", 3));
                    Console.Clear();
                    Console.WriteLine("\n\n\t\t\t\tDatabase created successfully!");
                }
                if (!db.Students.Any())
                {
                    db.Students.Add(new Student("0754678762", "kosar", "saberi", "09110124532", "10903", db.Roles.Find(3), new DateTime(2001, 11, 06), "It"));
                }
                if (!db.Masters.Any())
                {
                    db.Masters.Add(new Master("0981276593", "hamid", "sadati", "09104567009", "20074", db.Roles.Find(2), new DateTime(1982, 10, 01), "Asp.Net", 8000000));
                }
                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\t\t\t\t\t\tRedirecting to login form ");
                Console.WriteLine("\n\t\t\t\t\t\t     Please wait ... ");
                Thread.Sleep(4000);
                Console.Clear();
            }
            #endregion

            #region Pattern
            string patternPhoneNumber = @"^09[0-9]{9}$";
        //string patternNationalCode = "^[0-9]{10}$";
        #endregion

        #region Login
        loginMenu:
            Actions.JustDateTime();
            Console.Write("\n\t\t\t1. Admin\t\t2. Employee\t\t3. Master\t\t4. Student");
            Console.Write("\n\n\n\t\t\t\t\t\t\t  Login as : ");
            int answerMenu;
            while (!int.TryParse(Console.ReadLine(), out answerMenu))
            {
                Console.Clear();
                goto loginMenu;
            }
            #endregion

            switch (answerMenu)
            {
                #region Admin
                case 1:

                #region  Logged Admin
                AdminLogin:
                    Actions.JustDateTime();
                    Console.Write("\n\n\n\t\t\t\t\t\tPhonenumber : ");
                    string adminUsername = Console.ReadLine();
                    Console.Write("\n\t\t\t\t\t\tPassword    : ");
                    string adminPassword = Console.ReadLine();
                    string adminName = "";
                    using (UnivercityContext dbAdminLogin = new UnivercityContext("UniDBConStr"))
                    {
                        var admin = dbAdminLogin.Admins.FirstOrDefault(t => t.PhoneNumber == adminUsername && t.Password == adminPassword);
                        if (!Regex.IsMatch(adminUsername, patternPhoneNumber))
                        {
                            Actions.IncorrectPhonenumber();
                            goto AdminLogin;
                        }
                        else
                        {
                            adminName = admin.Name +" "+ admin.Family;
                        }
                    }
                #endregion

                #region Menu
                adminMenu:
                    Actions.DateTimeWithUserStatistic(adminName);
                    Console.Write("\n\t\t\t\t1.  View Employees List");
                    Console.WriteLine("\t\t\t2.   View Students List");
                    Console.Write("\n\t\t\t\t3.  View Masters List");
                    Console.WriteLine("\t\t\t4.   View Courses List\n");

                    Console.Write("\n\t\t\t\t5.  Add New Employee ");
                    Console.WriteLine("\t\t\t6.   Add New Student ");
                    Console.Write("\n\t\t\t\t7.  Add New Master ");
                    Console.WriteLine("\t\t\t8.   Add New Course\n ");

                    Console.Write("\n\t\t\t\t9.  Edit  Employee Information ");
                    Console.WriteLine("\t\t10.  Edit  Student Information");
                    Console.Write("\n\t\t\t\t11. Edit  Master Information");
                    Console.WriteLine("\t\t12.  Edit Course Information\n ");

                    Console.Write("\n\t\t\t\t13. Remove  Employee ");
                    Console.WriteLine("\t\t\t14.  Remove Student ");
                    Console.Write("\n\t\t\t\t15. Remove Master ");
                    Console.WriteLine("\t\t\t16.  Remove Course\n ");
                    Console.Write("\n\t\t\t\t17. Change Role Users ");
                    Console.WriteLine("\t\t\t18.  Exit The Dashboard ");
                    Console.Write("\n\n\t\t\t\t\t\t    Your request number : ");
                    int adminRequest;
                    while (!int.TryParse(Console.ReadLine(), out adminRequest))
                    {
                        goto adminMenu;
                    }
                    #endregion


                    switch (adminRequest)
                    {
                        #region View Employees List
                        case 1:
                            Actions.DateTimeWithUserStatistic(adminName);
                            Actions.PrintEmployees();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion

                        #region View Students List
                        case 2:
                            Actions.DateTimeWithUserStatistic(adminName);
                            Actions.PrintStudents();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion

                        #region View Masters List
                        case 3:
                            Actions.DateTimeWithUserStatistic(adminName);
                            Actions.PrintMasters();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion

                        #region View Courses List
                        case 4:
                            Actions.DateTimeWithUserStatistic(adminName);
                            Actions.PrintCourse();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion



                        #region Add New Employee
                        case 5:
                            Actions.AddEmployee();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion

                        #region Add New Student 
                        case 6:
                            Actions.AddStudent();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion

                        #region Add New Master
                        case 7:
                            Actions.AddMaster();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion

                        #region Add New Course
                        case 8:
                            Actions.AddCourse();
                            Actions.ReturnTheMenu();
                            goto adminMenu;
                        #endregion



                        #region Edit  Employee Information 
                        case 9:
                        editEmployee:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select employee id for edit\n");
                            Actions.PrintEmployees();
                            Console.Write("\n\t\t\t\t\t\tEmployee id is : ");
                            int answerEmployeeId;
                            while (!int.TryParse(Console.ReadLine(), out answerEmployeeId))
                            {
                                Actions.WarningJustNumber("Employee id");
                                goto editEmployee;
                            }
                            using (UnivercityContext dbEditEmployee = new UnivercityContext("UniDBConStr"))
                            {
                                var employeeForEdit = dbEditEmployee.Employees.Find(answerEmployeeId);
                                if (employeeForEdit == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Employee list", answerEmployeeId);
                                    goto editEmployee;
                                }
                            editEmployeeMenu:
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\tChosse property for edit employee : {employeeForEdit.Name} {employeeForEdit.Family}");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1. Department" +
                                                  "\n\t\t\t\t\t\t\t2. Salary" +
                                                  "\n\t\t\t\t\t\t\t3. Name" +
                                                  "\n\t\t\t\t\t\t\t4. Family" +
                                                  "\n\t\t\t\t\t\t\t5. Phonenumber" +
                                                  "\n\t\t\t\t\t\t\t6. Password" +
                                                  "\n\t\t\t\t\t\t\t7. Access" +
                                                  "\n\t\t\t\t\t\t\t8. Return the menu ");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditEmployee;
                                while (!int.TryParse(Console.ReadLine(), out answerEditEmployee))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editEmployeeMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                switch (answerEditEmployee)
                                {
                                    #region Department
                                    case 1:
                                    employeeDepartment:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for employee : {employeeForEdit.Name} {employeeForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Department     : {employeeForEdit.Department} ");
                                        Console.Write("\n\t\t\t\t\t\t     New demartment : ");
                                        string newDepartmentForEmployee = Console.ReadLine();
                                        if (newDepartmentForEmployee == employeeForEdit.Department)
                                        {
                                            Actions.DuplicateItem(newDepartmentForEmployee, "Department");
                                            goto employeeDepartment;
                                        }
                                        employeeForEdit.Department = newDepartmentForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The department was changed to {newDepartmentForEmployee} for {employeeForEdit.Name} {employeeForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Salary
                                    case 2:
                                    newSalaryForEmployee:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for employee : {employeeForEdit.Name} {employeeForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Salary     : {employeeForEdit.Salary} ");
                                        Console.Write("\n\t\t\t\t\t\t     New salary : ");
                                        float newSalaryForEmployee;
                                        while (!float.TryParse(Console.ReadLine(), out newSalaryForEmployee))
                                        {
                                            Actions.WarningJustNumber("Monthly salary");
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            goto newSalaryForEmployee;
                                        }
                                        if (newSalaryForEmployee == employeeForEdit.Salary)
                                        {
                                            Actions.DuplicateItem($"{newSalaryForEmployee}", "Salary");
                                            goto newSalaryForEmployee;
                                        }
                                        employeeForEdit.Salary = newSalaryForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The salary was changed to {newSalaryForEmployee} for {employeeForEdit.Name} {employeeForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Name
                                    case 3:
                                    employeeName:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for employee : {employeeForEdit.Name} {employeeForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {employeeForEdit.Name} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = employeeForEdit.Name;
                                        string newNameForEmployee = Console.ReadLine();
                                        if (newNameForEmployee == employeeForEdit.Name)
                                        {
                                            Actions.DuplicateItem(newNameForEmployee, "Name");
                                            goto employeeName;
                                        }
                                        employeeForEdit.Name = newNameForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForEmployee} for {name} {employeeForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Family
                                    case 4:
                                    employeeFamily:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for employee : {employeeForEdit.Name} {employeeForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     family     : {employeeForEdit.Family} ");
                                        string family = employeeForEdit.Family;
                                        Console.Write("\n\t\t\t\t\t\t     New family : ");
                                        string newFamilyForEmployee = Console.ReadLine();
                                        if (newFamilyForEmployee == employeeForEdit.Family)
                                        {
                                            Actions.DuplicateItem(newFamilyForEmployee, "Family");
                                            goto employeeFamily;
                                        }
                                        employeeForEdit.Family = newFamilyForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The family was changed to {newFamilyForEmployee} for {employeeForEdit.Name} {family} \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region PhoneNumber
                                    case 5:
                                    newPhoneNumberForEmployee:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for employee : {employeeForEdit.Name} {employeeForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     PhoneNumber     : {employeeForEdit.PhoneNumber} ");
                                        Console.Write("\n\t\t\t\t\t\t     New phonenumber : ");
                                        string newPhoneNumberForEmployee = Console.ReadLine();
                                        while (!Regex.IsMatch(newPhoneNumberForEmployee, patternPhoneNumber))
                                        {
                                            Actions.IncorrectPhonenumber();
                                            goto newPhoneNumberForEmployee;
                                        }
                                        if (newPhoneNumberForEmployee == employeeForEdit.PhoneNumber)
                                        {
                                            Actions.DuplicateItem(newPhoneNumberForEmployee, "PhoneNumber");
                                            goto newPhoneNumberForEmployee;
                                        }
                                        employeeForEdit.PhoneNumber = newPhoneNumberForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The phonenumber was changed to {newPhoneNumberForEmployee} for {employeeForEdit.Name} {employeeForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Password
                                    case 6:
                                    employeePassword:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for employee : {employeeForEdit.Name} {employeeForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Password     : {employeeForEdit.Password} ");
                                        Console.Write("\n\t\t\t\t\t\t     New password : ");
                                        string newPasswordForEmployee = Console.ReadLine();
                                        if (newPasswordForEmployee == employeeForEdit.Password)
                                        {
                                            Actions.DuplicateItem(newPasswordForEmployee, "Password");
                                            goto employeePassword;
                                        }
                                        employeeForEdit.Password = newPasswordForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The password was changed to {newPasswordForEmployee} for {employeeForEdit.Name} {employeeForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Access
                                    case 7:
                                        if (employeeForEdit.IsActive == true)
                                        {
                                            employeeForEdit.IsActive = false;
                                        }
                                        else
                                        {
                                            employeeForEdit.IsActive = true;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine($"\n\t\t\t\t\tThe Access was changed to {employeeForEdit.IsActive} for {employeeForEdit.Name} {employeeForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Return the menu
                                    case 8: goto adminMenu;
                                    #endregion

                                    default: goto editEmployeeMenu;
                                }
                            }
                        #endregion



                        //bug remove course
                        #region Edit  Student Information
                        case 10:
                        editStudent:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select student id for edit\n");
                            Actions.PrintStudents();
                            Console.Write("\n\t\t\t\t\t\tStudent id is : ");
                            int answerStudentId;
                            while (!int.TryParse(Console.ReadLine(), out answerStudentId))
                            {
                                Actions.WarningJustNumber("Student id");
                                goto editStudent;
                            }
                            UnivercityContext dbEditStudent = new UnivercityContext("UniDBConStr");
                            using (dbEditStudent)
                            {
                                var studentForEdit = dbEditStudent.Students.Find(answerStudentId);
                                if (studentForEdit == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Student list", answerStudentId);
                                    goto editStudent;
                                }
                            editStudentMenu:
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\tChsse property for edit student : {studentForEdit.Name} {studentForEdit.Family}");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1.  Degree" +
                                                  "\n\t\t\t\t\t\t\t2.  Code" +
                                                  "\n\t\t\t\t\t\t\t3.  Name" +
                                                  "\n\t\t\t\t\t\t\t4.  Family" +
                                                  "\n\t\t\t\t\t\t\t5.  Phonenumber" +
                                                  "\n\t\t\t\t\t\t\t6.  Password" +
                                                  "\n\t\t\t\t\t\t\t7.  Access" +
                                                  "\n\t\t\t\t\t\t\t8.  Add course" +
                                                  "\n\t\t\t\t\t\t\t9.  Remove course" +
                                                  "\n\t\t\t\t\t\t\t10. Return the menu");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditStudent;
                                while (!int.TryParse(Console.ReadLine(), out answerEditStudent))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editStudentMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                switch (answerEditStudent)
                                {
                                    #region Degree
                                    case 1:
                                    studentDegree:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Degree     : {studentForEdit.Degree} ");
                                        Console.Write("\n\t\t\t\t\t\t     New degree : ");
                                        string newDegreeForStudent = Console.ReadLine();
                                        if (newDegreeForStudent == studentForEdit.Degree)
                                        {
                                            Actions.DuplicateItem(newDegreeForStudent, "Degree");
                                            goto studentDegree;
                                        }
                                        studentForEdit.Degree = newDegreeForStudent;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The degree was changed to {newDegreeForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Code
                                    case 2:
                                    newCodeForStudent:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Code     : {studentForEdit.StudentCode} ");
                                        Console.Write("\n\t\t\t\t\t\t     New code : ");
                                        int newCodeForStudent;
                                        while (!int.TryParse(Console.ReadLine(), out newCodeForStudent))
                                        {
                                            Actions.WarningJustNumber("Student code");
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            goto newCodeForStudent;
                                        }
                                        if (newCodeForStudent == studentForEdit.StudentCode)
                                        {
                                            Actions.DuplicateItem($"{newCodeForStudent}", "StudentCode");
                                            goto newCodeForStudent;
                                        }
                                        studentForEdit.StudentCode = newCodeForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The code was changed to {newCodeForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Name
                                    case 3:
                                    studentName:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {studentForEdit.Name} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = studentForEdit.Name;
                                        string newNameForEmployee = Console.ReadLine();
                                        if (newNameForEmployee == studentForEdit.Name)
                                        {
                                            Actions.DuplicateItem(newNameForEmployee, "Name");
                                            goto studentName;
                                        }
                                        studentForEdit.Name = newNameForEmployee;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForEmployee} for {name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Family
                                    case 4:
                                    studentFamily:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Family     : {studentForEdit.Family} ");
                                        string family = studentForEdit.Family;
                                        Console.Write("\n\t\t\t\t\t\t     New family : ");
                                        string newFamilyForStudent = Console.ReadLine();
                                        if (newFamilyForStudent == studentForEdit.Family)
                                        {
                                            Actions.DuplicateItem(newFamilyForStudent, "Family");
                                            goto studentFamily;
                                        }
                                        studentForEdit.Family = newFamilyForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The family was changed to {newFamilyForStudent} for {studentForEdit.Name} {family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region PhoneNumber
                                    case 5:
                                    newPhoneNumberForStudent:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     PhoneNumber     : {studentForEdit.PhoneNumber} ");
                                        Console.Write("\n\t\t\t\t\t\t     New phonenumber : ");
                                        string newPhoneNumberForStudent = Console.ReadLine();
                                        while (!Regex.IsMatch(newPhoneNumberForStudent, patternPhoneNumber))
                                        {
                                            Actions.IncorrectPhonenumber();
                                            goto newPhoneNumberForStudent;
                                        }
                                        if (newPhoneNumberForStudent == studentForEdit.PhoneNumber)
                                        {
                                            Actions.DuplicateItem(newPhoneNumberForStudent, "PhoneNumber");
                                            goto newPhoneNumberForStudent;
                                        }
                                        studentForEdit.PhoneNumber = newPhoneNumberForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The phonenumber was changed to {newPhoneNumberForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Password
                                    case 6:
                                    studentPassword:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Password     : {studentForEdit.Password} ");
                                        Console.Write("\n\t\t\t\t\t\t     New password : ");
                                        string newPasswordForStudent = Console.ReadLine();
                                        if (newPasswordForStudent == studentForEdit.Password)
                                        {
                                            Actions.DuplicateItem(newPasswordForStudent, "Password");
                                            goto studentPassword;
                                        }
                                        studentForEdit.Password = newPasswordForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The password was changed to {newPasswordForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Access
                                    case 7:
                                        if (studentForEdit.IsActive == true)
                                        {
                                            studentForEdit.IsActive = false;
                                        }
                                        else
                                        {
                                            studentForEdit.IsActive = true;
                                        }

                                        Console.WriteLine($"\n\t\t\t\t\t\t\tThe Access was changed to {studentForEdit.IsActive} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Add course
                                    case 8:
                                    answerCourseId:
                                        if (studentForEdit.Courses.Count == dbEditStudent.Courses.Count())
                                        {
                                            Actions.AlreadySelected();
                                            goto editStudentMenu;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        List<Course> coursesList = studentForEdit.Courses.ToList();
                                        Console.WriteLine($"\n\t\t\t\tPlease select a course for add to student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Actions.PrintCourse();
                                        Console.Write("\n\t\t\t\t\t\t\tCourse id is : ");
                                        int courseIdForStudent;
                                        while (!int.TryParse(Console.ReadLine(), out courseIdForStudent))
                                        {
                                            Actions.WarningJustNumber("Course id");
                                            goto answerCourseId;
                                        }
                                        var courseForStudent = dbEditStudent.Courses.Find(courseIdForStudent);
                                        if (courseForStudent == null)
                                        {
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Actions.WarningNotFoundId("Course list", courseIdForStudent);
                                            goto answerCourseId;
                                        }
                                        if (coursesList.Any(t => t.CourseId == courseForStudent.CourseId))
                                        {
                                            Actions.DuplicateItem(courseForStudent.CourseName, "Course");
                                            goto answerCourseId;
                                        }
                                        else
                                        {
                                            studentForEdit.Courses.Add(courseForStudent);

                                        }
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine($"\n\t\t\t\t\t{courseForStudent.CourseName} course added to student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    //bug
                                    #region Remove course
                                    case 9:
                                    RemoveCourse:
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine("\n\t\t\t\t\tPlease select course id for remove\n");
                                        List<Course> courseList = studentForEdit.Courses.ToList();
                                        foreach (var course in courseList)
                                        {
                                            Console.WriteLine($"\tId :{course.CourseId}    Name :{course.CourseName}    Unit :{course.CourseUnit}    Is active:{course.IsActive}");
                                            Console.WriteLine("\n____________________________________________________________________________________________________________________________\n");
                                        }
                                        Console.Write("\n\t\t\t\t\t\t\tCourse id is : ");
                                        int corseIdForRemoveForStudent;
                                        while (!int.TryParse(Console.ReadLine(), out corseIdForRemoveForStudent))
                                        {
                                            Actions.WarningJustNumber("Course id");
                                            goto RemoveCourse;
                                        }
                                        UnivercityContext dbRemoveCourse = new UnivercityContext("UniDBConStr");
                                        using (dbRemoveCourse)
                                        {
                                            Course course = courseList.Find(t => t.CourseId == corseIdForRemoveForStudent);

                                            if (studentForEdit.Courses.Any())
                                            {
                                                studentForEdit.Courses.Remove(course);
                                            }
                                            else
                                            {
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Actions.WarningNotFoundId("Course list", corseIdForRemoveForStudent);
                                                goto RemoveCourse;
                                            }
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Console.WriteLine($"\n\t{course.CourseName} was removed\n");
                                            dbRemoveCourse.SaveChanges();
                                            Thread.Sleep(1000);
                                        }

                                        goto editStudentMenu;
                                    #endregion

                                    #region Return the menu
                                    case 10:
                                        goto adminMenu;
                                    #endregion

                                    default:
                                        goto editStudentMenu;
                                }
                            }
                        #endregion      

                        #region Edit  Master Information
                        case 11:
                        editMaster:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select master id for edit\n");
                            Actions.PrintMasters();
                            Console.Write("\n\t\t\t\t\t\tMaster id is : ");
                            int answerMasterId;
                            while (!int.TryParse(Console.ReadLine(), out answerMasterId))
                            {
                                Actions.WarningJustNumber("Master id");
                                goto editMaster;
                            }
                            UnivercityContext dbEditMaster = new UnivercityContext("UniDBConStr");
                            using (dbEditMaster)
                            {
                                var masterForEdit = dbEditMaster.Masters.Find(answerMasterId);
                                if (masterForEdit == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Master list", answerMasterId);
                                    goto editMaster;
                                }
                            editMasterMenu:
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\tChsse property for edit master : {masterForEdit.Name} {masterForEdit.Family}");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1. Degree" +
                                                  "\n\t\t\t\t\t\t\t2. Salary" +
                                                  "\n\t\t\t\t\t\t\t3. Name" +
                                                  "\n\t\t\t\t\t\t\t4. Family" +
                                                  "\n\t\t\t\t\t\t\t5. Phonenumber" +
                                                  "\n\t\t\t\t\t\t\t6. Password" +
                                                  "\n\t\t\t\t\t\t\t7. Access" +
                                                  "\n\t\t\t\t\t\t\t8. Return the menu");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditMaster;
                                while (!int.TryParse(Console.ReadLine(), out answerEditMaster))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editMasterMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                switch (answerEditMaster)
                                {
                                    #region Degree
                                    case 1:
                                    masterDegree:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Degree     : {masterForEdit.Degree} ");
                                        Console.Write("\n\t\t\t\t\t\t     New degree : ");
                                        string newDegreeForMaster = Console.ReadLine();
                                        if (newDegreeForMaster == masterForEdit.Degree)
                                        {
                                            Actions.DuplicateItem(newDegreeForMaster, "Degree");
                                            goto masterDegree;
                                        }
                                        masterForEdit.Degree = newDegreeForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The degree was changed to {newDegreeForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Salary
                                    case 2:
                                    newSalaryForEmployee:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Salary     : {masterForEdit.Salary} ");
                                        Console.Write("\n\t\t\t\t\t\t     New salary : ");
                                        float newSalaryForMaster;
                                        while (!float.TryParse(Console.ReadLine(), out newSalaryForMaster))
                                        {
                                            Actions.WarningJustNumber("Monthly salary");
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            goto newSalaryForEmployee;
                                        }
                                        if (newSalaryForMaster == masterForEdit.Salary)
                                        {
                                            Actions.DuplicateItem($"{newSalaryForMaster}", "Salary");
                                            goto newSalaryForEmployee;
                                        }
                                        masterForEdit.Salary = newSalaryForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The salary was changed to {newSalaryForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Name
                                    case 3:
                                    masterName:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {masterForEdit.Name} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = masterForEdit.Name;
                                        string newNameForMaster = Console.ReadLine();
                                        if (newNameForMaster == masterForEdit.Name)
                                        {
                                            Actions.DuplicateItem(newNameForMaster, "Name");
                                            goto masterName;
                                        }
                                        masterForEdit.Name = newNameForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForMaster} for {name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Family
                                    case 4:
                                    masterFamily:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     family     : {masterForEdit.Family} ");
                                        string family = masterForEdit.Family;
                                        Console.Write("\n\t\t\t\t\t\t     New family : ");
                                        string newFamilyForMaster = Console.ReadLine();
                                        if (newFamilyForMaster == masterForEdit.Family)
                                        {
                                            Actions.DuplicateItem(newFamilyForMaster, "Family");
                                            goto masterFamily;
                                        }
                                        masterForEdit.Family = newFamilyForMaster;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The family was changed to {newFamilyForMaster} for {masterForEdit.Name} {family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region PhoneNumber
                                    case 5:
                                    newPhoneNumberForMaster:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     PhoneNumber     : {masterForEdit.PhoneNumber} ");
                                        Console.Write("\n\t\t\t\t\t\t     New phonenumber : ");
                                        string newPhoneNumberForMaster = Console.ReadLine();
                                        while (!Regex.IsMatch(newPhoneNumberForMaster, patternPhoneNumber))
                                        {
                                            Actions.IncorrectPhonenumber();
                                            goto newPhoneNumberForMaster;
                                        }
                                        if (newPhoneNumberForMaster == masterForEdit.PhoneNumber)
                                        {
                                            Actions.DuplicateItem(newPhoneNumberForMaster, "PhoneNumber");
                                            goto newPhoneNumberForMaster;
                                        }
                                        masterForEdit.PhoneNumber = newPhoneNumberForMaster;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The phonenumber was changed to {newPhoneNumberForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Password
                                    case 6:
                                    masterPassword:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Password     : {masterForEdit.Password} ");
                                        Console.Write("\n\t\t\t\t\t\t     New password : ");
                                        string newPasswordForMaster = Console.ReadLine();
                                        if (newPasswordForMaster == masterForEdit.Password)
                                        {
                                            Actions.DuplicateItem(newPasswordForMaster, "Password");
                                            goto masterPassword;
                                        }
                                        masterForEdit.Password = newPasswordForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The password was changed to {newPasswordForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Access
                                    case 7:
                                        if (masterForEdit.IsActive == true)
                                        {
                                            masterForEdit.IsActive = false;
                                        }
                                        else
                                        {
                                            masterForEdit.IsActive = true;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine($"\n\tThe access was changed to {masterForEdit.IsActive} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Return the menu
                                    case 8:
                                        goto adminMenu;
                                    #endregion

                                    default:
                                        goto editMasterMenu;
                                }
                            }
                        #endregion



                        //bug change master and remove master
                        #region Edit Course Information 
                        case 12:
                        editCourse:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select course id for edit\n");
                            Actions.PrintCourse();
                            Console.Write("\n\t\t\t\t\t\tCourse id is : ");
                            int answerCourseId;
                            while (!int.TryParse(Console.ReadLine(), out answerCourseId))
                            {
                                Actions.WarningJustNumber("Course id");
                                goto editCourse;
                            }
                            UnivercityContext dbEditCourse = new UnivercityContext("UniDBConStr");
                            using (dbEditCourse)
                            {
                                var courseForEdit = dbEditCourse.Courses.Find(answerCourseId);
                                if (courseForEdit == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Course list", answerCourseId);
                                    goto editCourse;
                                }
                            editCourseMenu:
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\tChsse property for edit course : {courseForEdit.CourseName}");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1. Name" +
                                                  "\n\t\t\t\t\t\t\t2. Unit" +
                                                  "\n\t\t\t\t\t\t\t3. Add Master" +
                                                  "\n\t\t\t\t\t\t\t4. Change Master" +
                                                  "\n\t\t\t\t\t\t\t5. Remove Master" +
                                                  "\n\t\t\t\t\t\t\t6. Return the menu");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditCourse;
                                while (!int.TryParse(Console.ReadLine(), out answerEditCourse))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editCourseMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                switch (answerEditCourse)
                                {
                                    #region Name
                                    case 1:
                                    courseName:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for course : {courseForEdit.CourseName}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {courseForEdit.CourseName} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = courseForEdit.CourseName;
                                        string newNameForCourse = Console.ReadLine();
                                        if (newNameForCourse == courseForEdit.CourseName)
                                        {
                                            Actions.DuplicateItem(newNameForCourse, "Name");
                                            goto courseName;
                                        }
                                        courseForEdit.CourseName = newNameForCourse;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForCourse} for {name} \n");
                                        Thread.Sleep(3000);
                                        dbEditCourse.SaveChanges();
                                        goto editCourseMenu;
                                    #endregion

                                    #region Unit
                                    case 2:
                                    newSalaryForCourse:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for course : {courseForEdit.CourseName}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Unit     : {courseForEdit.CourseUnit} ");
                                        Console.Write("\n\t\t\t\t\t\t     New unit : ");
                                        int newUnitForCourse;
                                        while (!int.TryParse(Console.ReadLine(), out newUnitForCourse))
                                        {
                                            Actions.WarningJustNumber("Course name");
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            goto newSalaryForCourse;
                                        }
                                        if (newUnitForCourse == courseForEdit.CourseUnit)
                                        {
                                            Actions.DuplicateItem($"{newUnitForCourse}", "Unit");
                                            goto newSalaryForCourse;
                                        }
                                        courseForEdit.CourseUnit = newUnitForCourse;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The unit was changed to {newUnitForCourse} for {courseForEdit.CourseName}\n");
                                        Thread.Sleep(3000);
                                        dbEditCourse.SaveChanges();
                                        goto editCourseMenu;
                                    #endregion

                                    #region Add Master
                                    case 3:
                                    addMasterForCourse:
                                        if (courseForEdit.Masters.Count == dbEditCourse.Masters.Count())
                                        {
                                            Actions.AlreadySelected();
                                            goto editCourseMenu;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        List<Master> mastersList = courseForEdit.Masters.ToList();
                                        if (mastersList.Any())
                                        {
                                            Console.WriteLine($"\n\t\t\t\t\t\t\tMaster: {courseForEdit.CourseName}");
                                            foreach (var master in mastersList)
                                            {
                                                Console.WriteLine($"\n\t\t\t\t\tId : {master.UserId}     Name : {master.Name}     Family : {master.Family}");
                                            }
                                            Console.WriteLine("\n\t    ______________________________________________________________________________________________________");

                                        }
                                        Console.WriteLine($"\n\t\t\t\t\t\tPlease select master id for add to {courseForEdit.CourseName}\n");
                                        Actions.PrintMasters();
                                        Console.Write("\n\t\t\t\t\t\t\tMaster id is : ");
                                        int masterIdForAddCourse;
                                        while (!int.TryParse(Console.ReadLine(), out masterIdForAddCourse))
                                        {
                                            Actions.WarningJustNumber("Master id");
                                            goto addMasterForCourse;
                                        }
                                        var newMasterForCourse = dbEditCourse.Masters.Find(masterIdForAddCourse);
                                        if (newMasterForCourse == null)
                                        {
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Actions.WarningNotFoundId("Course list", masterIdForAddCourse);
                                            goto addMasterForCourse;
                                        }
                                        if (mastersList.Any(t => t.UserId == newMasterForCourse.UserId))
                                        {
                                            Actions.DuplicateItem($"{newMasterForCourse.Name}{newMasterForCourse.Family}", "Master");
                                            goto addMasterForCourse;
                                        }
                                        else
                                        {
                                            courseForEdit.Masters.Add(newMasterForCourse);
                                        }
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine($"\n\n\t\t\t\t\t\tMaster {newMasterForCourse.Name} {newMasterForCourse.Family} added to course : {courseForEdit.CourseName}");
                                        Thread.Sleep(3000);
                                        dbEditCourse.SaveChanges();
                                        goto editCourseMenu;

                                    #endregion

                                    //bug
                                    //هنگام وارد کردن مستر ایدی برای تغییر اگر ایدی ای وارد شه  که توی لیست استاد های این درس نیست هیچ اروری نمیده و میاد درس جایگزین رو اضافه میکنه به لیست استاد های این درس
                                    #region Change Master
                                    case 4:
                                    changeMasterForCourse1:

                                        if (courseForEdit.Masters.Count == dbEditCourse.Masters.Count())
                                        {
                                            Actions.AlreadySelected();
                                            goto editCourseMenu;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        List<Master> mastersList1 = courseForEdit.Masters.ToList();
                                        if (!mastersList1.Any())
                                        {
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Console.WriteLine("\n\t\t\t\t\t\t\tthis course doesnt have any master ...");
                                            Thread.Sleep(3000);
                                            goto editCourseMenu;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\n\t\t\t\t\t\t\tMaster: {courseForEdit.CourseName}");
                                            foreach (var master in mastersList1)
                                            {
                                                Console.WriteLine($"\n\t\t\t\t\tId : {master.UserId}     Name : {master.Name}     Family : {master.Family}");
                                            }
                                            Console.WriteLine("\n\t    ______________________________________________________________________________________________________");

                                            Console.WriteLine("\n\t\t\t\t\t\tWhich teacher do you want to change?");
                                            Console.Write("\n\t\t\t\t\t\t\tMaster id is : ");
                                            int masterIdForChangeCourse1;
                                            while (!int.TryParse(Console.ReadLine(), out masterIdForChangeCourse1))
                                            {
                                                Actions.WarningJustNumber("Master id");
                                                goto changeMasterForCourse1;
                                            }
                                            var changeMasterForCourse = dbEditCourse.Masters.Find(masterIdForChangeCourse1);
                                            if (changeMasterForCourse == null)
                                            {
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Actions.WarningNotFoundId("Master list", masterIdForChangeCourse1);
                                                goto changeMasterForCourse1;
                                            }
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                        changeMasterForCourse2:
                                            Console.WriteLine($"\n\t\t\t\tPlease enter the ID of the master you want to replace with {changeMasterForCourse.Name} {changeMasterForCourse.Family}\n");
                                            Actions.PrintMasters();
                                            Console.Write("\n\t\t\t\t\t\t\tMaster id is : ");
                                            int masterIdForChangeCourse2;
                                            while (!int.TryParse(Console.ReadLine(), out masterIdForChangeCourse2))
                                            {
                                                Actions.WarningJustNumber("Master id");
                                                goto changeMasterForCourse2;
                                            }
                                            var replaceMasterForCourse = dbEditCourse.Masters.Find(masterIdForChangeCourse2);
                                            if (replaceMasterForCourse == null)
                                            {
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Actions.WarningNotFoundId("Master list", masterIdForChangeCourse2);
                                                goto changeMasterForCourse2;
                                            }
                                            if (mastersList1.Any(t => t.UserId == replaceMasterForCourse.UserId))
                                            {
                                                Actions.DuplicateItem($"{replaceMasterForCourse.Name}", "Master");
                                                goto changeMasterForCourse2;
                                            }
                                            else
                                            {
                                                courseForEdit.Masters.Remove(changeMasterForCourse);
                                                courseForEdit.Masters.Add(replaceMasterForCourse);
                                            }

                                            //if (courseForEdit.Masters.Any)
                                            //{

                                            //}


                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Console.WriteLine($"\n\t\t\t\t\tMaster {changeMasterForCourse.Name} {changeMasterForCourse.Family} replace with  master {replaceMasterForCourse.Name} {replaceMasterForCourse.Family} for course : {courseForEdit.CourseName}");
                                            Thread.Sleep(3000);
                                            dbEditCourse.SaveChanges();
                                        }
                                        goto editCourseMenu;
                                    #endregion

                                    #region Remove Master
                                    case 5:



                                        goto editCourseMenu;
                                    #endregion


                                    #region Return the menu
                                    case 6: goto adminMenu;
                                    #endregion

                                    default: goto editCourseMenu;

                                }
                            }
                        //break;
                        #endregion


                        #region Remove  Employee 
                        case 13:
                        employeeListForRemove:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t  Please select employee id for remove\n");
                            Actions.PrintEmployees();
                            Console.Write("\n\t\t\t\t\t\tEmployee id is : ");
                            int employeeIdForRemove;
                            while (!int.TryParse(Console.ReadLine(), out employeeIdForRemove))
                            {
                                Actions.WarningJustNumber("Employee id");
                                goto employeeListForRemove;
                            }
                            UnivercityContext dbRemoveEmployee = new UnivercityContext("UniDBConStr");
                            using (dbRemoveEmployee)
                            {
                                var employeeForRemove = dbRemoveEmployee.Employees.Find(employeeIdForRemove);
                                if (employeeForRemove == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Employee list", employeeIdForRemove);
                                    goto employeeListForRemove;
                                }
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\t\t    {employeeForRemove.Name} {employeeForRemove.Family} was removed\n");
                                dbRemoveEmployee.Employees.Remove(employeeForRemove);
                                dbRemoveEmployee.SaveChanges();
                                Actions.ReturnTheMenu();
                            }
                            goto adminMenu;
                        #endregion

                        #region Remove Student
                        case 14:
                        studentForRemove:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t  Please select student id for remove\n");
                            Actions.PrintStudents();
                            Console.Write("\n\t\t\t\t\t\tStudent id is : ");
                            int studentIdForRemove;
                            while (!int.TryParse(Console.ReadLine(), out studentIdForRemove))
                            {
                                Actions.WarningJustNumber("Student id");
                                goto studentForRemove;
                            }
                            UnivercityContext dbRemoveStudent = new UnivercityContext("UniDBConStr");
                            using (dbRemoveStudent)
                            {
                                var studentForRemove = dbRemoveStudent.Students.Find(studentIdForRemove);
                                if (studentForRemove == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Student list", studentIdForRemove);
                                    goto studentForRemove;
                                }
                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\t\t   {studentForRemove.Name} {studentForRemove.Family} was removed\n");
                                dbRemoveStudent.Students.Remove(studentForRemove);
                                dbRemoveStudent.SaveChanges();
                                Actions.ReturnTheMenu();
                            }
                            goto adminMenu;
                        #endregion

                        #region  Remove Master
                        case 15:
                        masterForRemove:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t  Please select master id for remove\n");
                            Actions.PrintMasters();
                            Console.Write("\n\t\t\t\t\t\t  Master id is : ");
                            int masterIdForRemove;
                            while (!int.TryParse(Console.ReadLine(), out masterIdForRemove))
                            {
                                Actions.WarningJustNumber("Master id");
                                goto masterForRemove;
                            }
                            UnivercityContext dbRemoveMaster = new UnivercityContext("UniDBConStr");
                            using (dbRemoveMaster)
                            {
                                var masterForRemove = dbRemoveMaster.Masters.Find(masterIdForRemove);
                                if (masterForRemove == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Master list", masterIdForRemove);
                                    goto masterForRemove;
                                }
                                //نفهمیدم
                                //var removeCourseMaster = dbRemoveMaster.Courses.Where(t => t.Master.UserId == masterForRemove.UserId);

                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\t\t    {masterForRemove.Name} {masterForRemove.Family} was removed\n");
                                dbRemoveMaster.Masters.Remove(masterForRemove);
                                dbRemoveMaster.SaveChanges();
                                Actions.ReturnTheMenu();
                            }
                            goto adminMenu;

                        #endregion


                        #region Remove Course
                        case 16:
                        courseForRemove:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select course id for remove\n");
                            Actions.PrintCourse();
                            Console.Write("\n\t\t\t\t\t\tCourse id is : ");
                            int corseIdForRemove;
                            while (!int.TryParse(Console.ReadLine(), out corseIdForRemove))
                            {
                                Actions.WarningJustNumber("Course id");
                                goto courseForRemove;
                            }
                            UnivercityContext dbRemoveCorse = new UnivercityContext("UniDBConStr");
                            using (dbRemoveCorse)
                            {
                                var courseForRemove = dbRemoveCorse.Courses.Find(corseIdForRemove);
                                if (courseForRemove == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(adminName);
                                    Actions.WarningNotFoundId("Course list", corseIdForRemove);
                                    goto courseForRemove;
                                }
                                dbRemoveCorse.Courses.Remove(courseForRemove);                        

                                Actions.DateTimeWithoutUserStatistic(adminName);
                                Console.WriteLine($"\n\t\t\t\t\t\t{courseForRemove.CourseName} was removed\n");
                                dbRemoveCorse.Courses.Remove(courseForRemove);
                                dbRemoveCorse.SaveChanges();
                                Actions.ReturnTheMenu();
                            }
                            goto adminMenu;



                        #endregion

                        //bug
                        #region  Change Role Users
                        case 17:
                        changeRole:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine("\n\t\t\t\t\t     Pleace select object for change role ");
                            Console.WriteLine("\n\t\t\t\t\t\t\t1. Employee" +
                                              "\n\t\t\t\t\t\t\t2. Master" +
                                              "\n\t\t\t\t\t\t\t3. Student");
                            Console.Write("\n\t\t\t\t\t\t   Your answer is : ");
                            int answerChangeRole;
                            while (!int.TryParse(Console.ReadLine(), out answerChangeRole))
                            {
                                Actions.WarningJustNumber("Select item of menu");
                                goto changeRole;
                            }
                            UnivercityContext dbChangeRole = new UnivercityContext("UniDBConStr");
                            using (dbChangeRole)
                            {
                                switch (answerChangeRole)
                                {
                                    #region فرم تغییر نقش کارمندان
                                    case 1:
                                    changeEmployeeRole:
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine("\n\t\t\t\t\t\tPleace select employee for change role \n");
                                        Actions.PrintEmployees();
                                        Console.Write("\n\t\t\t\t\t\t\tEmployee id is : ");
                                        int employeeIdChangeRole;
                                        while (!int.TryParse(Console.ReadLine(), out employeeIdChangeRole))
                                        {
                                            Actions.WarningJustNumber("Employee id");
                                            goto changeEmployeeRole;
                                        }
                                        var employeeForChangeRole = dbChangeRole.Employees.Find(employeeIdChangeRole);
                                        if (employeeForChangeRole == null)
                                        {
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Actions.WarningNotFoundId("Employee list", employeeIdChangeRole);
                                            goto employeeListForRemove;
                                        }

                                    changeEmployeeRoleMenu:
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine($"\n\t\t\t\t\tPleace select object for employee : {employeeForChangeRole.Name} {employeeForChangeRole.Family} ");
                                        Console.WriteLine("\n\t\t\t\t\t\t\t1. Master" +
                                                         "\n\t\t\t\t\t\t\t2. Student");
                                        Console.Write("\n\t\t\t\t\t\t    Your answer is : ");
                                        int answerChangeEmployeeRole;
                                        while (!int.TryParse(Console.ReadLine(), out answerChangeEmployeeRole))
                                        {
                                            Actions.WarningJustNumber("Select item of menu");
                                            goto changeEmployeeRoleMenu;
                                        }
                                        switch (answerChangeEmployeeRole)
                                        {

                                            case 1:
                                                dbChangeRole.Masters.Add(employeeForChangeRole);
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Console.WriteLine($"\n\t\t\t\t\tRole of {employeeForChangeRole.Name} {employeeForChangeRole.Family} was change to master");
                                                dbChangeRole.Employees.Remove(employeeForChangeRole);
                                                break;

                                            case 2:
                                                dbChangeRole.Students.Add(employeeForChangeRole);
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                dbChangeRole.Employees.Remove(employeeForChangeRole);
                                                Console.WriteLine($"\n\t\t\t\t\tRole of {employeeForChangeRole.Name} {employeeForChangeRole.Family} was change to student");
                                                break;

                                            default:
                                                goto changeEmployeeRoleMenu;
                                        }

                                        break;
                                    #endregion

                                    #region فرم تغییر نقش اساتید 
                                    case 2:
                                    changeMasterRole:
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine("\n\t\t\t\t\t\tPleace select Master for change role \n");
                                        Actions.PrintMasters();
                                        Console.Write("\n\t\t\t\t\t\t\tMaster id is : ");
                                        int masterIdChangeRole;
                                        while (!int.TryParse(Console.ReadLine(), out masterIdChangeRole))
                                        {
                                            Actions.WarningJustNumber("Master id");
                                            goto changeMasterRole;
                                        }
                                        var masterForChangeRole = dbChangeRole.Masters.Find(masterIdChangeRole);
                                        if (masterForChangeRole == null)
                                        {
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Actions.WarningNotFoundId("Student list", masterIdChangeRole);
                                            goto changeMasterRole;
                                        }

                                    changeMasterRoleMenu:
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine($"\n\t\t\t\t\tPleace select object for Master : {masterForChangeRole.Name} {masterForChangeRole.Family} \n");
                                        Console.WriteLine("\n\t\t\t\t\t\t\t1. Student" +
                                                         "\n\t\t\t\t\t\t\t2. Employee");
                                        Console.Write("\n\t\t\t\t\t\t   Your answer is : ");
                                        int answerChangeMasterRole;
                                        while (!int.TryParse(Console.ReadLine(), out answerChangeMasterRole))
                                        {
                                            Actions.WarningJustNumber("Select item of menu");
                                            goto changeMasterRoleMenu;
                                        }
                                        switch (answerChangeMasterRole)
                                        {
                                            case 1:
                                                dbChangeRole.Students.Add((Student)masterForChangeRole);
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Console.WriteLine($"\n\t\t\t\t\tRole of {masterForChangeRole.Name} {masterForChangeRole.Family} was change to student");
                                                dbChangeRole.Masters.Remove(masterForChangeRole);
                                                break;

                                            case 2:
                                                dbChangeRole.Employees.Add(masterForChangeRole);
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Console.WriteLine($"\n\t\t\t\t\tRole of {masterForChangeRole.Name} {masterForChangeRole.Family} was change to employee");
                                                dbChangeRole.Masters.Remove(masterForChangeRole);
                                                break;

                                            default:
                                                goto changeMasterRole;
                                        }

                                        break;

                                    #endregion

                                    #region فرم تغییر نقش دانشجویان
                                    case 3:
                                    changeStudentRole:
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine("\n\t\t\t\t\t\tPleace select Student for change role \n");
                                        Actions.PrintStudents();
                                        Console.Write("\n\t\t\t\t\t\t\tStudent id is : ");
                                        int studentIdChangeRole;
                                        while (!int.TryParse(Console.ReadLine(), out studentIdChangeRole))
                                        {
                                            Actions.WarningJustNumber("Student id");
                                            goto changeStudentRole;
                                        }
                                        var studentForChangeRole = dbChangeRole.Students.Find(studentIdChangeRole);
                                        if (studentForChangeRole == null)
                                        {
                                            Actions.DateTimeWithoutUserStatistic(adminName);
                                            Actions.WarningNotFoundId("Student list", studentIdChangeRole);
                                            goto changeStudentRole;
                                        }
                                    changeStudentRoleMenu:
                                        Actions.DateTimeWithoutUserStatistic(adminName);
                                        Console.WriteLine($"\n\t\t\t\t\tPleace select object for student : {studentForChangeRole.Name} {studentForChangeRole.Family} \n");
                                        Console.WriteLine("\n\t\t\t\t\t\t\t1. Master" +
                                                         "\n\t\t\t\t\t\t\t2. Employee");
                                        Console.Write("\n\t\t\t\t\t\t   Your answer is : ");
                                        int answerChangeStudentRole;
                                        while (!int.TryParse(Console.ReadLine(), out answerChangeStudentRole))
                                        {
                                            Actions.WarningJustNumber("Select item of menu");
                                            goto changeStudentRoleMenu;
                                        }
                                        switch (answerChangeStudentRole)
                                        {
                                            case 1:
                                                dbChangeRole.Masters.Add(studentForChangeRole);
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Console.WriteLine($"\n\t\t\t\t\tRole of {studentForChangeRole.Name} {studentForChangeRole.Family} was change to master");
                                                dbChangeRole.Students.Remove(studentForChangeRole);
                                                break;

                                            case 2:
                                                dbChangeRole.Employees.Add(studentForChangeRole);
                                                Actions.DateTimeWithoutUserStatistic(adminName);
                                                Console.WriteLine($"\n\t\t\t\t\tRole of {studentForChangeRole.Name} {studentForChangeRole.Family} was change to employee");
                                                dbChangeRole.Students.Remove(studentForChangeRole);
                                                break;


                                            default:
                                                goto changeStudentRole;
                                        }
                                        break;
                                    #endregion

                                    default:
                                        goto changeRole;
                                }
                                dbChangeRole.SaveChanges();
                                Actions.ReturnTheMenu();
                            }
                            goto adminMenu;
                        #endregion

                        #region Exit The Dashboard
                        case 18:
                            Actions.DateTimeWithoutUserStatistic(adminName);
                            Console.WriteLine($"\n\n\n\n\t\t\t\t\t\tHave a good time {adminName}");
                            Thread.Sleep(3000);
                            Console.Clear();
                            goto loginMenu;
                        #endregion

                        default:
                            goto adminMenu;

                    }
                #endregion

                #region Employee 
                case 2:

                #region  Logged Employee
                EmployeeLogin:
                    Actions.JustDateTime();
                    Console.Write("\n\n\n\t\t\t\t\t\tPhonenumber : ");
                    string employeeUsername = Console.ReadLine();
                    Console.Write("\n\t\t\t\t\t\tPassword    : ");
                    string employeePassword = Console.ReadLine();

                    string employeeName = "";
                    using (UnivercityContext dbEmployeeLogin = new UnivercityContext("UniDBConStr"))
                    {
                        var employee = dbEmployeeLogin.Employees.FirstOrDefault(t => t.PhoneNumber == employeeUsername && t.Password == employeePassword);
                        if (employee == null || !Regex.IsMatch(employeeUsername, patternPhoneNumber))
                        {
                            Actions.IncorrectPhonenumber();
                            goto EmployeeLogin;
                        }
                        else
                        {
                            employeeName = employee.Name +" "+ employee.Family;
                        }
                    }
                #endregion

                #region Menu
                employeeMenu:
                    Actions.DateTimeWithUserStatistic(employeeName);
                    Console.Write("\n\t\t\t\t1.  View Employees List");
                    Console.WriteLine("\t\t\t2.  View Students List");
                    Console.Write("\n\t\t\t\t3.  View Masters List");
                    Console.WriteLine("\t\t\t4.  View Courses List\n");

                    Console.Write("\n\t\t\t\t5.  Add New Student");
                    Console.WriteLine("\t\t\t6.  Add New Master ");
                    Console.Write("\n\t\t\t\t\t\t    7.  Add New Course\n");

                    Console.Write("\n\t\t\t\t8.  Edit Student Information ");
                    Console.WriteLine("\t\t9. Edit Master Information  ");
                    Console.Write("\n\t\t\t\t10. Edit Course Information ");
                    Console.WriteLine("\t\t11.  Edit My Profile\n");

                    Console.Write("\n\t\t\t\t12. Remove Student");
                    Console.WriteLine("\t\t\t13. Remove Master");
                    Console.Write("\n\t\t\t\t14. Remove Course");
                    Console.WriteLine("\t\t\t15. Exit The Dashboard");

                    Console.Write("\n\n\t\t\t\t\t\t    Your request number : ");
                    int employeeRequest;
                    while (!int.TryParse(Console.ReadLine(), out employeeRequest))
                    {
                        goto employeeMenu;
                    }
                    #endregion

                    switch (employeeRequest)
                    {
                        #region View Employees List
                        case 1:
                            Actions.DateTimeWithUserStatistic(employeeName);
                            Actions.PrintEmployees();
                            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                            Console.ReadKey();
                            goto employeeMenu;
                        #endregion

                        #region View Students List
                        case 2:
                            Actions.DateTimeWithUserStatistic(employeeName);
                            Actions.PrintStudents();
                            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                            Console.ReadKey();
                            goto employeeMenu;
                        #endregion

                        #region View Masters List
                        case 3:
                            Actions.DateTimeWithUserStatistic(employeeName);
                            Actions.PrintMasters();
                            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                            Console.ReadKey();
                            goto employeeMenu;
                        #endregion

                        #region View Courses List
                        case 4:
                            Actions.DateTimeWithUserStatistic(employeeName);
                            Actions.PrintCourse();
                            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                            Console.ReadKey();
                            goto employeeMenu;
                        #endregion

                        #region Add New Student
                        case 5:
                            Actions.AddStudent();
                            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                            Console.ReadKey();
                            goto employeeMenu;
                        #endregion

                        #region Add New Master
                        case 6:
                            Actions.AddMaster();
                            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                            Console.ReadKey();
                            goto employeeMenu;
                        #endregion

                        #region Add New Course
                        case 7:
                            Actions.AddCourse();
                            Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                            Console.ReadKey();
                            goto employeeMenu;
                        #endregion

                        #region Edit My Profile
                        case 11:      
                            using (UnivercityContext dbEditEmployee = new UnivercityContext("UniDBConStr"))
                            {
                            editEmployeeMenu:
                                var employee = dbEditEmployee.Employees.FirstOrDefault(t => t.PhoneNumber == employeeUsername && t.Password == employeePassword);
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                Console.WriteLine($"\n\t\t\t\t\t\tChosse property for edit");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1. Name" +
                                                  "\n\t\t\t\t\t\t\t2. Family" +
                                                  "\n\t\t\t\t\t\t\t3. Phonenumber" +
                                                  "\n\t\t\t\t\t\t\t4. Password" +
                                                  "\n\t\t\t\t\t\t\t5. Return the menu ");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditEmployee;
                                while (!int.TryParse(Console.ReadLine(), out answerEditEmployee))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editEmployeeMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                switch (answerEditEmployee)
                                {
                                    #region Name
                                    case 1:
                                    employeeName:
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {employee.Name} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = employee.Name;
                                        string newNameForEmployee = Console.ReadLine();
                                        if (newNameForEmployee == employee.Name)
                                        {
                                            Actions.DuplicateItem(newNameForEmployee, "Name");
                                            goto employeeName;
                                        }
                                        employee.Name = newNameForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForEmployee} for you \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Family
                                    case 2:
                                    employeeFamily:                 
                                        Console.WriteLine($"\n\t\t\t\t\t\t     family     : {employee.Family} ");
                                        string family = employee.Family;
                                        Console.Write("\n\t\t\t\t\t\t     New family : ");
                                        string newFamilyForEmployee = Console.ReadLine();
                                        if (newFamilyForEmployee == employee.Family)
                                        {
                                            Actions.DuplicateItem(newFamilyForEmployee, "Family");
                                            goto employeeFamily;
                                        }
                                        employee.Family = newFamilyForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The family was changed to {newFamilyForEmployee} for you \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region PhoneNumber
                                    case 3:
                                    newPhoneNumberForEmployee:
                                        Console.WriteLine($"\n\t\t\t\t\t\t     PhoneNumber     : {employee.PhoneNumber} ");
                                        Console.Write("\n\t\t\t\t\t\t     New phonenumber : ");
                                        string newPhoneNumberForEmployee = Console.ReadLine();
                                        while (!Regex.IsMatch(newPhoneNumberForEmployee, patternPhoneNumber))
                                        {
                                            Actions.IncorrectPhonenumber();
                                            goto newPhoneNumberForEmployee;
                                        }
                                        if (newPhoneNumberForEmployee == employee.PhoneNumber)
                                        {
                                            Actions.DuplicateItem(newPhoneNumberForEmployee, "PhoneNumber");
                                            goto newPhoneNumberForEmployee;
                                        }
                                        employee.PhoneNumber = newPhoneNumberForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The phonenumber was changed to {newPhoneNumberForEmployee} for you \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Password
                                    case 6:
                                    employeePassword:
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Password     : {employee.Password} ");
                                        Console.Write("\n\t\t\t\t\t\t     New password : ");
                                        string newPasswordForEmployee = Console.ReadLine();
                                        if (newPasswordForEmployee == employee.Password)
                                        {
                                            Actions.DuplicateItem(newPasswordForEmployee, "Password");
                                            goto employeePassword;
                                        }
                                        employee.Password = newPasswordForEmployee;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The password was changed to {newPasswordForEmployee} for you \n");
                                        Thread.Sleep(3000);
                                        dbEditEmployee.SaveChanges();
                                        goto editEmployeeMenu;
                                    #endregion

                                    #region Return the menu
                                    case 5: goto employeeMenu;
                                    #endregion

                                    default: goto editEmployeeMenu;
                                }
                            }  
                        #endregion



                        //bug remove course
                        #region Edit Student Information
                        case 8:
                        editStudent:
                            Actions.DateTimeWithoutUserStatistic(employeeName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select student id for edit\n");
                            Actions.PrintStudents();
                            Console.Write("\n\t\t\t\t\t\tStudent id is : ");
                            int answerStudentId;
                            while (!int.TryParse(Console.ReadLine(), out answerStudentId))
                            {
                                Actions.WarningJustNumber("Student id");
                                goto editStudent;
                            }
                            UnivercityContext dbEditStudent = new UnivercityContext("UniDBConStr");
                            using (dbEditStudent)
                            {
                                var studentForEdit = dbEditStudent.Students.Find(answerStudentId);
                                if (studentForEdit == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(employeeName);
                                    Actions.WarningNotFoundId("Student list", answerStudentId);
                                    goto editStudent;
                                }
                            editStudentMenu:
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                Console.WriteLine($"\n\t\t\t\t\tChsse property for edit student : {studentForEdit.Name} {studentForEdit.Family}");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1.  Degree" +
                                                  "\n\t\t\t\t\t\t\t2.  Code" +
                                                  "\n\t\t\t\t\t\t\t3.  Name" +
                                                  "\n\t\t\t\t\t\t\t4.  Family" +
                                                  "\n\t\t\t\t\t\t\t5.  Phonenumber" +
                                                  "\n\t\t\t\t\t\t\t6.  Password" +
                                                  "\n\t\t\t\t\t\t\t7.  Access" +
                                                  "\n\t\t\t\t\t\t\t8.  Add course" +
                                                  "\n\t\t\t\t\t\t\t9.  Remove course" +
                                                  "\n\t\t\t\t\t\t\t10. Return the menu");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditStudent;
                                while (!int.TryParse(Console.ReadLine(), out answerEditStudent))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editStudentMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                switch (answerEditStudent)
                                {
                                    #region Degree
                                    case 1:
                                    studentDegree:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Degree     : {studentForEdit.Degree} ");
                                        Console.Write("\n\t\t\t\t\t\t     New degree : ");
                                        string newDegreeForStudent = Console.ReadLine();
                                        if (newDegreeForStudent == studentForEdit.Degree)
                                        {
                                            Actions.DuplicateItem(newDegreeForStudent, "Degree");
                                            goto studentDegree;
                                        }
                                        studentForEdit.Degree = newDegreeForStudent;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The degree was changed to {newDegreeForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Code
                                    case 2:
                                    newCodeForStudent:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Code     : {studentForEdit.StudentCode} ");
                                        Console.Write("\n\t\t\t\t\t\t     New code : ");
                                        int newCodeForStudent;
                                        while (!int.TryParse(Console.ReadLine(), out newCodeForStudent))
                                        {
                                            Actions.WarningJustNumber("Student code");
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            goto newCodeForStudent;
                                        }
                                        if (newCodeForStudent == studentForEdit.StudentCode)
                                        {
                                            Actions.DuplicateItem($"{newCodeForStudent}", "StudentCode");
                                            goto newCodeForStudent;
                                        }
                                        studentForEdit.StudentCode = newCodeForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The code was changed to {newCodeForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Name
                                    case 3:
                                    studentName:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {studentForEdit.Name} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = studentForEdit.Name;
                                        string newNameForEmployee = Console.ReadLine();
                                        if (newNameForEmployee == studentForEdit.Name)
                                        {
                                            Actions.DuplicateItem(newNameForEmployee, "Name");
                                            goto studentName;
                                        }
                                        studentForEdit.Name = newNameForEmployee;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForEmployee} for {name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Family
                                    case 4:
                                    studentFamily:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Family     : {studentForEdit.Family} ");
                                        string family = studentForEdit.Family;
                                        Console.Write("\n\t\t\t\t\t\t     New family : ");
                                        string newFamilyForStudent = Console.ReadLine();
                                        if (newFamilyForStudent == studentForEdit.Family)
                                        {
                                            Actions.DuplicateItem(newFamilyForStudent, "Family");
                                            goto studentFamily;
                                        }
                                        studentForEdit.Family = newFamilyForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The family was changed to {newFamilyForStudent} for {studentForEdit.Name} {family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region PhoneNumber
                                    case 5:
                                    newPhoneNumberForStudent:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     PhoneNumber     : {studentForEdit.PhoneNumber} ");
                                        Console.Write("\n\t\t\t\t\t\t     New phonenumber : ");
                                        string newPhoneNumberForStudent = Console.ReadLine();
                                        while (!Regex.IsMatch(newPhoneNumberForStudent, patternPhoneNumber))
                                        {
                                            Actions.IncorrectPhonenumber();
                                            goto newPhoneNumberForStudent;
                                        }
                                        if (newPhoneNumberForStudent == studentForEdit.PhoneNumber)
                                        {
                                            Actions.DuplicateItem(newPhoneNumberForStudent, "PhoneNumber");
                                            goto newPhoneNumberForStudent;
                                        }
                                        studentForEdit.PhoneNumber = newPhoneNumberForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The phonenumber was changed to {newPhoneNumberForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Password
                                    case 6:
                                    studentPassword:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Password     : {studentForEdit.Password} ");
                                        Console.Write("\n\t\t\t\t\t\t     New password : ");
                                        string newPasswordForStudent = Console.ReadLine();
                                        if (newPasswordForStudent == studentForEdit.Password)
                                        {
                                            Actions.DuplicateItem(newPasswordForStudent, "Password");
                                            goto studentPassword;
                                        }
                                        studentForEdit.Password = newPasswordForStudent;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The password was changed to {newPasswordForStudent} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Access
                                    case 7:
                                        if (studentForEdit.IsActive == true)
                                        {
                                            studentForEdit.IsActive = false;
                                        }
                                        else
                                        {
                                            studentForEdit.IsActive = true;
                                        }

                                        Console.WriteLine($"\n\t\t\t\t\t\t\tThe Access was changed to {studentForEdit.IsActive} for {studentForEdit.Name} {studentForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    #region Add course
                                    case 8:
                                    answerCourseId:
                                        if (studentForEdit.Courses.Count == dbEditStudent.Courses.Count())
                                        {
                                            Actions.AlreadySelected();
                                            goto editStudentMenu;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(employeeName);
                                        List<Course> coursesList = studentForEdit.Courses.ToList();
                                        Console.WriteLine($"\n\t\t\t\tPlease select a course for add to student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Actions.PrintCourse();
                                        Console.Write("\n\t\t\t\t\t\t\tCourse id is : ");
                                        int courseIdForStudent;
                                        while (!int.TryParse(Console.ReadLine(), out courseIdForStudent))
                                        {
                                            Actions.WarningJustNumber("Course id");
                                            goto answerCourseId;
                                        }
                                        var courseForStudent = dbEditStudent.Courses.Find(courseIdForStudent);
                                        if (courseForStudent == null)
                                        {
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            Actions.WarningNotFoundId("Course list", courseIdForStudent);
                                            goto answerCourseId;
                                        }
                                        if (coursesList.Any(t => t.CourseId == courseForStudent.CourseId))
                                        {
                                            Actions.DuplicateItem(courseForStudent.CourseName, "Course");
                                            goto answerCourseId;
                                        }
                                        else
                                        {
                                            studentForEdit.Courses.Add(courseForStudent);
                                        }
                                        Actions.DateTimeWithoutUserStatistic(employeeName);
                                        Console.WriteLine($"\n\t\t\t\t\t{courseForStudent.CourseName} course added to student : {studentForEdit.Name} {studentForEdit.Family}");
                                        Thread.Sleep(3000);
                                        dbEditStudent.SaveChanges();
                                        goto editStudentMenu;
                                    #endregion

                                    //bug
                                    #region Remove course
                                    case 9:
                                    RemoveCourse:
                                        Actions.DateTimeWithoutUserStatistic(employeeName);
                                        Console.WriteLine("\nPlease select course id for remove\n");
                                        List<Course> courseList = studentForEdit.Courses.ToList();
                                        //foreach (var course in courseList)
                                        //{
                                        //    Console.WriteLine($"\tId :{course.CourseId}    Name :{course.CourseName}    Unit :{course.CourseUnit}    Is active:{course.IsActive}    Registerdate : {course.Registerdate} ");
                                        //    if (course.Masterss.Any())
                                        //    {
                                        //        Console.WriteLine("\t____________________________________________________________________________________________________________________\n");

                                        //        Console.WriteLine("\n\tMaster : ");
                                        //        foreach (var master in course.Masterss)
                                        //        {
                                        //            Console.WriteLine($"\n\tId : {master.UserId}     Name : {master.Name}     Family : {master.Family}     Phonenumber : {master.PhoneNumber}     Password : {master.Password}\n\n\tSalary :{master.Salary}      Degree : {master.Degree}     Is active : {master.IsActive}     Birthdate : {master.Birthdate}     RegisterDate : {master.RegisterDate}\n ");
                                        //        }
                                        //    }
                                        //    Console.WriteLine("\n____________________________________________________________________________________________________________________________\n");
                                        //}
                                        Console.Write("\nCourse id is : ");
                                        int corseIdForRemoveForStudent;
                                        while (!int.TryParse(Console.ReadLine(), out corseIdForRemoveForStudent))
                                        {
                                            Actions.WarningJustNumber("Course id");
                                            goto RemoveCourse;
                                        }

                                        UnivercityContext dbRemoveCourse = new UnivercityContext("UniDBConStr");
                                        using (dbRemoveCourse)
                                        {
                                            Course course = courseList.Find(t => t.CourseId == corseIdForRemoveForStudent);

                                            if (course == null)
                                            {
                                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                                Actions.WarningNotFoundId("Course list", corseIdForRemoveForStudent);
                                                goto RemoveCourse;
                                            }
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            //dbRemoveCourse.Courses.Remove(course);
                                            //courseList.Remove(course);
                                            Console.WriteLine($"\n\t{course.CourseName} was removed\n");
                                            Thread.Sleep(3000);
                                            dbRemoveCourse.SaveChanges();
                                        }

                                        goto editStudentMenu;
                                    #endregion

                                    #region Return the menu
                                    case 10:
                                        goto employeeMenu;
                                    #endregion

                                    default:
                                        goto editStudentMenu;
                                }
                            }
                        #endregion      

                        #region Edit Master Information
                        case 9:
                        editMaster:
                            Actions.DateTimeWithoutUserStatistic(employeeName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select master id for edit\n");
                            Actions.PrintMasters();
                            Console.Write("\n\t\t\t\t\t\tMaster id is : ");
                            int answerMasterId;
                            while (!int.TryParse(Console.ReadLine(), out answerMasterId))
                            {
                                Actions.WarningJustNumber("Master id");
                                goto editMaster;
                            }
                            UnivercityContext dbEditMaster = new UnivercityContext("UniDBConStr");
                            using (dbEditMaster)
                            {
                                var masterForEdit = dbEditMaster.Masters.Find(answerMasterId);
                                if (masterForEdit == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(employeeName);
                                    Actions.WarningNotFoundId("Master list", answerMasterId);
                                    goto editMaster;
                                }
                            editMasterMenu:
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                Console.WriteLine($"\n\t\t\t\t\tChsse property for edit master : {masterForEdit.Name} {masterForEdit.Family}");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1. Degree" +
                                                  "\n\t\t\t\t\t\t\t2. Salary" +
                                                  "\n\t\t\t\t\t\t\t3. Name" +
                                                  "\n\t\t\t\t\t\t\t4. Family" +
                                                  "\n\t\t\t\t\t\t\t5. Phonenumber" +
                                                  "\n\t\t\t\t\t\t\t6. Password" +
                                                  "\n\t\t\t\t\t\t\t7. Access" +
                                                  "\n\t\t\t\t\t\t\t8. Return the menu");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditMaster;
                                while (!int.TryParse(Console.ReadLine(), out answerEditMaster))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editMasterMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                switch (answerEditMaster)
                                {
                                    #region Degree
                                    case 1:
                                    masterDegree:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Degree     : {masterForEdit.Degree} ");
                                        Console.Write("\n\t\t\t\t\t\t     New degree : ");
                                        string newDegreeForMaster = Console.ReadLine();
                                        if (newDegreeForMaster == masterForEdit.Degree)
                                        {
                                            Actions.DuplicateItem(newDegreeForMaster, "Degree");
                                            goto masterDegree;
                                        }
                                        masterForEdit.Degree = newDegreeForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The degree was changed to {newDegreeForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Salary
                                    case 2:
                                    newSalaryForEmployee:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Salary     : {masterForEdit.Salary} ");
                                        Console.Write("\n\t\t\t\t\t\t     New salary : ");
                                        float newSalaryForMaster;
                                        while (!float.TryParse(Console.ReadLine(), out newSalaryForMaster))
                                        {
                                            Actions.WarningJustNumber("Monthly salary");
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            goto newSalaryForEmployee;
                                        }
                                        if (newSalaryForMaster == masterForEdit.Salary)
                                        {
                                            Actions.DuplicateItem($"{newSalaryForMaster}", "Salary");
                                            goto newSalaryForEmployee;
                                        }
                                        masterForEdit.Salary = newSalaryForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The salary was changed to {newSalaryForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Name
                                    case 3:
                                    masterName:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {masterForEdit.Name} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = masterForEdit.Name;
                                        string newNameForMaster = Console.ReadLine();
                                        if (newNameForMaster == masterForEdit.Name)
                                        {
                                            Actions.DuplicateItem(newNameForMaster, "Name");
                                            goto masterName;
                                        }
                                        masterForEdit.Name = newNameForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForMaster} for {name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Family
                                    case 4:
                                    masterFamily:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     family     : {masterForEdit.Family} ");
                                        string family = masterForEdit.Family;
                                        Console.Write("\n\t\t\t\t\t\t     New family : ");
                                        string newFamilyForMaster = Console.ReadLine();
                                        if (newFamilyForMaster == masterForEdit.Family)
                                        {
                                            Actions.DuplicateItem(newFamilyForMaster, "Family");
                                            goto masterFamily;
                                        }
                                        masterForEdit.Family = newFamilyForMaster;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The family was changed to {newFamilyForMaster} for {masterForEdit.Name} {family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region PhoneNumber
                                    case 5:
                                    newPhoneNumberForMaster:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     PhoneNumber     : {masterForEdit.PhoneNumber} ");
                                        Console.Write("\n\t\t\t\t\t\t     New phonenumber : ");
                                        string newPhoneNumberForMaster = Console.ReadLine();
                                        while (!Regex.IsMatch(newPhoneNumberForMaster, patternPhoneNumber))
                                        {
                                            Actions.IncorrectPhonenumber();
                                            goto newPhoneNumberForMaster;
                                        }
                                        if (newPhoneNumberForMaster == masterForEdit.PhoneNumber)
                                        {
                                            Actions.DuplicateItem(newPhoneNumberForMaster, "PhoneNumber");
                                            goto newPhoneNumberForMaster;
                                        }
                                        masterForEdit.PhoneNumber = newPhoneNumberForMaster;

                                        Console.WriteLine($"\n\n\t\t\t\t\t  The phonenumber was changed to {newPhoneNumberForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Password
                                    case 6:
                                    masterPassword:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for master : {masterForEdit.Name} {masterForEdit.Family}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Password     : {masterForEdit.Password} ");
                                        Console.Write("\n\t\t\t\t\t\t     New password : ");
                                        string newPasswordForMaster = Console.ReadLine();
                                        if (newPasswordForMaster == masterForEdit.Password)
                                        {
                                            Actions.DuplicateItem(newPasswordForMaster, "Password");
                                            goto masterPassword;
                                        }
                                        masterForEdit.Password = newPasswordForMaster;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The password was changed to {newPasswordForMaster} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Access
                                    case 7:
                                        if (masterForEdit.IsActive == true)
                                        {
                                            masterForEdit.IsActive = false;
                                        }
                                        else
                                        {
                                            masterForEdit.IsActive = true;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(employeeName);
                                        Console.WriteLine($"\n\tThe access was changed to {masterForEdit.IsActive} for {masterForEdit.Name} {masterForEdit.Family} \n");
                                        Thread.Sleep(3000);
                                        dbEditMaster.SaveChanges();
                                        goto editMasterMenu;
                                    #endregion

                                    #region Return the menu
                                    case 8:
                                        goto employeeMenu;
                                    #endregion

                                    default:
                                        goto editMasterMenu;
                                }
                            }
                        #endregion



                        //bug change master and remove master
                        #region Edit Course Information 
                        case 10:
                        editCourse:
                            Actions.DateTimeWithoutUserStatistic(employeeName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select course id for edit\n");
                            Actions.PrintCourse();
                            Console.Write("\n\t\t\t\t\t\tCourse id is : ");
                            int answerCourseId;
                            while (!int.TryParse(Console.ReadLine(), out answerCourseId))
                            {
                                Actions.WarningJustNumber("Course id");
                                goto editCourse;
                            }
                            UnivercityContext dbEditCourse = new UnivercityContext("UniDBConStr");
                            using (dbEditCourse)
                            {
                                var courseForEdit = dbEditCourse.Courses.Find(answerCourseId);
                                if (courseForEdit == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(employeeName);
                                    Actions.WarningNotFoundId("Course list", answerCourseId);
                                    goto editCourse;
                                }
                            editCourseMenu:
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                Console.WriteLine($"\n\t\t\t\t\tChsse property for edit course : {courseForEdit.CourseName}");
                                Console.WriteLine("\n\t\t\t\t\t\t\t1. Name" +
                                                  "\n\t\t\t\t\t\t\t2. Unit" +
                                                  "\n\t\t\t\t\t\t\t3. Add Master" +
                                                  "\n\t\t\t\t\t\t\t4. Change Master" +
                                                  "\n\t\t\t\t\t\t\t5. Return the menu");
                                Console.Write("\n\t\t\t\t\t\t   Your answer number :");
                                int answerEditCourse;
                                while (!int.TryParse(Console.ReadLine(), out answerEditCourse))
                                {
                                    Actions.WarningJustNumber("Select item of menu");
                                    goto editCourseMenu;
                                }
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                switch (answerEditCourse)
                                {
                                    #region Name
                                    case 1:
                                    courseName:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for course : {courseForEdit.CourseName}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Name     : {courseForEdit.CourseName} ");
                                        Console.Write("\n\t\t\t\t\t\t     New name : ");
                                        string name = courseForEdit.CourseName;
                                        string newNameForCourse = Console.ReadLine();
                                        if (newNameForCourse == courseForEdit.CourseName)
                                        {
                                            Actions.DuplicateItem(newNameForCourse, "Name");
                                            goto courseName;
                                        }
                                        courseForEdit.CourseName = newNameForCourse;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The name was changed to {newNameForCourse} for {name} \n");
                                        Thread.Sleep(3000);
                                        dbEditCourse.SaveChanges();
                                        goto editCourseMenu;
                                    #endregion

                                    #region Unit
                                    case 2:
                                    newSalaryForCourse:
                                        Console.WriteLine($"\n\t\t\t\t\t    Edit form for course : {courseForEdit.CourseName}");
                                        Console.WriteLine($"\n\t\t\t\t\t\t     Unit     : {courseForEdit.CourseUnit} ");
                                        Console.Write("\n\t\t\t\t\t\t     New unit : ");
                                        int newUnitForCourse;
                                        while (!int.TryParse(Console.ReadLine(), out newUnitForCourse))
                                        {
                                            Actions.WarningJustNumber("Course name");
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            goto newSalaryForCourse;
                                        }
                                        if (newUnitForCourse == courseForEdit.CourseUnit)
                                        {
                                            Actions.DuplicateItem($"{newUnitForCourse}", "Unit");
                                            goto newSalaryForCourse;
                                        }
                                        courseForEdit.CourseUnit = newUnitForCourse;
                                        Console.WriteLine($"\n\n\t\t\t\t\t  The unit was changed to {newUnitForCourse} for {courseForEdit.CourseName}\n");
                                        Thread.Sleep(3000);
                                        dbEditCourse.SaveChanges();
                                        goto editCourseMenu;
                                    #endregion

                                    #region Add Master
                                    case 3:
                                    addMasterForCourse:
                                        if (courseForEdit.Masters.Count == dbEditCourse.Masters.Count())
                                        {
                                            Actions.AlreadySelected();
                                            goto editCourseMenu;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(employeeName);
                                        List<Master> mastersList = courseForEdit.Masters.ToList();
                                        if (mastersList.Any())
                                        {
                                            Console.WriteLine($"\n\t\t\t\t\t\t\tMaster: {courseForEdit.CourseName}");
                                            foreach (var master in mastersList)
                                            {
                                                Console.WriteLine($"\n\t\t\t\t\tId : {master.UserId}     Name : {master.Name}     Family : {master.Family}");
                                            }
                                            Console.WriteLine("\n\t    ______________________________________________________________________________________________________");

                                        }
                                        Console.WriteLine($"\n\t\t\t\t\t\tPlease select master id for add to {courseForEdit.CourseName}\n");
                                        Actions.PrintMasters();
                                        Console.Write("\n\t\t\t\t\t\t\tMaster id is : ");
                                        int masterIdForAddCourse;
                                        while (!int.TryParse(Console.ReadLine(), out masterIdForAddCourse))
                                        {
                                            Actions.WarningJustNumber("Master id");
                                            goto addMasterForCourse;
                                        }
                                        var newMasterForCourse = dbEditCourse.Masters.Find(masterIdForAddCourse);
                                        if (newMasterForCourse == null)
                                        {
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            Actions.WarningNotFoundId("Course list", masterIdForAddCourse);
                                            goto addMasterForCourse;
                                        }
                                        if (mastersList.Any(t => t.UserId == newMasterForCourse.UserId))
                                        {
                                            Actions.DuplicateItem($"{newMasterForCourse.Name}{newMasterForCourse.Family}", "Master");
                                            goto addMasterForCourse;
                                        }
                                        else
                                        {
                                            courseForEdit.Masters.Add(newMasterForCourse);
                                        }
                                        Actions.DateTimeWithoutUserStatistic(employeeName);
                                        Console.WriteLine($"\n\n\t\t\t\t\t\tMaster {newMasterForCourse.Name} {newMasterForCourse.Family} added to course : {courseForEdit.CourseName}");
                                        Thread.Sleep(3000);
                                        dbEditCourse.SaveChanges();
                                        goto editCourseMenu;

                                    #endregion

                                    //bug
                                    //هنگام وارد کردن مستر ایدی برای تغییر اگر ایدی ای وارد شه  که توی لیست استاد های این درس نیست هیچ اروری نمیده و میاد درس جایگزین رو اضافه میکنه به لیست استاد های این درس
                                    #region Change Master
                                    case 4:
                                    changeMasterForCourse1:

                                        if (courseForEdit.Masters.Count == dbEditCourse.Masters.Count())
                                        {
                                            Actions.AlreadySelected();
                                            goto editCourseMenu;
                                        }
                                        Actions.DateTimeWithoutUserStatistic(employeeName);
                                        List<Master> mastersList1 = courseForEdit.Masters.ToList();
                                        if (!mastersList1.Any())
                                        {
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            Console.WriteLine("\n\t\t\t\t\t\t\tthis course doesnt have any master ...");
                                            Thread.Sleep(3000);
                                            goto editCourseMenu;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\n\t\t\t\t\t\t\tMaster: {courseForEdit.CourseName}");
                                            foreach (var master in mastersList1)
                                            {
                                                Console.WriteLine($"\n\t\t\t\t\tId : {master.UserId}     Name : {master.Name}     Family : {master.Family}");
                                            }
                                            Console.WriteLine("\n\t    ______________________________________________________________________________________________________");

                                            Console.WriteLine("\n\t\t\t\t\t\tWhich teacher do you want to change?");
                                            Console.Write("\n\t\t\t\t\t\t\tMaster id is : ");
                                            int masterIdForChangeCourse1;
                                            while (!int.TryParse(Console.ReadLine(), out masterIdForChangeCourse1))
                                            {
                                                Actions.WarningJustNumber("Master id");
                                                goto changeMasterForCourse1;
                                            }
                                            var changeMasterForCourse = dbEditCourse.Masters.Find(masterIdForChangeCourse1);
                                            if (changeMasterForCourse == null)
                                            {
                                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                                Actions.WarningNotFoundId("Master list", masterIdForChangeCourse1);
                                                goto changeMasterForCourse1;
                                            }
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                        changeMasterForCourse2:
                                            Console.WriteLine($"\n\t\t\t\tPlease enter the ID of the master you want to replace with {changeMasterForCourse.Name} {changeMasterForCourse.Family}\n");
                                            Actions.PrintMasters();
                                            Console.Write("\n\t\t\t\t\t\t\tMaster id is : ");
                                            int masterIdForChangeCourse2;
                                            while (!int.TryParse(Console.ReadLine(), out masterIdForChangeCourse2))
                                            {
                                                Actions.WarningJustNumber("Master id");
                                                goto changeMasterForCourse2;
                                            }
                                            var replaceMasterForCourse = dbEditCourse.Masters.Find(masterIdForChangeCourse2);
                                            if (replaceMasterForCourse == null)
                                            {
                                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                                Actions.WarningNotFoundId("Master list", masterIdForChangeCourse2);
                                                goto changeMasterForCourse2;
                                            }
                                            if (mastersList1.Any(t => t.UserId == replaceMasterForCourse.UserId))
                                            {
                                                Actions.DuplicateItem($"{replaceMasterForCourse.Name}", "Master");
                                                goto changeMasterForCourse2;
                                            }
                                            else
                                            {
                                                courseForEdit.Masters.Remove(changeMasterForCourse);
                                                courseForEdit.Masters.Add(replaceMasterForCourse);
                                            }
                                            Actions.DateTimeWithoutUserStatistic(employeeName);
                                            Console.WriteLine($"\n\t\t\t\t\tMaster {changeMasterForCourse.Name} {changeMasterForCourse.Family} replace with  master {replaceMasterForCourse.Name} {replaceMasterForCourse.Family} for course : {courseForEdit.CourseName}");
                                            Thread.Sleep(3000);
                                            dbEditCourse.SaveChanges();
                                        }
                                        goto editCourseMenu;
                                    #endregion

                                    #region Return the menu
                                    case 5: goto employeeMenu;
                                    #endregion

                                    default: goto editCourseMenu;

                                }
                            }
                        //break;
                        #endregion


                        #region Remove Student
                        case 12:
                        studentForRemove:
                            Actions.DateTimeWithoutUserStatistic(employeeName);
                            Console.WriteLine("\n\t\t\t\t\t  Please select student id for remove\n");
                            Actions.PrintStudents();
                            Console.Write("\n\t\t\t\t\t\tStudent id is : ");
                            int studentIdForRemove;
                            while (!int.TryParse(Console.ReadLine(), out studentIdForRemove))
                            {
                                Actions.WarningJustNumber("Student id");
                                goto studentForRemove;
                            }
                            UnivercityContext dbRemoveStudent = new UnivercityContext("UniDBConStr");
                            using (dbRemoveStudent)
                            {
                                var studentForRemove = dbRemoveStudent.Students.Find(studentIdForRemove);
                                if (studentForRemove == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(employeeName);
                                    Actions.WarningNotFoundId("Student list", studentIdForRemove);
                                    goto studentForRemove;
                                }
                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                Console.WriteLine($"\n\t\t\t\t\t\t   {studentForRemove.Name} {studentForRemove.Family} was removed\n");
                                dbRemoveStudent.Students.Remove(studentForRemove);
                                dbRemoveStudent.SaveChanges();
                                Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                                Console.ReadKey();
                            }
                            goto employeeMenu;
                        #endregion

                        #region Remove Master
                        case 13:
                        masterForRemove:
                            Actions.DateTimeWithoutUserStatistic(employeeName);
                            Console.WriteLine("\n\t\t\t\t\t  Please select master id for remove\n");
                            Actions.PrintMasters();
                            Console.Write("\n\t\t\t\t\t\t  Master id is : ");
                            int masterIdForRemove;
                            while (!int.TryParse(Console.ReadLine(), out masterIdForRemove))
                            {
                                Actions.WarningJustNumber("Master id");
                                goto masterForRemove;
                            }
                            UnivercityContext dbRemoveMaster = new UnivercityContext("UniDBConStr");
                            using (dbRemoveMaster)
                            {
                                var masterForRemove = dbRemoveMaster.Masters.Find(masterIdForRemove);
                                if (masterForRemove == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(employeeName);
                                    Actions.WarningNotFoundId("Master list", masterIdForRemove);
                                    goto masterForRemove;
                                }
                                //نفهمیدم
                                //var removeCourseMaster = dbRemoveMaster.Courses.Where(t => t.Master.UserId == masterForRemove.UserId);

                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                Console.WriteLine($"\n\t\t\t\t\t\t    {masterForRemove.Name} {masterForRemove.Family} was removed\n");
                                dbRemoveMaster.Masters.Remove(masterForRemove);
                                dbRemoveMaster.SaveChanges();
                                Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                                Console.ReadKey();
                            }
                            goto employeeMenu;

                        #endregion


                        //bug
                        #region Remove Course
                        case 16:
                        courseForRemove:
                            Actions.DateTimeWithoutUserStatistic(employeeName);
                            Console.WriteLine("\n\t\t\t\t\t\tPlease select course id for remove\n");
                            Actions.PrintCourse();
                            Console.Write("\n\t\t\t\t\t\tCourse id is : ");
                            int corseIdForRemove;
                            while (!int.TryParse(Console.ReadLine(), out corseIdForRemove))
                            {
                                Actions.WarningJustNumber("Course id");
                                goto courseForRemove;
                            }
                            UnivercityContext dbRemoveCorse = new UnivercityContext("UniDBConStr");
                            using (dbRemoveCorse)
                            {
                                var courseForRemove = dbRemoveCorse.Courses.Find(corseIdForRemove);
                                if (courseForRemove == null)
                                {
                                    Actions.DateTimeWithoutUserStatistic(employeeName);
                                    Actions.WarningNotFoundId("Course list", corseIdForRemove);
                                    goto courseForRemove;
                                }
                                //if (dbRemoveCorse.Courses.Any(t => t.Masterss != null))
                                //{
                                //    dbRemoveCorse.Courses.Remove(courseForRemove);
                                //}
                                //else
                                //{

                                //}

                                //dbRemoveCorse.Courses.Any(t => t.Masterss == null);

                                //else
                                //{
                                //    List<Master> masters = courseForRemove.Masterss.ToList();
                                //    foreach (var master in masters)
                                //    {

                                //        dbRemoveCorse.Masters.Remove(master);
                                //    }
                                //}  

                                Actions.DateTimeWithoutUserStatistic(employeeName);
                                Console.WriteLine($"\n\t\t\t\t\t\t{courseForRemove.CourseName} was removed\n");
                                dbRemoveCorse.Courses.Remove(courseForRemove);
                                dbRemoveCorse.SaveChanges();
                                Console.WriteLine("\n\t\t\t\t\t\tDo you want to return the menu ?");
                                Console.ReadKey();




                                //    Console.WriteLine($"\n\t{studentForRemove.Name} {studentForRemove.Family} was removed\n");
                                //    dbRemoveStudent.Students.Remove(studentForRemove);
                                //    dbRemoveStudent.SaveChanges();
                            }
                            goto employeeMenu;



                        #endregion


                        #region Exit The Dashboard
                        case 18:
                            Actions.DateTimeWithoutUserStatistic(employeeName);
                            Console.WriteLine($"\n\n\n\n\t\t\t\t\t\tHave a good time {employeeName}");
                            Thread.Sleep(3000);
                            Console.Clear();
                            goto loginMenu;
                        #endregion

                        default:
                            goto employeeMenu;
                    }
                #endregion

                #region Master
                case 3:

                #region  Logged Master
                MasterLogin:
                    Master masterMain;
                    Actions.JustDateTime();
                    Console.Write("\n\n\n\t\t\t\t\t\tPhonenumber : ");
                    string materUsername = Console.ReadLine();
                    Console.Write("\n\t\t\t\t\t\tPassword    : ");
                    string masterPassword = Console.ReadLine();
                    string masterName = "";
                    using (UnivercityContext dbMasterLogin = new UnivercityContext("UniDBConStr"))
                    {
                        masterMain = dbMasterLogin.Masters.FirstOrDefault(t => t.PhoneNumber == materUsername && t.Password == masterPassword);
                        if (masterMain == null || !Regex.IsMatch(materUsername, patternPhoneNumber))
                        {
                            Actions.IncorrectPhonenumber();
                            goto MasterLogin;
                        }
                        else
                        {
                            masterName = masterMain.Name + masterMain.Family;
                        }
                    }

                #endregion

                #region Menu
                masterMenu:
                    Actions.DateTimeWithUserStatistic(masterName);
                    Console.Write("\n\t\t\t\t1.  View Students List");
                    Console.WriteLine("\t\t\t2.  View Courses List");
                    Console.Write("\n\t\t\t\t3.  View My Profile");
                    Console.WriteLine("\t\t\t4.  Exit The Dashboard\n");

                    Console.Write("\n\n\t\t\t\t\t\t    Your request number : ");
                    int masterRequest;
                    while (!int.TryParse(Console.ReadLine(), out masterRequest))
                    {
                        goto masterMenu;
                    }
                    #endregion

                    switch (masterRequest)
                    {
                        #region  View Students List
                        case 1:
                            using (UnivercityContext dbViewStudents = new UnivercityContext("UniDBConStr"))
                            {
                                List<Student> studentList = masterMain.Students.ToList();
                                if (studentList.Any())
                                {
                                    foreach (var student in studentList)
                                    {
                                        Console.WriteLine($"\n\tStudent Code : {student.StudentCode}    Name : {student.Name}    Family : {student.Family}     Is active : {student.IsActive}");
                                        if (student.Courses.Any())
                                        {
                                            foreach (var course in student.Courses)
                                            {
                                                Console.WriteLine("\n\t           *************************************************************************************");
                                                Console.WriteLine($"\t\t\t\t\t\tName :{course.CourseName}    Unit :{course.CourseUnit}");
                                                foreach (Master master1 in course.Masters)
                                                {
                                                    Console.Write($"\t\t\t\t\t       Master : {master1.Name} {master1.Family}\n");
                                                }
                                            }
                                        }
                                        Console.WriteLine("\n\t    ______________________________________________________________________________________________________");
                                    }
                                }
                                
                            }

                            goto masterMenu;
                        #endregion



                        default: goto masterMenu;

                    }

                // break;
                #endregion

                default:
                    goto loginMenu;
            }
        }
    }
    //1833
}
