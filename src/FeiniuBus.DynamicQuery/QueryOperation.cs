using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus
{
    public enum QueryOperation
    {
        /// <summary>
        ///     等于
        /// </summary>
        Equal = 1,

        /// <summary>
        ///     小于
        /// </summary>
        LessThan,

        /// <summary>
        ///     小于等于
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        ///     大于
        /// </summary>
        GreaterThan,

        /// <summary>
        ///     大于等于
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        ///     包含（like）
        /// </summary>
        Contains,

        /// <summary>
        ///     以xx开始
        /// </summary>
        StartsWith,

        /// <summary>
        ///     以xx结束
        /// </summary>
        EndsWith,

        /// <summary>
        ///     in
        /// </summary>
        In,
        Any,
        DataTimeLessThanOrEqualThenDay
    }
}
