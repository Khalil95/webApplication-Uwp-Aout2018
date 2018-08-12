using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public Person() { }
    }
}
