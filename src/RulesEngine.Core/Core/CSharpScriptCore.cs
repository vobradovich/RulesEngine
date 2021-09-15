using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RulesEngine.Core
{
    public class CSharpScriptCore<T> : IScriptCore<T>
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

        public Task<TResult> EvalAsync<TResult>(string expression, T parameter, CancellationToken cancellationToken = default)
            //=> CSharpScript.EvaluateAsync<TResult>(expression, options: ScriptOptions.Default.WithReferences(
            //        MetadataReference.CreateFromFile(typeof(T).GetTypeInfo().Assembly.Location),
            //        MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location)
            //    ), globals: parameter, cancellationToken: cancellationToken);
            => ScriptRunnerCache<T, TResult>.GetRunner(expression).Invoke(parameter, cancellationToken);
    }

    public static class ScriptRunnerCache<TParameter, TResult>
    {
        private static ConcurrentDictionary<string, ScriptRunner<TResult>> _cache = new ConcurrentDictionary<string, ScriptRunner<TResult>>();

        public static ScriptRunner<TResult> GetRunner(string expression)
            => _cache.GetOrAdd(expression, (e) => CreateScript(e).CreateDelegate());

        public static Script<TResult> CreateScript(string expression)
        {
            var script = CSharpScript.Create<TResult>(expression,
                options: ScriptOptions.Default
                    .WithReferences(
                        MetadataReference.CreateFromFile(typeof(TParameter).GetTypeInfo().Assembly.Location),
                        MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location)
                    )
                    .WithImports("System", typeof(Enumerable).Namespace, typeof(TParameter).Namespace),
                globalsType: typeof(TParameter));
            return script;
        }
    }
}
