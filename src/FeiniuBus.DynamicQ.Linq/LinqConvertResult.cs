namespace FeiniuBus.DynamicQ.Linq
{
    public class LinqConvertResult
    {
        public LinqConvertResult()
        {
            Where = Order = string.Empty;
            IsPager = true;
        }

        public string Where { get; set; }
        public object[] WhereValue { get; set; }
        public string Order { get; set; }
        public bool IsPager { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Select { get; set; }
    }
}