using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public class Login {
        //[Editable(false)]
        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        //  [Editable(false)]
        //[Required(ErrorMessage = "Please enter a username")]
        public string Username { get; set; }
        public Login() { }
    }
}
