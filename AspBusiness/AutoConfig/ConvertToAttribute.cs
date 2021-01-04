using System;

namespace AspBusiness.AutoConfig
{
    public class ConvertToAttribute: Attribute
    {
        public Type ToType { get; }

        public ConvertToAttribute(Type toType)
        {
            ToType = toType;
        }
    }
}