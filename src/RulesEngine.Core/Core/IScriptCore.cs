using RulesEngine.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RulesEngine.Core
{
    public interface IScriptCore<T>
    {
        void Validate(string expression);
        Task<TResult> EvalAsync<TResult>(string expression, T parameter, CancellationToken cancellationToken = default);
    }
}

