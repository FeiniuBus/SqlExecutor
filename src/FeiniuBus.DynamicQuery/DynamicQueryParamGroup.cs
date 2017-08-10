using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus
{
    public class DynamicQueryParamGroup
    {
        public DynamicQueryParamGroup()
        {
            ChildGroups = new List<DynamicQueryParamGroup>();
            Params = new List<DynamicQueryParam>();
        }

        public QueryRelation Relation { get; set; }
        public List<DynamicQueryParamGroup> ChildGroups { get; private set; }
        public List<DynamicQueryParam> Params { get; private set; }
    }
}
