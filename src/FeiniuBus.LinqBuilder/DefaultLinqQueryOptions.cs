namespace FeiniuBus.LinqBuilder
{
    public static class DefaultLinqQueryOptions
    {
        private static DynamicQueryOptions _options = new DynamicQueryOptions();

        public static DynamicQueryOptions Options => _options;
    }
}
