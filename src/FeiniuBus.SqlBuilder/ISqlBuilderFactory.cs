using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISqlBuilderFactory
    {
        IInsertBuider InsertBuider();
        IUpdateBuilder UpdateBuilder();
        IDeleteBuilder DeleteBuilder();
    }
}
