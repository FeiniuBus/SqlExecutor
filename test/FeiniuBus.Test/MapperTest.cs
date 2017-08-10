using System;
using System.Linq;
using AutoMapper;
using FeiniuBus.Mapper;
using Xunit;

namespace FeiniuBus.Test
{
    public class MapperTest
    {
        [Fact]
        public void Test()
        {
            IFeiniuBusMapperInitialize initialize = new FeiniuBusMapperInitialize();
            initialize.AddMapperConfig(new DefaultIMapperConfig());
            initialize.Initialize();



            IEntityMapper mapper = new FeiniuBusEntityMapper();


            IHashObject obj = new HashObject();

            var d = DateTime.Today.AddYears(-10);
            obj.Add("Id", "1001");
            obj.Add("FirstName", "Hello");
            obj.Add("LastName", "Joy");
            obj.Add("Brd", d);
            obj.Add("State", "0");

            var model = mapper.Mapper<IHashObject, TestClass1>(obj);

            Assert.Equal(model.Id, 1001);
            Assert.Equal(model.FirstName, "Hello");
            Assert.Equal(model.LastName, "Joy");
            Assert.Equal(model.Brd, d);
            Assert.Equal(model.State, State.Disabled);

            var model1 = mapper.Mapper<TestClass1, TestClass2>(model);

            Assert.Equal(model1.Name, model.FirstName + " " + model.LastName);
        }



    }

    public class DefaultIMapperConfig : IMapperConfig
    {
        public void Configuration(IMapperConfigurationExpression config)
        {
            config.CreateMap<TestClass1, TestClass2>()
                .ForMember(i => i.Name, s => s.MapFrom(src => src.FirstName + " " + src.LastName))
                ;
        }
    }

    public enum State
    {
        Disabled,
        Enabled
    }

    public class TestClass1
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Brd { get; set; }
        public State State { get; set; }

    }

    public class TestClass2
    {
        public long Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
