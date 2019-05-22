using Sports.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sports.DomainModel.Models
{
    public class AddAthleteModel
    {
        public List<User> athletes { get; set; }
        public int testid { get; set; }
        public Athlete athlete { get; set; }
    }
}
