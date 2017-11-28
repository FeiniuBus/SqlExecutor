using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FeiniuBus.DynamicQ.Infrastructure
{
    public sealed class ParameterGroupConvertResult
    {
        public ParameterGroupConvertResult(string clause, IEnumerable<object> values)
        {
            Clause = clause;
            Values = new ReadOnlyCollection<object>(values.ToList());
        }

        public string Clause { get; }
        public IReadOnlyList<object> Values { get; }
    }
}