using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Clara {
    public class ClaraCSStudent {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int StudentId { get; set; }
        [Key]
        public string CSStudentId { get; set; }
        /*
        public async Task<List<ClaraCSStudent>> GetClaraCSStudents() {
            return await _db.ClaraCSStudents.ToListAsync();
        }
        */
    }
}
