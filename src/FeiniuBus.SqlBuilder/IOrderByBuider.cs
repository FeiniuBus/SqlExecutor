using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public interface IOrderByBuider
    {
        string OrderBy(IEnumerable<DynamicQueryOrder> orders, bool fieldConverter = true);
    }
}
