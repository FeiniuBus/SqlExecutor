using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public interface IDeleteBuilder : IWhereBuilder, ISqlBuilder
    {
        IDeleteBuilder Delete(string tableName);
    }
}
