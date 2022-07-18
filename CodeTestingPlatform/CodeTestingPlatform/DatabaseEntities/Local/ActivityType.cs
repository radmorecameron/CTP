using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class ActivityType {
        public ActivityType() {
            Activities = new HashSet<Activity>();
        }

        [Key]
        [Required]
        public int ActivityTypeId { get; set; }
        [StringLength(50)]
        [Required]
        public string ActivityName { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
