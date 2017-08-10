using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.Mapper
{
    public interface IFeiniuBusMapperInitialize
    {
        void Initialize();
        void AddMapperConfig(IMapperConfig config);
        void AddMapperConfigs(IEnumerable<IMapperConfig> configs);
    }
}
