using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sports.Core
{
    public class Athlete
    {
        [Key]
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public int Result { get; set; }
    }
}
