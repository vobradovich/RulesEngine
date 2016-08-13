using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RulesEngine.Models;

namespace RulesEngine.Core
{
    public interface IPolicyCore<T>
    {
        void Validate(string expression);
        TResult RunPolicy<TResult>(PolicyVersion policyVersion, T parameter);
    }
}

