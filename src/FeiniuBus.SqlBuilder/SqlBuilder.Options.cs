using System.Collections.Generic;
using FeiniuBus.SqlBuilder;

namespace Microsoft.Extensions.DependencyInjection
{
    public class SqlBuilderOptions
    {
        private readonly IList<ISqlBuilderExtension> _extensions;

        public SqlBuilderOptions()
        {
            _extensions = new List<ISqlBuilderExtension>();
        }

        public IReadOnlyList<ISqlBuilderExtension> Extensions => (IReadOnlyList<ISqlBuilderExtension>) _extensions;

        public void RegisterExtension(ISqlBuilderExtension extension)
        {
            _extensions.Add(extension);
        }
    }
}