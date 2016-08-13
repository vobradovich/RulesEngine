using System;
using System.Collections.Generic;

namespace RulesEngine.Models
{
    public partial class PolicyModel
    {
        public PolicyModel()
        {
            Policies = new HashSet<Policy>();
        }

        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Name { get; set; }

        public string SystemName { get; set; }

        public string TypeName { get; set; }

        public virtual ICollection<Policy> Policies { get; set; }
    }
}

