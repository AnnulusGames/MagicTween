using System;

namespace MagicTween
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RegisterTweenTypeAttribute : Attribute
    {
        public readonly Type valueType;
        public readonly Type optionsType;
        public readonly Type pluginType;

        public RegisterTweenTypeAttribute(Type valueType, Type optionsType, Type pluginType)
        {
            this.valueType = valueType;
            this.optionsType = optionsType;
            this.pluginType = pluginType;
        }
    }
}