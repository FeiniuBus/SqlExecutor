using FeiniuBus.SqlBuilder;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public class SqlBuilderOptions
    {
        public SqlBuilderOptions()
        {
            _extensions = new List<ISqlBuilderExtension>();
        }

        private IList<ISqlBuilderExtension> _extensions;

        public IReadOnlyList<ISqlBuilderExtension> Extensions => (IReadOnlyList<ISqlBuilderExtension>)_extensions;

        public void RegisterExtension(ISqlBuilderExtension extension) => _extensions.Add(extension);
    }
}
