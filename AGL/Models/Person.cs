using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGL.Models
{
    /// <summary>
    /// Person Details
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string age { get; set; }

        public List<Pets> Pets { get; set; }
    }
}