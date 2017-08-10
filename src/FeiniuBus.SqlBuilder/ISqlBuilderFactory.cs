using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public interface ISqlBuilderFactory
    {
        IInsertBuider InsertBuider();
        IUpdateBuilder UpdateBuilder();
        IDeleteBuilder DeleteBuilder();

    }
}
