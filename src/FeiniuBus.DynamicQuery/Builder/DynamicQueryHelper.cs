using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeiniuBus
{
    static class DynamicQueryHelper
    {
        public static void CheckDynamicQueryParamGroup(this DynamicQueryParamGroup group)
        {
            if (group.Params.Any() && group.ChildGroups.Any())
                throw new Exception("DynamicQueryParamGroup不能同时添加Params和Group");
        }
    }
}
