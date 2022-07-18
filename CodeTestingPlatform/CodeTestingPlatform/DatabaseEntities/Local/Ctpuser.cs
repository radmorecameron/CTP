using CodeTestingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class Ctpuser {

        public Ctpuser() {
            UserCourses = new HashSet<UserCourse>();
        }

        public Ctpuser(string firstName, string lastName) : this() {
            FirstName = firstName;
            LastName = lastName;
        }


        [Key]
        [Required]
        public int UserId { get; set; }
        [StringLength(30)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}
