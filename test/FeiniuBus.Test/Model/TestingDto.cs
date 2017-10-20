using System.Collections.Generic;

namespace FeiniuBus.Test.Model
{
    public class TestingDto
    {
        public TestingDto()
        {
            Extras = new HashSet<Extra>();
        }
        public string Id { get; set; }
        public string Address { get; set; }
        public bool Disabled { get; set; }
        public int Amout { get; set; }
        public double Price { get; set; }
        public string Drink { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public string Url { get; set; }

        public HashSet<Extra> Extras { get; set; }

        public class Extra
        {
            public string Guest { get; set; }
        }
    }
}
