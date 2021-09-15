using System;
using System.Collections.Generic;

namespace RulesEngine.Models
{
    public partial class Policy
    {
        public Policy()
        {
            PolicyVersions = new HashSet<PolicyVersion>();
        }

        public int Id { get; set; }
        public int PolicyModelId { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public virtual PolicyModel PolicyModel { get; set; }
        public virtual ICollection<PolicyVersion> PolicyVersions { get; set; }
    }
}

