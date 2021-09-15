using RulesEngine.Core;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RulesEngine.Models
{
    public static class PolicyExtensions
    {
        public static async Task<TResult> RunPolicy<TModel, TResult>(this IScriptCore<TModel> policyCore, PolicyVersion policyVersion, TModel parameter, CancellationToken cancellationToken = default)
        {
            foreach (var rules in policyVersion.PolicyRules.OrderBy(r => r.Order))
            {
                if (await policyCore.EvalAsync<bool>(rules.Condition, parameter, cancellationToken))
                {
                    return await policyCore.EvalAsync<TResult>(rules.Value, parameter, cancellationToken);
                }
            }
            //var result = policyVersion.PolicyRules.OrderBy(r => r.Order)
            //    .Where(r => EvalAsync<bool>(r.Condition, parameter).Result)
            //    .Select(r => EvalAsync<TResult>(r.Value, parameter).Result)
            //    .FirstOrDefault();
            return default;
        }
    }
}

