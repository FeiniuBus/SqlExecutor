using System.Collections.Generic;

namespace FeiniuBus.SqlBuilder
{
    public interface IOrderByBuider
    {
        string OrderBy(IEnumerable<DynamicQueryOrder> orders, bool fieldConverter = true);
    }
}
