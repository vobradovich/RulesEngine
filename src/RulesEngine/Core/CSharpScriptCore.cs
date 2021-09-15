using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis;
using RulesEngine.Models;
using System.Reflection;

namespace RulesEngine.Core
{
    public class CSharpScriptCore<T> : IPolicyCore<T>
    {
        public void Validate(string expression)
        {
            var script = ScriptRunnerCache<T, object>.CreateScript(expression);
            var res = script.Compile();
            if (res.Length > 0)
            {
                throw new Exception(string.Join(Environment.NewLine, res));
            }
        }

        public TResult RunPolicy<TResult>(PolicyVersion policyVersion, T parameter)
        {
            var result = policyVersion.PolicyRules
                .OrderBy(r => r.Order)
                .Where(r => EvalAsync<bool>(r.Condition, parameter).Result)
                .Select(r => EvalAsync<TResult>(r.Value, parameter).Result)
                .FirstOrDefault();
            return result;
        }

        private async Task<TResult> EvalAsync<TResult>(string expression, T parameter)
        {
            var runner = ScriptRunnerCache<T, TResult>.GetRunner(expression);
            return await runner(parameter);
        }
    }

    public static class ScriptRunnerCache<TParameter, TResult>
    {
        private static ConcurrentDictionary<string, ScriptRunner<TResult>> _cache = new ConcurrentDictionary<string, ScriptRunner<TResult>>();

        public static ScriptRunner<TResult> GetRunner(string expression)
        {
            return _cache.GetOrAdd(expression, (e) => CreateScript(e).CreateDelegate());
        }

        public static Script<TResult> CreateScript(string expression)
        {
            var script = CSharpScript.Create<TResult>(expression,
                options: ScriptOptions.Default.WithReferences(
                    MetadataReference.CreateFromFile(typeof(TParameter).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location)
                ),
                globalsType: typeof(TParameter));
            return script;
        }
    }
}
