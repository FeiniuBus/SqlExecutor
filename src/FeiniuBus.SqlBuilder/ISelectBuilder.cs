using System;

namespace FeiniuBus.SqlBuilder
{
    public interface ISelectBuilder : ISqlBuilder, IWhereBuilder, IOrderByBuider, IDisposable
    {
        /// <summary>
        ///  Create a new instance of ISelectBuilder
        /// </summary>
        /// <returns>ISelectBuilder</returns>
        ISelectBuilder CreateScope();
    }
}
