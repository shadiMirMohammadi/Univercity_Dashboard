using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univercity_Dashboard
{
    public class Admin : User
    {
        public Admin() { }
        public Admin(string NationalCode, string Name, string Family, string PhoneNumber, string Password, Role RoleId, DateTime Birthdate) : base(NationalCode,Name, Family, PhoneNumber, Password, RoleId, Birthdate) { }
    }
}
