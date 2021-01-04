using System;

namespace AspBusiness.AutoConfig
{
    public class ImplementByAttribute: Attribute
    {
        public Type Type { get; }

        public ImplementByAttribute(Type type)
        {
            Type = type;
        }
    }
}