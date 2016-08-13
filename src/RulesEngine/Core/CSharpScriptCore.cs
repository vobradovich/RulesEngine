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
                .Where(r => Eval<bool>(r.Condition, parameter).Result)
                .Select(r => Eval<TResult>(r.Value, parameter).Result)
                .FirstOrDefault();
            return result;
        }

        private Task<T2> Eval<T2>(string expression, T parameter)
        {
            var runner = ScriptRunnerCache<T, T2>.GetRunner(expression);
            return runner(parameter);
        }
    }

    public static class ScriptRunnerCache<T, T2>
    {
        static Dictionary<string, ScriptRunner<T2>> _cache = new Dictionary<string, ScriptRunner<T2>>();

        public static ScriptRunner<T2> GetRunner(string expression)
        {
            ScriptRunner<T2> runner;
            if (!_cache.TryGetValue(expression, out runner))
            {
                runner = CreateScript(expression).CreateDelegate();
                _cache.Add(expression, runner);                
            }
            return runner;
        }

        public static Script<T2> CreateScript(string expression)
        {
            var script = CSharpScript.Create<T2>(expression,
                 options: ScriptOptions.Default.WithReferences(
                     MetadataReference.CreateFromFile(typeof(T).GetTypeInfo().Assembly.Location),
                     MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location)
                 ),
                globalsType: typeof(T));
            return script;
        }
    }
}
