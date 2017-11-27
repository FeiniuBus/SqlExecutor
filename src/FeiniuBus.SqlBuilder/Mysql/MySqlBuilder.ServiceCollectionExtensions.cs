using FeiniuBus.SqlBuilder.Mysql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MySQLServiceCollectionExtensions
    {
        public static void UseMySQL(this SqlBuilderOptions opts)
        {
            var extension = new MySqlBuilderExtension();
            opts.RegisterExtension(extension);
        }
    }
}