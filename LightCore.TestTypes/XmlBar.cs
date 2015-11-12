using System;

namespace LightCore.TestTypes
{
    public class XmlBar : IBar
    {
        public XmlBar()
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; set; }
    }
}