using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus
{
    public class DynamicQuery
    {
        public DynamicQuery() : this(true)
        {
        }

        public DynamicQuery(bool pager)
        {
            ParamGroup = new DynamicQueryParamGroup();
            Order = new List<DynamicQueryOrder>();
            Pager = pager;
        }

        public DynamicQueryParamGroup ParamGroup { get; set; }
        public List<DynamicQueryOrder> Order { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public bool Pager { get; set; }
        public string Select { get; set; }
    }
}
