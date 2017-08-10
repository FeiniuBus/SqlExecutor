using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlExecutor
{
    public class SqlExecutorOptions
    {
        public string ConnectionString { get; set; }
        public bool EnableLogger { get; set; } = true;
    }
}
