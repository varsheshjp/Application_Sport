using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sports.Core
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public int CoachId { get; set; }
    }
}
