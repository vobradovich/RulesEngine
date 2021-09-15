using System;
using System.Collections.Generic;

namespace RulesEngine.Models
{
    public partial class PolicyVersion
    {
        public PolicyVersion()
        {
            PolicyRules = new HashSet<PolicyRule>();
        }

        public int Id { get; set; }

        public int PolicyId { get; set; }

        public DateTime Created { get; set; }

        public virtual Policy Policy { get; set; }

        public virtual ICollection<PolicyRule> PolicyRules { get; set; }
    }
}

