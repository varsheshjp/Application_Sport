﻿using Sports.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sports.DomainModel.Models
{
    public class IndexModel
    {
        public List<User> users { get; set; }
        public User user { get; set; }
    }
}
