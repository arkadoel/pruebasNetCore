using System;
using System.Collections.Generic;

namespace consolaYentityFramework.Models
{
    public partial class Persona
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NuevaColumnaEdad { get; set; }
    }
}
