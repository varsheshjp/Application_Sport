using Sports.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace Sports.DomainModel.Models
{
    public class DetailTestModel
    {
        public Test test { get; set; }
        public List<UserResult> athletes { get; set; }
    }
}
