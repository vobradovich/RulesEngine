using System;
using System.Collections.Generic;

namespace RulesEngine.Models
{
    public partial class PolicyRule
    {
        public int PolicyVersionId { get; set; }

        public DateTime Created { get; set; }

        public int Order { get; set; }

        public string Condition { get; set; }

        public string Value { get; set; }

        public virtual PolicyVersion PolicyVersion { get; set; }
    }
}

