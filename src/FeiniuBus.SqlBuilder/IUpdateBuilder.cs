using System.Collections.Generic;

namespace FeiniuBus.SqlBuilder
{
    public interface IUpdateBuilder : IWhereBuilder, ISqlBuilder
    {
        IUpdateBuilder Update(string tableName);
        IUpdateBuilder Set(string key, object value);
        IUpdateBuilder Set(IDictionary<string, object> values);
    }
}