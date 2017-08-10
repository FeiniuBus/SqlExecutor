using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace FeiniuBus.Mapper
{
    public interface IMapperConfig
    {
        void Configuration(IMapperConfigurationExpression config);
    }
}
