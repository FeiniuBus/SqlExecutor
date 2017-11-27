using System.Collections.Generic;

namespace FeiniuBus.Mapper
{
    public class FeiniuBusMapperInitialize : IFeiniuBusMapperInitialize
    {
        private readonly List<IMapperConfig> _mapperConfig;

        public FeiniuBusMapperInitialize()
        {
            _mapperConfig = new List<IMapperConfig>();
        }

        public void AddMapperConfig(IMapperConfig config)
        {
            _mapperConfig.Add(config);
        }

        public void AddMapperConfigs(IEnumerable<IMapperConfig> configs)
        {
            foreach (var mapperConfig in configs)
                AddMapperConfig(mapperConfig);
        }

        public void Initialize()
        {
            AutoMapper.Mapper.Initialize(map =>
            {
                foreach (var item in _mapperConfig)
                    item.Configuration(map);
            });
        }
    }
}