using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Models
{
    public class Associate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Github { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Associate associate &&
                   Name == associate.Name &&
                   Email == associate.Email &&
                   Github == associate.Github;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email, Github);
        }
    }
}
