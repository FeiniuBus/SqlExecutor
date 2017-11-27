using System.Collections.Generic;

namespace FeiniuBus.Mapper
{
    public interface IFeiniuBusMapperInitialize
    {
        void Initialize();
        void AddMapperConfig(IMapperConfig config);
        void AddMapperConfigs(IEnumerable<IMapperConfig> configs);
    }
}