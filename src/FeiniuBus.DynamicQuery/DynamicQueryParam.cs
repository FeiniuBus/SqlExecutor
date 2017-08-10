using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus
{
    public class DynamicQueryParam
    {
        public string Field { get; set; }
        public QueryOperation Operator { get; set; }
        public object Value { get; set; }
    }
}
