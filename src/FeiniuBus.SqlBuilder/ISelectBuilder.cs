using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public interface ISelectBuilder : ISqlBuilder, IWhereBuilder, IOrderByBuider
    {
    }
}
