using System;

namespace LightCore.TestTypes
{
    public class Bar : IBar
    {
        public Bar()
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; set; }
    }
}