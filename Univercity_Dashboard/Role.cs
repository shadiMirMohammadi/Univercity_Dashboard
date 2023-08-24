using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Univercity_Dashboard
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }


        [Required]
        [Column("Name", TypeName = "varchar")]
        [MaxLength(20)]
        [Index("Key-Name", IsUnique = true)]
        public string RoleName { get; set; }




        public Role() { }
        public Role(int RoleId, string RoleName)
        {
            this.RoleId = RoleId;
            this.RoleName = RoleName;
        }




    }
}
