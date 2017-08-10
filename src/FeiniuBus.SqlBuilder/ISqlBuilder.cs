using System;
using System.Collections.Generic;
using System.Text;
using FeiniuBus.Builder;

namespace FeiniuBus.SqlBuilder
{
    public interface ISqlBuilder
    {
        SqlBuiderResult Buid();
    }
}
