using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public class UpdateParameter {
        public virtual string PreviousParameterName { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}
