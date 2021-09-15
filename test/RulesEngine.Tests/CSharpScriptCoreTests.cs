using Newtonsoft.Json.Linq;
using RulesEngine.Core;
using RulesEngine.Models;
using System.Threading.Tasks;
using Xunit;

namespace RulesEngine.Tests
{
    public class CSharpScriptCoreTests
    {
        [Fact]
        public async Task Core_String_Echo()
        {
            var core = new CSharpScriptCore<string>();
            var policyVersion = new PolicyVersion
            {
                PolicyRules =
                {
                    new PolicyRule { Condition = "Equals(\"\")", Value = "ToString()", Order = 1 },
                    new PolicyRule { Condition = "Equals(\"test\")", Value = "ToString()", Order = 2 },
                    new PolicyRule { Condition = "true", Value = "ToString()", Order = 3 },
                }
            };
            var result = await core.RunPolicy<string, string>(policyVersion, "test");
            Assert.NotNull(result);
            Assert.Equal("test", result);
        }
        [Fact]
        public async Task Core_JObject_ToString()
        {
            var core = new CSharpScriptCore<JObject>();
            var policyVersion = new PolicyVersion
            {
                Id = 1,
                PolicyRules =
                {
                    new PolicyRule { Condition = "true", Value = "ToString()" }
                }
            };
            var jobject = JObject.FromObject(policyVersion);
            var result = await core.RunPolicy<JObject, string>(policyVersion, jobject);
            Assert.Equal(jobject.ToString(), result);
        }

        [Fact]
        public async Task Core_JObject_JToken()
        {
            var core = new CSharpScriptCore<JObject>();
            var policyVersion = new PolicyVersion
            {
                Id = 1,
                PolicyRules =
                {
                    new PolicyRule { Condition = "ContainsKey(\"Id\")", Value = "GetValue(\"Id\")" }
                }
            };
            var result = await core.RunPolicy<JObject, JToken>(policyVersion, JObject.FromObject(policyVersion));
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Core_JObject_Int()
        {
            var core = new CSharpScriptCore<JObject>();
            var policyVersion = new PolicyVersion
            {
                Id = 1,
                PolicyRules =
                {
                    new PolicyRule { Condition = "ContainsKey(\"Id\")", Value = "Value<int>(\"Id\")" }
                }
            };
            var result = await core.RunPolicy<JObject, int>(policyVersion, JObject.FromObject(policyVersion));
            Assert.Equal(1, result);
        }
    }
}
