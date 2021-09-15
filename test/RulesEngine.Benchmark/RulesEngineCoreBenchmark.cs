using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using RulesEngine.Core;
using RulesEngine.Models;

namespace RulesEngine.Benchmark
{
    public class RulesEngineCoreBenchmark
    {
        private CSharpScriptCore<string> core;

        [Params(100, 1000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            core = new CSharpScriptCore<string>();
        }

        [Benchmark]
        public void RunPolicyString()
        {

            var policyVersion = new PolicyVersion
            {
                PolicyRules =
                {
                    new PolicyRule { Condition = "Equals(\"\")", Value = "ToString()", Order = 1 },
                    new PolicyRule { Condition = "Equals(\"test\")", Value = "ToString()", Order = 2 },
                    new PolicyRule { Condition = "true", Value = "ToString()", Order = 3 },
                }
            };
            var result = core.RunPolicy<string, string>(policyVersion, "test").ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Benchmark]
        public void RunPolicyJObject()
        {
            var core2 = new CSharpScriptCore<JObject>();
            var policyVersion = new PolicyVersion
            {
                Id = 1,
                PolicyRules =
                {
                    new PolicyRule { Condition = "ContainsKey(\"Id\")", Value = "Value<int>(\"Id\")" }
                }
            };
            var result = core2.RunPolicy<JObject, int>(policyVersion, JObject.FromObject(policyVersion)).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
