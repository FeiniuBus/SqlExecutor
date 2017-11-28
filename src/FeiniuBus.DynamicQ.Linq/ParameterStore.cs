using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.DynamicQ.Linq
{
    public sealed class ParameterStore
    {
        public ParameterStore()
        {
            Statement = new StringBuilder();
            Values = new List<object>();
        }

        public StringBuilder Statement { get; }
        public IList<object> Values { get; }

        public string NextParameterName()
        {
            return CurrentIndex().ToString();
        }

        public int CurrentIndex()
        {
            return Values.Count;
        }
    }
}