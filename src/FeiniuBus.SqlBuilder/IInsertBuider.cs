using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public interface IInsertBuider : ISqlBuilder
    {
        IInsertBuider InsertInto(string tableName);
        IInsertBuider Values(string key, object value);
        IInsertBuider Values(IDictionary<string, object> keyvalues);

    }
}
