using System;

namespace LightCore.TestTypes
{
    public class SqlBar : IBar
    {
        public SqlBar()
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; set; }
    }
}