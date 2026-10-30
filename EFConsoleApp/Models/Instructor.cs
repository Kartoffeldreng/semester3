using System;
using System.Collections.Generic;
using System.Text;

namespace EFConsoleApp.Models {
    public class Instructor {
        public int Id { get; set; }

        public string Name { get; set; }


        // Collection Navigation Property
        public ICollection<Course> Courses { get; set; } = [];
    }
}
