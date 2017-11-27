using System.Collections.Generic;

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
        public List<DynamicQueryParamGroup> ChildGroups { get; }
        public List<DynamicQueryParam> Params { get; }
    }
}