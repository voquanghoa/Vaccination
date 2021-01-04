using System;

namespace AspBusiness.AutoConfig
{
    public class ConvertFromAttribute: Attribute
    {
        public Type FromType { get; }

        public ConvertFromAttribute(Type fromType)
        {
            FromType = fromType;
        }
    }
}