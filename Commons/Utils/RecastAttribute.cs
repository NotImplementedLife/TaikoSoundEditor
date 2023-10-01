using System;
namespace TaikoSoundEditor.Commons.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class RecastAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public RecastAttribute(string property)
        {
            PropertyName = property;
        }
    }
}
