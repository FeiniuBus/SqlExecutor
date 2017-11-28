namespace FeiniuBus.DynamicQ.Infrastructure
{
    public sealed partial class QueryOperations
    {
        /// <summary>
        ///     等于
        /// </summary>
        public static QueryOperations Equal => "Equal";

        /// <summary>
        ///     小于
        /// </summary>
        public static QueryOperations LessThan => "LessThan";

        /// <summary>
        ///     小于等于
        /// </summary>
        public static QueryOperations LessThanOrEqual => "LessThanOrEqual";

        /// <summary>
        ///     大于
        /// </summary>
        public static QueryOperations GreaterThan => "GreaterThan";

        /// <summary>
        ///     大于等于
        /// </summary>
        public static QueryOperations GreaterThanOrEqual => "GreaterThanOrEqual";

        /// <summary>
        ///     包含（like）
        /// </summary>
        public static QueryOperations Contains => "Contains";

        /// <summary>
        ///     以xx开始
        /// </summary>
        public static QueryOperations StartsWith => "StartsWith";

        /// <summary>
        ///     以xx结束
        /// </summary>
        public static QueryOperations EndsWith => "EndsWith";

        /// <summary>
        ///     in
        /// </summary>
        public static QueryOperations In => "In";

        public static QueryOperations Any => "Any";
        //public static QueryOperations DataTimeLessThanOrEqualThenDay => "DataTimeLessThanOrEqualThenDay";
    }
}