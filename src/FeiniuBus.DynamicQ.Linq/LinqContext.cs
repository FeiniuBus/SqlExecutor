namespace FeiniuBus.DynamicQ.Linq
{
    public sealed class LinqContext
    {
        public LinqContext()
        {
            Parameters = new ParameterStore();
        }

        public ParameterStore Parameters { get; }
    }
}