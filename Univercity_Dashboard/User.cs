using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Univercity_Dashboard
{
    public abstract class User
    {   
        [Key]
        public int UserId { get; set; }

        [Required]
        public string NationalCode { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "varchar")]
        public string Family { get; set; }

        [Required]
        [MaxLength(11)]
        [Column(TypeName = "varchar")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }

        [Required]
        public Role RoleId { get; set; }
 

        public DateTime Birthdate { get; set; }


        public DateTime RegisterDate { get; set; }


        public bool IsActive { get; set; }

        public User() { }
        /// <summary>
        /// متد سازنده کلاس والد
        /// </summary>
        /// <param name="UserId">آیدی</param>
        /// <param name="Name">نام</param>
        /// <param name="Family">نام خانوادگی</param>
        /// <param name="PhoneNumber">تلفن همراه</param>
        /// <param name="Password">پسوورد</param>
        /// <param name="RoleId">آیدی نقش کاربری</param>
        /// <param name="Birthdate">تاریخ تولد</param>
        public User(int UserId, string NationalCode, string Name, string Family, string PhoneNumber, string Password, DateTime Birthdate ,Role RoleId)
        {
            this.UserId = UserId;
            this.NationalCode = NationalCode;
            this.Name = Name;
            this.Family = Family;
            this.PhoneNumber = PhoneNumber;
            this.Password = Password;
            this.RoleId = RoleId;
            this.Birthdate = Birthdate;
            RegisterDate = DateTime.Now;
            IsActive = true;

        }
        /// <summary>
        /// متد سازنده کلاس والد
        /// </summary>
        /// <param name="Name">نام</param>
        /// <param name="Family">نام خانوادگی</param>
        /// <param name="PhoneNumber">تلفن همراه</param>
        /// <param name="Password">پسوورد</param>
        /// <param name="RoleId">آیدی نقش کاربری</param>
        /// <param name="Birthdate">تاریخ تولد</param>
        public User(string NationalCode,string Name, string Family, string PhoneNumber, string Password, Role RoleId, DateTime Birthdate)
        {
            this.NationalCode = NationalCode;
            this.Name = Name;
            this.Family = Family;
            this.PhoneNumber = PhoneNumber;
            this.Password = Password;
            this.RoleId = RoleId;
            this.Birthdate = Birthdate;
            RegisterDate = DateTime.Now;
            IsActive = true;
        }
    }
}
