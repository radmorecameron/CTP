using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class Teacher {

        public Teacher() {
        }

        public Teacher(int teacherId) : this() {
            TeacherId = teacherId;
        }

        public Teacher(int teacherId, int userId) : this(teacherId) {
            UserId = userId;
        }

        [Key]
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual Ctpuser User { get; set; }
    }
}
