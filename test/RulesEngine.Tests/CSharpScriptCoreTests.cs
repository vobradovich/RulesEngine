using System;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RulesEngine.Core;
using RulesEngine.Models;

namespace RulesEngine.Tests
{
    public class CSharpScriptCoreTests
    {
        [Fact]
        public void Core_Echo() 
        {
            var core = new CSharpScriptCore<string>();
            var policyVersion = new PolicyVersion { PolicyRules = new List<PolicyRule> { new PolicyRule { Condition = "true", Value = "ToString()" }} };
            var result = core.RunPolicy<string>(policyVersion, "test");
            Assert.NotNull(result);
            Assert.Equal(result, "test");
        }

        [Fact]
        public void Core_Jobject_ToString() 
        {
            var core = new CSharpScriptCore<JObject>();
            var policyVersion = new PolicyVersion { PolicyRules = new List<PolicyRule> { new PolicyRule { Condition = "true", Value = "ToString()" }} };
            var result = core.RunPolicy<string>(policyVersion, JObject.FromObject(policyVersion));
            Assert.NotNull(result);
            Assert.Equal(result, JObject.FromObject(policyVersion).ToString());
        }
    }
}
